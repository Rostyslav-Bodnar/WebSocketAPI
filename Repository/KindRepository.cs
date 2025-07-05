using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class KindRepository : IKindRepository
    {
        private readonly AppDbContext dbContext;

        public KindRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Kind> AddAsync(Kind entity)
        {
            await dbContext.Kinds.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.Kinds.FindAsync(id);
            if (entity == null)
                return false;

            dbContext.Kinds.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Kind>> GetAllAsync()
        {
            return await dbContext.Kinds.ToListAsync();
        }

        public async Task<Kind> GetByIdAsync(int id)
        {
            return await dbContext.Kinds.FindAsync(id);
        }

        public async Task<Kind> GetByNameAsync(string name)
        {
            return await dbContext.Kinds
                .FirstOrDefaultAsync(k => k.Name == name);
        }

        public async Task<Kind> UpdateAsync(Kind entity)
        {
            var existing = await dbContext.Kinds.FindAsync(entity.Id);
            if (existing == null)
                throw new Exception("Kind not found");

            existing.Name = entity.Name;
            await dbContext.SaveChangesAsync();

            return existing;
        }

    }
}

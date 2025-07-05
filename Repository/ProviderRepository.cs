using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly AppDbContext dbContext;

        public ProviderRepository(AppDbContext context) => dbContext = context;

        public async Task<Provider> AddAsync(Provider entity)
        {
            await dbContext.Providers.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.Providers.FindAsync(id);
            if (entity == null) return false;
            dbContext.Providers.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Provider>> GetAllAsync() => await dbContext.Providers.ToListAsync();

        public async Task<Provider?> GetByIdAsync(int id) => await dbContext.Providers.FindAsync(id);

        public async Task<Provider?> GetByName(string name) => await dbContext.Providers.FirstOrDefaultAsync(p => p.Name ==name);

        public async Task<Provider> UpdateAsync(Provider entity)
        {
            var existing = await dbContext.Providers.FindAsync(entity.Id);
            if (existing == null) throw new Exception("Provider not found");

            existing.Name = entity.Name;
            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}

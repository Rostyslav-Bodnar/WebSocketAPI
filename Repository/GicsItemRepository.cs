using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class GicsItemRepository : IGicsItemRepository
    {
        private readonly AppDbContext dbContext;

        public GicsItemRepository(AppDbContext context) => dbContext = context;

        public async Task<GicsItem> AddAsync(GicsItem entity)
        {
            await dbContext.GicsItems.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.GicsItems.FindAsync(id);
            if (entity == null) return false;
            dbContext.GicsItems.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<GicsItem>> GetAllAsync() => await dbContext.GicsItems.Include(i => i.Children).ToListAsync();

        public async Task<GicsItem?> GetByIdAsync(int id) => await dbContext.GicsItems.Include(i => i.Children).FirstOrDefaultAsync(i => i.GicsId == id);

        public async Task<GicsItem> UpdateAsync(GicsItem entity)
        {
            var existing = await dbContext.GicsItems.FindAsync(entity.GicsId);
            if (existing == null) throw new Exception("GicsItem not found");

            existing.Name = entity.Name;
            existing.GicsParentId = entity.GicsParentId;
            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}

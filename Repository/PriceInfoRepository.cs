using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class PriceInfoRepository : IPriceInfoRepository
    {
        private readonly AppDbContext dbContext;

        public PriceInfoRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PriceInfo> AddAsync(PriceInfo entity)
        {
            await dbContext.PriceInfos.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.PriceInfos.FindAsync(id);
            if (entity == null) return false;
            dbContext.PriceInfos.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<PriceInfo>> GetAllAsync()
        {
            return await dbContext.PriceInfos.ToListAsync();
        }

        public async Task<PriceInfo> GetByIdAsync(int id)
        {
            return await dbContext.PriceInfos.FindAsync(id);
        }

        public async Task<PriceInfo> UpdateAsync(PriceInfo entity)
        {
            var existing = await dbContext.PriceInfos.FindAsync(entity.Id);
            if (existing == null) throw new Exception("Instrument not found");

            existing.Price = entity.Price;
            existing.UpdatedAt = entity.UpdatedAt;
            existing.AssetId = entity.AssetId;

            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}

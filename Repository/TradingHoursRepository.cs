using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class TradingHoursRepository : ITradingHoursRepository
    {
        private readonly AppDbContext dbContext;

        public TradingHoursRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TradingHours> AddAsync(TradingHours entity)
        {
            await dbContext.TradingHours.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.TradingHours.FindAsync(id);
            if (entity == null)
                return false;

            dbContext.TradingHours.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<TradingHours>> GetAllAsync()
        {
            return await dbContext.TradingHours.ToListAsync();
        }

        public async Task<TradingHours?> GetByIdAsync(int id)
        {
            return await dbContext.TradingHours.FindAsync(id);
        }

        public async Task<TradingHours> UpdateAsync(TradingHours entity)
        {
            var existing = await dbContext.TradingHours.FindAsync(entity.Id);
            if (existing == null)
                throw new Exception("TradingHours not found");

            existing.RegularStart = entity.RegularStart;
            existing.RegularEnd = entity.RegularEnd;
            existing.ElectronicStart = entity.ElectronicStart;
            existing.ElectronicEnd = entity.ElectronicEnd;
            existing.InstrumentMappingId = entity.InstrumentMappingId;

            await dbContext.SaveChangesAsync();

            return existing;
        }
    }
}

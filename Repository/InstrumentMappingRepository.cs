using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class InstrumentMappingRepository : IInstrumentMappingRepository
    {
        private readonly AppDbContext dbContext;

        public InstrumentMappingRepository(AppDbContext context)
        {
            dbContext = context;
        }

        public async Task<InstrumentMapping> AddAsync(InstrumentMapping entity)
        {
            await dbContext.InstrumentMappings.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.InstrumentMappings.FindAsync(id);
            if (entity == null) return false;
            dbContext.InstrumentMappings.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<InstrumentMapping>> GetAllAsync()
        {
            return await dbContext.InstrumentMappings.ToListAsync();
        }

        public async Task<InstrumentMapping> GetByIdAsync(int id)
        {
            return await dbContext.InstrumentMappings.FindAsync(id);
        }

        public async Task<InstrumentMapping> UpdateAsync(InstrumentMapping entity)
        {
            var existing = await dbContext.InstrumentMappings.FindAsync(entity.Id);
            if (existing == null) throw new Exception("InstrumentMapping not found");

            existing.Symbol = entity.Symbol;
            existing.ProviderId = entity.ProviderId;
            existing.ExchangeId = entity.ExchangeId;
            existing.DefaultOrderSize = entity.DefaultOrderSize;
            existing.MaxOrderSize = entity.MaxOrderSize;
            existing.TradingHoursId = entity.TradingHoursId;

            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}

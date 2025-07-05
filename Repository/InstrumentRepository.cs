using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class InstrumentRepository : IInstrumentRepository
    {
        private readonly AppDbContext dbContext;

        public InstrumentRepository(AppDbContext context)
        {
            dbContext = context;
        }

        public async Task<Instrument> AddAsync(Instrument entity)
        {
            await dbContext.Instruments.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.Instruments.FindAsync(id);
            if (entity == null) return false;
            dbContext.Instruments.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Instrument>> GetAllAsync()
        {
            return await dbContext.Instruments
                .Include(i => i.InstrumentMappings)
                    .ThenInclude(im => im.Provider)
                .Include(i => i.InstrumentProfile)
                .ToListAsync();
        }

        public async Task<Instrument> GetByIdAsync(int id)
        {
            return await dbContext.Instruments.FindAsync(id);
        }

        public async Task<Instrument?> GetByInstrumentId(string instrumentId)
        {
            return await dbContext.Instruments
                .Include(i => i.InstrumentMappings)
                    .ThenInclude(im => im.Provider)
                .Include(i => i.InstrumentProfile)
                .FirstOrDefaultAsync(i => i.InstrumentId == instrumentId);
        }

        public async Task<Instrument?> GetBySymbol(string symbol)
        {
            return await dbContext.Instruments
                .Include(i => i.InstrumentMappings)
                    .ThenInclude(im => im.Provider)
                .Include(i => i.InstrumentProfile)
                .FirstOrDefaultAsync(i => i.Symbol == symbol);
        }

        public async Task<Instrument> UpdateAsync(Instrument entity)
        {
            var existing = await dbContext.Instruments.FindAsync(entity.Id);
            if (existing == null) throw new Exception("Instrument not found");

            existing.Symbol = entity.Symbol;
            existing.Description = entity.Description;
            existing.Currency = entity.Currency;
            existing.tickSize = entity.tickSize;
            existing.ExchangeId = entity.ExchangeId;
            existing.KindId = entity.KindId;
            existing.InstrumentProfileId = entity.InstrumentProfileId;

            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}

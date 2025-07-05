using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class InstrumentProfileRepository : IInstrumentProfileRepository
    {
        private readonly AppDbContext dbContext;

        public InstrumentProfileRepository(AppDbContext context) => dbContext = context;

        public async Task<InstrumentProfile> AddAsync(InstrumentProfile entity)
        {
            await dbContext.InstrumentProfiles.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.InstrumentProfiles.FindAsync(id);
            if (entity == null) return false;
            dbContext.InstrumentProfiles.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<InstrumentProfile>> GetAllAsync() => await dbContext.InstrumentProfiles.Include(p => p.GicsClassification).ToListAsync();

        public async Task<InstrumentProfile?> GetByIdAsync(int id) => await dbContext.InstrumentProfiles.Include(p => p.GicsClassification).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<InstrumentProfile> GetByInstrumentIdAsync(string instrumentId)
        {
            return await dbContext.InstrumentProfiles
                .Include(p => p.GicsClassification)
                .FirstOrDefaultAsync(p => p.Name == instrumentId);
        }

        public async Task<InstrumentProfile> UpdateAsync(InstrumentProfile entity)
        {
            var existing = await dbContext.InstrumentProfiles.FindAsync(entity.Id);
            if (existing == null) throw new Exception("InstrumentProfile not found");

            existing.Name = entity.Name;
            existing.Location = entity.Location;
            existing.GicsClassificationId = entity.GicsClassificationId;
            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}

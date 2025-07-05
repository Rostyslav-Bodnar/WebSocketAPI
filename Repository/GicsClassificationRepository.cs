using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class GicsClassificationRepository : IGicsClassificationRepository
    {
        private readonly AppDbContext dbContext;

        public GicsClassificationRepository(AppDbContext context) => dbContext = context;

        public async Task<GicsClassification> AddAsync(GicsClassification entity)
        {
            await dbContext.GicsClassifications.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.GicsClassifications.FindAsync(id);
            if (entity == null) return false;
            dbContext.GicsClassifications.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<GicsClassification>> GetAllAsync() => await dbContext.GicsClassifications.ToListAsync();

        public async Task<GicsClassification?> GetByIdAsync(int id) => await dbContext.GicsClassifications.FindAsync(id);

        public async Task<GicsClassification> UpdateAsync(GicsClassification entity)
        {
            var existing = await dbContext.GicsClassifications.FindAsync(entity.Id);
            if (existing == null) throw new Exception("GICS Classification not found");

            existing.SectorId = entity.SectorId;
            existing.IndustryGroupId = entity.IndustryGroupId;
            existing.IndustryId = entity.IndustryId;
            existing.SubIndustryId = entity.SubIndustryId;
            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}

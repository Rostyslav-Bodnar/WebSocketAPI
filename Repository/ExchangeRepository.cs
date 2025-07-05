using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly AppDbContext dbContext;

        public ExchangeRepository(AppDbContext context) => dbContext = context;

        public async Task<Exchange> AddAsync(Exchange entity)
        {
            await dbContext.Exchanges.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.Exchanges.FindAsync(id);
            if (entity == null) return false;
            dbContext.Exchanges.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Exchange>> GetAllAsync() => await dbContext.Exchanges.ToListAsync();

        public async Task<Exchange?> GetByIdAsync(int id) => await dbContext.Exchanges.FindAsync(id);

        public async Task<Exchange> GetByNameAsync(string name)
        {
            return await dbContext.Exchanges
                .FirstOrDefaultAsync(k => k.Name == name);
        }

        public async Task<Exchange> UpdateAsync(Exchange entity)
        {
            var existing = await dbContext.Exchanges.FindAsync(entity.Id);
            if (existing == null) throw new Exception("Exchange not found");

            existing.Name = entity.Name;
            await dbContext.SaveChangesAsync();
            return existing;
        }
    }
}

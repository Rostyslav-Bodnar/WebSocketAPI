using Microsoft.EntityFrameworkCore;
using WebSocket_API.Data;
using WebSocket_API.DTO;
using WebSocket_API.Entities;
using WebSocket_API.Repository.RepositoryInterfaces;

namespace WebSocket_API.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AppDbContext dbContext;

        public AssetRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Asset> AddAsync(Asset entity)
        {
            await dbContext.Assets.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbContext.Assets.FindAsync(id);
            if (entity == null) return false;
            dbContext.Assets.Remove(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Asset?> DeleteBySymbol(string symbol)
        {
            var entity = await dbContext.Assets
                .Include(a => a.Instrument)
                .Include(a => a.LatestPrice)
                .Include(a => a.PriceHistory)
                .FirstOrDefaultAsync(a => a.Instrument.Symbol == symbol);

            if (entity == null) return null;

            if (entity.LatestPriceId != null)
            {
                entity.LatestPriceId = null;
                entity.LatestPrice = null;
            }

            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();

            dbContext.PriceInfos.RemoveRange(entity.PriceHistory);

            dbContext.Assets.Remove(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }


        public async Task<List<Asset>> GetAllAsync()
        {
            return await dbContext.Assets
                .Include(a => a.Instrument)
                .Include(a => a.Provider)
                .Include(a => a.LatestPrice)
                .ToListAsync();
        }

        public async Task<Asset> GetByIdAsync(int id)
        {
            return await dbContext.Assets.FindAsync(id);
        }

        public async Task<Asset> UpdateAsync(Asset entity)
        {
            var existing = await dbContext.Assets.FindAsync(entity.Id);
            if (existing == null) throw new Exception("Instrument not found");

            existing.InstrumentId = entity.InstrumentId;
            existing.ProviderId = entity.ProviderId;
            existing.LatestPriceId = entity.LatestPriceId;
            existing.PriceHistory = entity.PriceHistory;

            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<Asset?> UpdateLatestPrice(PriceInfo priceInfo)
        {
            var asset = await dbContext.Assets.FindAsync(priceInfo.AssetId);
            if (asset == null) return null;

            asset.LatestPriceId = priceInfo.Id;
            await dbContext.SaveChangesAsync();

            return asset;
        }

        public async Task<List<Asset>?> GetByInstrumentIdAsync(string instrumentId)
        {
            return await dbContext.Assets
                .Include(a => a.Instrument)
                .Include(a => a.Provider)
                .Include(a => a.LatestPrice)
                .Where(a => a.Instrument.InstrumentId == instrumentId).ToListAsync();
        }

        public async Task<List<Asset>?> GetByProvider(string providerName)
        {
            return await dbContext.Assets
                .Include(a => a.Instrument)
                .Include(a => a.Provider)
                .Include(a => a.LatestPrice)
                .Where(a => a.Provider.Name == providerName).ToListAsync();
        }

        public async Task<Asset?> GetByProviderAndInstrument(string instrumentId, string providerName)
        {
            return await dbContext.Assets
                .Include(a => a.Instrument)
                .Include(a => a.Provider)
                .Include(a => a.LatestPrice)
                .FirstOrDefaultAsync(a => a.Provider.Name == providerName && a.Instrument.InstrumentId == instrumentId);
        }

        public async Task<Asset> Create(string instrumentId, string providerName)
        {
            var instrument = await dbContext.Instruments.FirstOrDefaultAsync(i => i.InstrumentId == instrumentId);
            if (instrument == null)
                throw new Exception("Instrument not found");

            var provider = await dbContext.Providers.FirstOrDefaultAsync(p => p.Name == providerName);
            if (provider == null)
                throw new Exception("Provider not found");

            var newAsset = new Asset
            {
                InstrumentId = instrument.Id,
                ProviderId = provider.Id,
                PriceHistory = new List<PriceInfo>()
            };

            await dbContext.Assets.AddAsync(newAsset);
            await dbContext.SaveChangesAsync();

            return newAsset;
        }

        public async Task<List<Asset>?> GetBySymbol(string symbol)
        {
            return await dbContext.Assets
                .Include(a => a.Instrument)
                .Include(a => a.Provider)
                .Include(a => a.LatestPrice)
                .Where(a => a.Instrument.Symbol == symbol).ToListAsync();
        }

    }
}

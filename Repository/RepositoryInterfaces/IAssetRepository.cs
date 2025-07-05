using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Repository.RepositoryInterfaces
{
    public interface IAssetRepository : IRepository<Asset>
    {
        Task<Asset?> UpdateLatestPrice(PriceInfo priceInfo);
        Task<List<Asset>?> GetByInstrumentIdAsync(string instrumentId);
        Task<List<Asset>?> GetByProvider(string providerName);
        Task<Asset?> GetByProviderAndInstrument(string instrumentId, string providerName);
        Task<List<Asset>?> GetBySymbol(string symbol);
        Task<Asset> Create(string instrumentId, string providerName);

        Task<Asset?> DeleteBySymbol(string symbol);
    }
}

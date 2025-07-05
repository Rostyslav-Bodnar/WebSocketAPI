using WebSocket_API.DTO;

namespace WebSocket_API.Interfaces
{
    public interface IAssetService
    {
        Task<List<SupportedAssetsDTO>> GetSupportedAssetsAsync();
        Task<List<AssetDTO>> GetAssetBySymbolAsync(string symbol, string? providerName = null);
        Task<List<AssetDTO>> GetAssetsPricesAsync(int[] assetIds);

        Task<AssetDTO?> DeleteAssetAsync(string symbol);
        Task<List<AssetDTO>> GetAssetsBySymbolsAsync(string[] symbols, string? providerName = null);
    }
}

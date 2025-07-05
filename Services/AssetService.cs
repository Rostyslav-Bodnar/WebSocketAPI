using WebSocket_API.DTO;
using WebSocket_API.Entities;
using WebSocket_API.Interfaces;
using WebSocket_API.Mappers;
using WebSocket_API.Repository.RepositoryInterfaces;
using WebSocket_API.Services.WebSocket;

namespace WebSocket_API.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository assetRepository;
        private readonly IInstrumentRepository instrumentRepository;
        private readonly IProviderRepository providerRepository;
        private readonly WebSocketSubscriptionService webSocketSubscriptionService;
        private readonly ILogger<AssetService> logger;

        public AssetService(
            IAssetRepository assetRepository,
            IInstrumentRepository instrumentRepository,
            IProviderRepository providerRepository,
            WebSocketSubscriptionService webSocketSubscriptionService,
            ILogger<AssetService> logger)
        {
            this.assetRepository = assetRepository;
            this.instrumentRepository = instrumentRepository;
            this.providerRepository = providerRepository;
            this.webSocketSubscriptionService = webSocketSubscriptionService;
            this.logger = logger;
        }

        public async Task<List<SupportedAssetsDTO>> GetSupportedAssetsAsync()
        {
            logger.LogInformation("Retrieving all supported assets");
            var assets = await assetRepository.GetAllAsync();

            var result = assets.Select(a => new SupportedAssetsDTO
            {
                InstrumentId = a.Instrument?.InstrumentId,
                Provider = a.Provider?.Name,
                Symbol = a.Instrument?.Symbol,
                Price = a.LatestPrice?.Price
            }).ToList();

            logger.LogInformation("Retrieved {Count} supported assets", result.Count);
            return result;
        }

        public async Task<List<AssetDTO>> GetAssetBySymbolAsync(string symbol, string? providerName = null)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                logger.LogError("Symbol is null or empty");
                throw new ArgumentException("Symbol must not be null or empty.", nameof(symbol));
            }

            logger.LogInformation("Retrieving assets by symbol: {Symbol}, provider: {ProviderName}", symbol, providerName ?? "none");

            var assets = await assetRepository.GetBySymbol(symbol);
            if (assets.Any())
            {
                var result = MapToAssetDTOs(assets);
                logger.LogInformation("Found {Count} assets for symbol: {Symbol}", result.Count, symbol);
                return result;
            }

            if (providerName == null)
            {
                logger.LogWarning("No assets found for symbol: {Symbol} and no provider specified", symbol);
                var instrument = await instrumentRepository.GetBySymbol(symbol);
                if(instrument != null)
                    providerName = instrument.InstrumentMappings.Select(im => im.Provider.Name).FirstOrDefault();
                else
                    return new List<AssetDTO>();
            }

            var asset = await SubscribeAndCreateAssetAsync(symbol, providerName);
            if (asset == null)
            {
                logger.LogWarning("Failed to create or retrieve asset for symbol: {Symbol}, provider: {ProviderName}", symbol, providerName);
                return new List<AssetDTO>();
            }

            return new List<AssetDTO> { AssetMapper.ToDto(asset) };
        }

        public async Task<List<AssetDTO>> GetAssetsPricesAsync(int[] assetIds)
        {
            if (assetIds == null || !assetIds.Any())
            {
                logger.LogError("Asset IDs list is null or empty");
                throw new ArgumentException("Asset IDs must not be null or empty.", nameof(assetIds));
            }

            logger.LogInformation("Retrieving prices for assets with IDs: {AssetIds}", string.Join(", ", assetIds));
            var result = new List<AssetDTO>();

            foreach (var assetId in assetIds)
            {
                var asset = await assetRepository.GetByIdAsync(assetId);
                if (asset != null)
                {
                    result.Add(new AssetDTO
                    {
                        InstrumentId = asset.Instrument?.InstrumentId,
                        Provider = asset.Provider?.Name,
                        Symbol = asset.Instrument?.Symbol,
                        Price = asset.LatestPrice?.Price,
                        UpdatedAt = asset.LatestPrice?.UpdatedAt,
                        Change = asset.LatestPrice?.Change
                    });
                }
            }

            logger.LogInformation("Retrieved prices for {Count} assets", result.Count);
            return result;
        }

        private async Task<Asset?> SubscribeAndCreateAssetAsync(string symbol, string providerName)
        {
            var instrument = await instrumentRepository.GetBySymbol(symbol);
            if (instrument == null)
            {
                logger.LogWarning("Instrument with symbol: {Symbol} not found", symbol);
                return null;
            }

            var provider = await providerRepository.GetByName(providerName);
            if (provider == null)
            {
                logger.LogWarning("Provider with name: {ProviderName} not found", providerName);
                return null;
            }

            var dto = new AssetDTO
            {
                InstrumentId = instrument.InstrumentId,
                Provider = provider.Name,
                Symbol = symbol
            };

            await webSocketSubscriptionService.SubscribeAsync(dto);
            var asset = await assetRepository.GetByProviderAndInstrument(dto.InstrumentId, dto.Provider);
            return asset;
        }

        private List<AssetDTO> MapToAssetDTOs(IEnumerable<Asset> assets)
        {
            return assets.Select(a => new AssetDTO
            {
                InstrumentId = a.Instrument?.InstrumentId,
                Provider = a.Provider?.Name,
                Symbol = a.Instrument?.Symbol,
                Price = a.LatestPrice?.Price,
                UpdatedAt = a.LatestPrice?.UpdatedAt,
                Change = a.LatestPrice?.Change
            }).ToList();
        }

        public async Task<AssetDTO?> DeleteAssetAsync(string symbol)
        {
            var entity = await assetRepository.DeleteBySymbol(symbol);

            if (entity == null) return null;

            var result = AssetMapper.ToDto(entity);
            await webSocketSubscriptionService.UnsubscribeAsync(result);

            return result;
        }

        public async Task<List<AssetDTO>> GetAssetsBySymbolsAsync(string[] symbols, string? providerName = null)
        {
            if (symbols == null || symbols.Length == 0)
            {
                logger.LogError("Symbols array is null or empty");
                throw new ArgumentException("Symbols array must not be null or empty.", nameof(symbols));
            }

            logger.LogInformation("Retrieving assets for symbols: {Symbols}, provider: {ProviderName}",
                string.Join(", ", symbols), providerName ?? "none");

            var result = new List<AssetDTO>();

            foreach (var symbol in symbols)
            {
                try
                {
                    var assets = await GetAssetBySymbolAsync(symbol, providerName);
                    result.AddRange(assets);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to retrieve asset for symbol: {Symbol}", symbol);
                }
            }

            return result;
        }

    }
}

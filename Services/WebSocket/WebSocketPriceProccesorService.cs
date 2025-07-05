using WebSocket_API.DTO;
using WebSocket_API.Mappers;
using WebSocket_API.Repository.RepositoryInterfaces;
using WebSocket_API.Response;

namespace WebSocket_API.Services.WebSocket
{
    public class WebSocketPriceProccesorService
    {
        private readonly IPriceInfoRepository priceInfoRepository;
        private readonly IAssetRepository assetRepository;
        private readonly ILogger<WebSocketPriceProccesorService> logger;

        private readonly Dictionary<string, DateTime> lastSaved = new();
        private readonly TimeSpan saveInterval = TimeSpan.FromSeconds(10);

        public WebSocketPriceProccesorService(IPriceInfoRepository priceInfoRepository,
            IAssetRepository assetRepository,
            ILogger<WebSocketPriceProccesorService> logger)
        {
            this.priceInfoRepository = priceInfoRepository;
            this.assetRepository = assetRepository;
            this.logger = logger;
        }

        public async Task UpdateAsync(UpdateResponse update)
        {
            var instrumentId = update.InstrumentId;
            var provider = update.Provider;
            var last = update.Last!;

            if (lastSaved.TryGetValue(instrumentId, out var lastTime))
            {
                if ((DateTime.UtcNow - lastTime) < saveInterval)
                {
                    logger.LogDebug("Skipping save for {instrumentId} due to save interval", instrumentId);
                    return;
                }
            }

            lastSaved[instrumentId] = DateTime.UtcNow;

            var asset = await assetRepository.GetByProviderAndInstrument(instrumentId, provider);
            if (asset == null)
            {
                logger.LogWarning("Asset not found for InstrumentId {instrumentId}", instrumentId);
                return;
            }

            var dto = new PriceInfoDTO
            {
                Price = last.Price,
                UpdatedAt = last.Timestamp,
                Volume = last.Volume,
                Change = last.Change,
                ChangePct = last.ChangePct
            };

            var entity = PriceInfoMapper.ToEntity(dto, asset.Id);

            await priceInfoRepository.AddAsync(entity);
            await assetRepository.UpdateLatestPrice(entity);

            logger.LogInformation("Saved price update for InstrumentId {instrumentId}: {price} at {time}", instrumentId, last.Price, last.Timestamp);
        }
    }
}

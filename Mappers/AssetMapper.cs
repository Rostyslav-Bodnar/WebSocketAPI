using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class AssetMapper
    {
        public static AssetDTO ToDto(Asset asset)
        {
            return new AssetDTO
            {
                InstrumentId = asset.Instrument?.InstrumentId,
                Provider = asset.Provider?.Name,
                Symbol = asset.Instrument?.Symbol,
                Price = asset.LatestPrice?.Price,
                UpdatedAt = asset.LatestPrice?.UpdatedAt,
                Change = asset.LatestPrice?.Change
            };
        }

        public static Asset ToEntity(AssetDTO dto, Instrument instrument, Provider provider, PriceInfo? latestPrice = null)
        {
            return new Asset
            {
                InstrumentId = instrument.Id,
                Instrument = instrument,
                ProviderId = provider.Id,
                Provider = provider,
                LatestPriceId = latestPrice?.Id,
                LatestPrice = latestPrice
            };
        }
    }
}

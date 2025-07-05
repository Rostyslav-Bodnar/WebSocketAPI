using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class PriceInfoMapper
    {
        public static PriceInfoDTO ToDTO(this PriceInfo entity)
        {
            return new PriceInfoDTO
            {
                Price = entity.Price,
                UpdatedAt = entity.UpdatedAt,
                Volume = entity.Volume,
                Change = entity.Change,
                ChangePct = entity.ChangePct
            };
        }

        public static PriceInfo ToEntity(this PriceInfoDTO dto, int assetId)
        {
            return new PriceInfo
            {
                AssetId = assetId,
                Price = dto.Price,
                UpdatedAt = dto.UpdatedAt,
                Volume = dto.Volume,
                Change = dto.Change,
                ChangePct = dto.ChangePct
            };
        }
    }
}

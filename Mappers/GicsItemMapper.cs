using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class GicsItemMapper
    {
        public static GicsItemDTO ToDto(GicsItem gics) => new()
        {
            Id = gics.GicsId,
            ParentId = gics.GicsParentId,
            Name = gics.Name,
            Items = gics.Children?.Select(ToDto).ToList()
        };

        public static GicsItem ToEntity(GicsItemDTO dto) => new()
        {
            GicsId = dto.Id,
            GicsParentId = dto.ParentId,
            Name = dto.Name,
            Children = dto.Items?.Select(ToEntity).ToList()
        };
    }
}

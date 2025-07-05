using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class KindMapper
    {
        public static KindDTO ToDto(Kind kind) => new() { Name = kind.Name };

        public static Kind ToEntity(KindDTO dto) => new() { Name = dto.Name ?? string.Empty };
    }
}

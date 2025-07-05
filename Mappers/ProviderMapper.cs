using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class ProviderMapper
    {
        public static ProviderDTO ToDto(Provider provider) => new() { Name = provider.Name };

        public static Provider ToEntity(ProviderDTO dto) => new() { Name = dto.Name ?? string.Empty };
    }
}

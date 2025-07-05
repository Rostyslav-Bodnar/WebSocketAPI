using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class ExchangeMapper
    {
        public static ExchangeDTO ToDto(Exchange exchange) => new() { Name = exchange.Name };

        public static Exchange ToEntity(ExchangeDTO dto) => new() { Name = dto.Name ?? string.Empty };
    }
}

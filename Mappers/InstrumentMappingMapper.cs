using WebSocket_API.DTO;
using WebSocket_API.Entities;

namespace WebSocket_API.Mappers
{
    public static class InstrumentMappingMapper
    {
        public static InstrumentMappingDTO ToDto(InstrumentMapping mapping) => new()
        {
            Symbol = mapping.Symbol,
            Exchange = mapping.Exchange?.Name,
            DefaultOrderSize = mapping.DefaultOrderSize,
            MaxOrderSize = mapping.MaxOrderSize,
            TradingHours = mapping.TradingHours != null ? TradingHoursMapper.ToDto(mapping.TradingHours) : null
        };

        public static InstrumentMapping ToEntity(InstrumentMappingDTO dto, Provider provider, Exchange exchange) => new()
        {
            Symbol = dto.Symbol,
            DefaultOrderSize = dto.DefaultOrderSize ?? 0,
            MaxOrderSize = dto.MaxOrderSize,
            Provider = provider,
            Exchange = exchange,
            TradingHours = dto.TradingHours != null ? TradingHoursMapper.ToEntity(dto.TradingHours) : null
        };
    }
}

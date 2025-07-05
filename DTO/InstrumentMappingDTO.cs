using WebSocket_API.Entities;

namespace WebSocket_API.DTO
{
    public class InstrumentMappingDTO
    {
        public string Symbol { get; set; } = string.Empty;
        public string? Exchange { get; set; }
        public string? Provider { get; set; }
        public int? DefaultOrderSize { get; set; }
        public int? MaxOrderSize { get; set; }
        public TradingHoursDTO? TradingHours { get; set; }
    }
}

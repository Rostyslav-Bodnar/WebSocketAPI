namespace WebSocket_API.Entities
{
    public class InstrumentMapping
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;

        public int? ProviderId { get; set; }  
        public Provider? Provider{ get; set; }
        public int? ExchangeId { get; set; }
        public Exchange? Exchange { get; set; }
        public int DefaultOrderSize { get; set; }
        public int? MaxOrderSize { get; set; }
        public int? TradingHoursId { get; set; }
        public TradingHours? TradingHours { get; set; }

        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }
    }
}

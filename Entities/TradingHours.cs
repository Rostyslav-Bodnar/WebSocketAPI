namespace WebSocket_API.Entities
{
    public class TradingHours
    {
        public int Id { get; set; }
        public string RegularStart { get; set; } = string.Empty;
        public string RegularEnd { get; set; } = string.Empty;
        public string ElectronicStart { get; set; } = string.Empty;
        public string ElectronicEnd { get; set; } = string.Empty;
        public int? InstrumentMappingId { get; set; }
        public InstrumentMapping? InstrumentMapping { get; set; }
    }
}

namespace WebSocket_API.Entities
{
    public class Instrument
    {
        public int Id { get; set; }
        public string InstrumentId { get; set; }
        public string? Symbol { get; set; }
        public int? KindId { get; set; }
        public Kind? Kind { get; set; }
        public int? ExchangeId { get; set; }
        public Exchange? Exchange { get; set; }
        public string? Description { get; set; }
        public double? tickSize { get; set; }
        public string? Currency { get; set; }
        public ICollection<InstrumentMapping>? InstrumentMappings { get; set; }
        public int InstrumentProfileId { get; set; }
        public InstrumentProfile? InstrumentProfile { get; set; }

    }
}

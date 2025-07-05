namespace WebSocket_API.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int? LatestPriceId { get; set; }
        public PriceInfo? LatestPrice { get; set; }

        public ICollection<PriceInfo> PriceHistory { get; set; } = new List<PriceInfo>();
    }
}

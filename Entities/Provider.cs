namespace WebSocket_API.Entities
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<InstrumentMapping> InstrumentMappings { get; set; } = new List<InstrumentMapping>();
        public ICollection<ProviderExchange> ProviderExchanges { get; set; } = new List<ProviderExchange>();

    }
}

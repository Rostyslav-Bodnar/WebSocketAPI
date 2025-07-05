namespace WebSocket_API.Entities
{
    public class ProviderExchange
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int ExchangeId { get; set; }
        public Exchange Exchange { get; set; }
    }
}

using WebSocket_API.Interfaces;

namespace WebSocket_API.Response
{
    public class PriceInfoResponse : IResponse
    {
        public DateTime Timestamp { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePct { get; set; }
    }
}

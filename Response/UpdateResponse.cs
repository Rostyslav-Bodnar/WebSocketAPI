using WebSocket_API.Interfaces;

namespace WebSocket_API.Response
{
    public class UpdateResponse : IResponse
    {
        public string Type { get; set; } = null!;
        public string InstrumentId { get; set; } = null!;
        public string Provider { get; set; } = null!;
        public PriceInfoResponse? Last { get; set; }
    }
}

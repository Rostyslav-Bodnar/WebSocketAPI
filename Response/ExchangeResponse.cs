using WebSocket_API.DTO;
using WebSocket_API.Interfaces;

namespace WebSocket_API.Response
{
    public class ExchangeResponse : IResponse
    {
        public Dictionary<string, List<string>>? Data { get; set; }
    }
}

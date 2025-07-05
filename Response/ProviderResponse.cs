using WebSocket_API.Interfaces;

namespace WebSocket_API.Response
{
    public class ProviderResponse : IResponse
    {
        public List<string>? Data { get; set; }
    }
}

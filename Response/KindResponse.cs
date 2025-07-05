using WebSocket_API.Interfaces;

namespace WebSocket_API.Response
{
    public class KindResponse : IResponse
    {
        public List<string>? Data { get; set; }
    }
}

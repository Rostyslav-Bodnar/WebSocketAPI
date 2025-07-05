using WebSocket_API.DTO;
using WebSocket_API.Interfaces;

namespace WebSocket_API.Response
{
    public class GicsResponse : IResponse
    {
        public List<GicsItemDTO>? Data { get; set; }
    }
}

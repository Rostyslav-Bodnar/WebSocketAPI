using WebSocket_API.DTO;
using WebSocket_API.Interfaces;

namespace WebSocket_API.Response
{
    public class InstrumentResponse : IResponse
    {
        public List<InstrumentDTO>? Data { get; set; }

    }
}

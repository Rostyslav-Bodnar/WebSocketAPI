using System.Net.WebSockets;

namespace WebSocket_API.Services.WebSocket
{
    public interface IWebSocketConnectionAccessor
    {
        ClientWebSocket Socket { get; }

    }
}

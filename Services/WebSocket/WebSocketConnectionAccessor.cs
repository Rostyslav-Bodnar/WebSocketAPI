using System.Net.WebSockets;

namespace WebSocket_API.Services.WebSocket
{
    public class WebSocketConnectionAccessor : IWebSocketConnectionAccessor
    {
        public ClientWebSocket Socket { get; } = new();
    }

}

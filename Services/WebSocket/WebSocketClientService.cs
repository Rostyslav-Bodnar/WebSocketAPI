using System.Net.WebSockets;
using System.Text;

namespace WebSocket_API.Services.WebSocket
{
    public class WebSocketClientService : BackgroundService
    {
        private readonly ILogger<WebSocketClientService> logger;
        private readonly IWebSocketConnectionAccessor socketAccessor;
        private ClientWebSocket webSocket => socketAccessor.Socket;
        private readonly FintachartsAuthService fintachartsAuthService;
        private readonly IServiceScopeFactory scopeFactory;
        
        public WebSocketClientService(ILogger<WebSocketClientService> logger,
            FintachartsAuthService fintachartsAuthService,
            IServiceScopeFactory scopeFactory,
            IWebSocketConnectionAccessor socketAccessor)
        {
            this.logger = logger;
            this.fintachartsAuthService = fintachartsAuthService;
            this.scopeFactory = scopeFactory; this.socketAccessor = socketAccessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectWebSocketAsync(stoppingToken);

            using var scope = scopeFactory.CreateScope();
            var subscriptionService = scope.ServiceProvider.GetRequiredService<WebSocketSubscriptionService>();

            await subscriptionService.Initialize(); 
            
            _ = ReceiveLoop(stoppingToken);
        }

        private async Task ConnectWebSocketAsync(CancellationToken stoppingToken)
        {
            if (webSocket.State == WebSocketState.Open) return;

            var token = await fintachartsAuthService.GetAccessTokenAsync();
            var uri = new Uri($"wss://platform.fintacharts.com/api/streaming/ws/v1/realtime?token={token}");

            await webSocket.ConnectAsync(uri, stoppingToken);
            logger.LogInformation("WebSocket connected to {uri}", uri);
        }

        private async Task ReceiveLoop(CancellationToken token)
        {
            var buffer = new byte[8192];

            while (!token.IsCancellationRequested && webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(buffer, token);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", token);
                    break;
                }

                var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

                try
                {
                    using var scope = scopeFactory.CreateScope();
                    var messageProcessor = scope.ServiceProvider.GetRequiredService<IMessageProcessorService>();

                    await messageProcessor.ProcessMessageAsync(json);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to process WebSocket message: {json}", json);
                }
            }
        }
    }
}

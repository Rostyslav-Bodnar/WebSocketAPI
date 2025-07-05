using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using WebSocket_API.Repository.RepositoryInterfaces;
using WebSocket_API.DTO;
using WebSocket_API.Entities;
using WebSocket_API.Mappers;

namespace WebSocket_API.Services.WebSocket
{
    public class WebSocketSubscriptionService
    {
        private readonly IWebSocketConnectionAccessor socketAccessor;
        private readonly IAssetRepository assetRepository;

        private ClientWebSocket webSocket => socketAccessor.Socket;

        private readonly HashSet<(string instrumentId, string provider)> activeSubscriptions = new();

        public WebSocketSubscriptionService(IWebSocketConnectionAccessor socketAccessor, IAssetRepository assetRepository)
        {
            this.socketAccessor = socketAccessor;
            this.assetRepository = assetRepository;
        }

        public async Task Initialize()
        {
            var assets = await assetRepository.GetAllAsync();
            foreach(var asset in assets)
            {
                var dto = AssetMapper.ToDto(asset);
                if (String.IsNullOrEmpty(dto.InstrumentId)) throw new Exception("dto");
                await SubscribeAsync(dto);
            }
        }

        public async Task SubscribeAsync(AssetDTO dto)
        {
            var key = (dto.InstrumentId, dto.Provider);
            if (activeSubscriptions.Contains(key)) return;

            var asset = await assetRepository.GetByProviderAndInstrument(dto.InstrumentId, dto.Provider);

            if (asset == null)
                asset = await assetRepository.Create(dto.InstrumentId, dto.Provider);

            activeSubscriptions.Add(key);

            var payload = new
            {
                type = "l1-subscription",
                id = Guid.NewGuid().ToString(),
                instrumentId = dto.InstrumentId,
                provider = dto.Provider,
                subscribe = true,
                kinds = new[] { "ask", "bid", "last" }
            };

            var message = JsonSerializer.Serialize(payload);
            var bytes = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task UnsubscribeAsync(AssetDTO dto)
        {
            var asset = await assetRepository.GetByProviderAndInstrument(dto.InstrumentId, dto.Provider);

            if (asset == null)
                return;

            var key = (dto.InstrumentId, dto.Provider);
            if (!activeSubscriptions.Contains(key)) return;
            activeSubscriptions.Remove(key);

            var payload = new
            {
                type = "l1-subscription",
                id = Guid.NewGuid().ToString(),
                dto.InstrumentId,
                dto.Provider,
                subscribe = false,
                kinds = new[] { "ask", "bid", "last" }
            };
            var message = JsonSerializer.Serialize(payload);
            var bytes = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);

            await assetRepository.DeleteAsync(asset.Id);
        
        }
    }

}

using System.Text.Json;
using WebSocket_API.Response;

namespace WebSocket_API.Services.WebSocket
{
    public class WebSocketMessageProcessorService : IMessageProcessorService
    {
        private readonly ILogger<WebSocketMessageProcessorService> logger;
        private readonly WebSocketPriceProccesorService priceInfoService;

        public WebSocketMessageProcessorService(ILogger<WebSocketMessageProcessorService> logger, WebSocketPriceProccesorService priceInfoService)
        {
            this.logger = logger;
            this.priceInfoService = priceInfoService;
        }

        public async Task ProcessMessageAsync(string json)
        {

            var update = JsonSerializer.Deserialize<UpdateResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (update == null)
            {
                logger.LogWarning("Received unknown message: {json}", json);
                return;
            }

            if (update.Type == "l1-update" && update.Last != null)
            {
                await priceInfoService.UpdateAsync(update);
            }
            else
            {
                logger.LogInformation("Received message of type {type} which is not handled", update.Type);
            }
        }
    }
}

using WebSocket_API.DTO;
using WebSocket_API.Response;

namespace WebSocket_API.Services
{
    public class FintachartsGicsService
    {
        private readonly ClientService clientService;
        private const string endpoint = "https://platform.fintacharts.com/api/instruments/v1/gics";

        public FintachartsGicsService(ClientService clientService)
        {
            this.clientService = clientService;
        }

        public async Task<List<GicsItemDTO>> GetGicsItemsAsync()
        {
            var result = await clientService.GetResponse<GicsResponse>(endpoint);
            if (result?.Data == null) return new List<GicsItemDTO>();

            return result.Data.ToList();
        }
    }
}

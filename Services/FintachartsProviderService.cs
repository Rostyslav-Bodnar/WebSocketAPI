using WebSocket_API.DTO;
using WebSocket_API.Response;

namespace WebSocket_API.Services
{
    public class FintachartsProviderService
    {
        private readonly ClientService clientService;

        private const string endpoint = "https://platform.fintacharts.com/api/instruments/v1/providers";

        public FintachartsProviderService(ClientService clientService)
        {
            this.clientService = clientService;
        }

        public async Task<List<ProviderDTO>> GetProvidersAsync()
        {
            var result = await clientService.GetResponse<ProviderResponse>(endpoint);

            if (result?.Data == null) return new List<ProviderDTO>();

            return result.Data
                     .Select(name => new ProviderDTO { Name = name })
                     .ToList();
        }
    }
}

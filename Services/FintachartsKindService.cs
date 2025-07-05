using WebSocket_API.DTO;
using WebSocket_API.Response;

namespace WebSocket_API.Services
{
    public class FintachartsKindService
    {
        private readonly ClientService clientService;
        private const string endpoint = "https://platform.fintacharts.com/api/instruments/v1/kinds";

        public FintachartsKindService(ClientService clientService)
        {
            this.clientService = clientService;
        }

        public async Task<List<KindDTO>> GetKinds()
        {
            var result = await clientService.GetResponse<KindResponse>(endpoint);

            if (result?.Data == null) return new List<KindDTO>();

            return result.Data
                     .Select(name => new KindDTO { Name = name })
                     .ToList();

        }
    }
}

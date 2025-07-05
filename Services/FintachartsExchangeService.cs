using WebSocket_API.DTO;
using WebSocket_API.Response;

namespace WebSocket_API.Services
{
    public class FintachartsExchangeService
    {
        private readonly ClientService clientService;

        public FintachartsExchangeService(ClientService clientService)
        {
            this.clientService = clientService;
        }

        private const string endpoint = "https://platform.fintacharts.com/api/instruments/v1/exchanges";

        public async Task<List<ExchangeDTO>> GetExchangeAsync()
        {
            var result = await clientService.GetResponse<ExchangeResponse>(endpoint);
            if (result?.Data == null) return new();

            var exchangeDtos = new List<ExchangeDTO>();

            foreach (var kvp in result.Data)
            {
                var provider = kvp.Key;
                var exchanges = kvp.Value;

                foreach (var exchange in exchanges.Where(e => !string.IsNullOrWhiteSpace(e)))
                {
                    exchangeDtos.Add(new ExchangeDTO
                    {
                        ProviderName = provider,
                        Name = exchange
                    });
                }
            }

            return exchangeDtos;
        }
    }
}

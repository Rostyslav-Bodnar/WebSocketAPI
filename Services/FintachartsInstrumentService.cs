using System.Text.Json;
using WebSocket_API.DTO;
using WebSocket_API.Mappers;
using WebSocket_API.Response;

namespace WebSocket_API.Services
{
    public class FintachartsInstrumentService
    {
        private readonly ClientService clientService;
        private const string endpoint = "https://platform.fintacharts.com/api/instruments/v1/instruments";

        public FintachartsInstrumentService(ClientService clientService)
        {
            this.clientService = clientService;
        }

        public async Task<List<InstrumentDTO>> GetInstrumentsAsync()
        {

            var json = await clientService.GetRawJson(endpoint);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<InstrumentResponse>(json, options);
            if (result?.Data == null) return new List<InstrumentDTO>();

            return result.Data.ToList();

        }
    }
}

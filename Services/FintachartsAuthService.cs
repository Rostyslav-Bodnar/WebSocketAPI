using System.Text.Json;

namespace WebSocket_API.Services
{
    public class FintachartsAuthService
    {
        private readonly HttpClient client;
        private readonly IConfiguration configuration;

        private const string endpoint = "https://platform.fintacharts.com/identity/realms/fintatech/protocol/openid-connect/token";
        public FintachartsAuthService(HttpClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["client_id"] = "app-cli",
                ["username"] = configuration["Fintacharts:Username"],
                ["password"] = configuration["Fintacharts:Password"]
            });

            var response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            return json.GetProperty("access_token").ToString(); // GETSTRING()
        }
    }
}

using System.Net.Http.Headers;
using WebSocket_API.Interfaces;

namespace WebSocket_API.Services
{
    public class ClientService
    {
        private readonly HttpClient client;
        private readonly FintachartsAuthService authService;

        public ClientService(HttpClient client, FintachartsAuthService authService)
        {
            this.client = client;
            this.authService = authService;
        }

        public async Task<T> GetResponse<T>(string endPoint) where T : IResponse
        {
            var token = await authService.GetAccessTokenAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(endPoint);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<T>();

            return result;
        }

        public async Task<string> GetRawJson(string url)
        {
            var token = await authService.GetAccessTokenAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }



    }
}

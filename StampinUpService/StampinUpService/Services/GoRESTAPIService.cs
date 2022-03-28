using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using StampinUp.Service.Models;

namespace StampinUp.Service.Services
{
    public class GoRESTApiService : IGoRESTApiService
    {
        private readonly HttpClient client;

        public GoRESTApiService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("GoRESTApi");
        }

        public async Task<List<GoRESTApiUserInfo>> GetUsers()
        {
            var url = "/public/v2/users";
            var result = new List<GoRESTApiUserInfo>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<List<GoRESTApiUserInfo>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}

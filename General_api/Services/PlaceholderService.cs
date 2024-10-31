using General_api.DTOs;
using System.Text.Json;

namespace General_api.Services
{
    public class PlaceholderService : IPlaceholderService
    {
        private readonly HttpClient _httpClient;

        public PlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PlaceholderDto>> Get()
        {
            var url = _httpClient.BaseAddress;

            var result = await _httpClient.GetAsync(url);
            var body = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var placeholder = JsonSerializer.Deserialize<IEnumerable<PlaceholderDto>>(body, options);

            return placeholder;
        }
    }
}

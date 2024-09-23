using PTTTest01.DataAccess;
using System.Text.Json;

namespace PTTTest01.Helpers
{
    public class AvatarJsonHelper(IHttpClientFactory httpClientFactory, ILogger<AvatarRepository> logger) : IAvatarJsonHelper
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
        private readonly ILogger<AvatarRepository> _logger = logger;

        public async Task<string?> FetchAndExtractProperty(string jsonUrl, string propertyName)
        {
            try
            {
                var jsonResponse = await _httpClient.GetStringAsync(jsonUrl);

                var jsonDoc = JsonDocument.Parse(jsonResponse);
                if (jsonDoc.RootElement.TryGetProperty(propertyName, out var propertyValue))
                {
                    return propertyValue.GetString();
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, message: "HTTP error occurred while retrieving image from JSON URL: {jsonUrl}", jsonUrl);
                throw;
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, message: "JSON parsing error occurred while retrieving image from JSON URL: {jsonUrl}", jsonUrl);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "Unexpected error occurred while retrieving image URL from JSON URL: {jsonUrl}", jsonUrl);
                throw;
            }

            _logger.LogError("Image URL can not be parsed from JSON URL: {jsonUrl}.", jsonUrl);
            return null;
        }
    }
}


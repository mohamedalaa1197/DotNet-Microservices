using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong when calling API: {response.ReasonPhrase}");

            var dataString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataString,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
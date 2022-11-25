using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vandebron.Verbruik.Console.App.Api.Usage;

namespace Vandebron.Verbruik.Console.App
{
    public class VerbruikDownloader : IDisposable
    {
        public VerbruikDownloader()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }


        private const string BaseUrl = @"https://mijn.vandebron.nl/";
        private readonly HttpClient _httpClient;



        /// <summary>
        /// Downloads the usage data for the specified consumer and connection
        /// </summary>
        /// <remarks>Yields data in descending order</remarks>
        /// <param name="consumerId">consumer id (guid)</param>
        /// <param name="connectionId">connection id (guid)</param>
        /// <returns></returns>
        public async IAsyncEnumerable<Entry> Download(string consumerId, string connectionId, string token)
        {
            var now = DateTime.Now;
            var date = new DateTime(now.Year, now.Month, 1);
            Result result;
            do
            {
                var start = date;
                var end = date.AddMonths(1).AddDays(-1);
                var uri =
                    $"/api/consumers/{consumerId}/connections/{connectionId}/usage?resolution=Days&startDate={start:yyyy-MM-dd}&endDate={end:yyyy-MM-dd}";
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                request.Headers.Add("authorization", $"Bearer {token}");
                var response = await _httpClient.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                result = JsonSerializer.Deserialize<Result>(json, options);
                foreach (var item in result.Values)
                {
                    yield return item;
                }

                date = date.AddMonths(-1);
            } while (result != null && result.Values.Any());
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}

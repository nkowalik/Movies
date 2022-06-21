using Movies.Api.Models;
using Newtonsoft.Json;

namespace Movies.Api.DataCollectors
{
    /// <summary>
    /// Collector of the movies data
    /// </summary>
    public class MoviesDataCollector : IMoviesDataCollector
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// A constructor for MoviesDataCollector
        /// </summary>
        /// <param name="config">Configuration containing access key value</param>
        /// <param name="httpClientFactory">Factory for http client</param>
        public MoviesDataCollector(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Collects movies data from OmDb based on its title
        /// </summary>
        /// <returns>OmDbMovieDto</returns>
        public async Task<OmDbMovieDto?> FetchMovieDataFromOmDbAsync(string title)
        {
            var apiKey = _config.GetValue<string>("Movies:ApiKey");
            using var httpClient = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"http://www.omdbapi.com/?api_key={apiKey}&t={title}");

            var response = await httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<OmDbMovieDto>(apiResponse);
            }

            return null;
        }

        /// <summary>
        /// Collects movies data from FakeDb based on its title. In FakeDb only 21 movies exist.
        /// </summary>
        /// <returns>FakeDbMovieDto</returns>
        public async Task<FakeDbMovieDto?> FetchMovieDataFromFakeDbAsync(string title)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://fake-movie-database-api.herokuapp.com/api?s={title}");

            var response = await httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<FakeDbMovieDto>(apiResponse);
            }

            return null;
        }
    }
}

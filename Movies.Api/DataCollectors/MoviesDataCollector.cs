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
        private readonly ILogger<MoviesDataCollector> _logger;

        /// <summary>
        /// A constructor for MoviesDataCollector
        /// </summary>
        /// <param name="config">Configuration containing access key value</param>
        /// <param name="httpClientFactory">Factory for http client</param>
        public MoviesDataCollector(IConfiguration config, IHttpClientFactory httpClientFactory,
            ILogger<MoviesDataCollector> logger)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Collects movies data from OmDb based on its title
        /// </summary>
        /// <param name="title">A movie title</param>
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
                _logger.LogInformation("Movie {title} data from OmDb was fetched successfully.", title);

                string apiResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<OmDbMovieDto>(apiResponse);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _logger.LogError("Movie {title} data from OmDb was not fetched due to invalid api key.", title);
            }
            else
            {
                _logger.LogError("Movie {title} data from OmDb was not fetched due to status code: {statusCode}.", 
                    title, response.StatusCode);
            }

            return null;
        }

        /// <summary>
        /// Collects movies data from FakeDb based on its title. In FakeDb only 21 movies exist.
        /// </summary>
        /// <param name="title">A movie title</param>
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
                _logger.LogInformation("Movie {title} data from FakeDb was fetched successfully.", title);

                string apiResponse = await response.Content.ReadAsStringAsync();

                if (apiResponse != null && apiResponse.Any())
                {
                    return JsonConvert.DeserializeObject<FakeDbMovieDto>(apiResponse);
                }
            }

            _logger.LogError("Movie {title} data from FakeDb was not fetched due to status code: {statusCode}.",
                    title, response.StatusCode);

            return null;
        }
    }
}

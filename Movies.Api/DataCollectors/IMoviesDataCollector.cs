using Movies.Api.Models;

namespace Movies.Api.DataCollectors
{
    public interface IMoviesDataCollector
    {
        Task<OmDbMovieDto?> FetchMovieDataFromOmDbAsync(string title);
        Task<FakeDbMovieDto?> FetchMovieDataFromFakeDbAsync(string title);
    }
}

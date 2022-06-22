using Movies.Api.Models;

namespace Movies.Api.DataCollectors
{
    /// <summary>
    /// An interface for movies data collector
    /// </summary>
    public interface IMoviesDataCollector
    {
        /// <summary>
        /// Collects movies data from OmDb based on its title
        /// </summary>
        /// <param name="title">A movie title</param>
        /// <returns>OmDbMovieDto</returns>
        Task<OmDbMovieDto?> FetchMovieDataFromOmDbAsync(string title);

        /// <summary>
        /// Collects movies data from FakeDb based on its title. In FakeDb only 21 movies exist.
        /// </summary>
        /// <param name="title">A movie title</param>
        /// <returns>FakeDbMovieDto</returns>
        Task<FakeDbMovieDto?> FetchMovieDataFromFakeDbAsync(string title);
    }
}

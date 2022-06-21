using Movies.Api.Entities;

namespace Movies.Api.Repositories
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<MovieEntity>> GetMoviesAsync(string source);
        Task<MovieEntity?> GetMovieAsync(string source, string name);
    }
}

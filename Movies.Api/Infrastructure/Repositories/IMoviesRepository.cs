using Movies.Api.Infrastructure.DbContexts;

namespace Movies.Api.Infrastructure.Repositories
{
    public interface IMoviesRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetMoviesAsync();
        Task<T?> GetMovieByTitleAsync(string title);
        Task<T?> GetMovieByIdAsync(int id);
        Task DeleteMovie(T movie);
        Task<bool> MovieExistsAsync(int id);
        Task<bool> SaveChangesAsync(MoviesContext context);
    }
}

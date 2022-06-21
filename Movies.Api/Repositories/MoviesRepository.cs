using Microsoft.EntityFrameworkCore;
using Movies.Api.DbContexts;
using Movies.Api.Entities;

namespace Movies.Api.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ILogger<MoviesRepository> _logger;
        private readonly AMoviesContext _aContext;
        private readonly BMoviesContext _bContext;

        public MoviesRepository(ILogger<MoviesRepository> logger, AMoviesContext aContext, BMoviesContext bContext)
        {
            _logger = logger;
            _aContext = aContext;
            _bContext = bContext;
        }

        public async Task<IEnumerable<MovieEntity>> GetMoviesAsync(string source)
        {
            switch (source.ToUpper())
            {
                case "A":
                    return _aContext.Movies.ToList();
                case "B":
                    return _bContext.Movies.ToList();
                default:
                    _logger.LogError($"Invalid source {source}. Supported sources: A, B.");
                    break;
            }
            return null;
        }

        public async Task<MovieEntity?> GetMovieAsync(string source, string name)
        {
            switch (source.ToUpper())
            {
                case "A":
                    return await _aContext.Movies.Where(m => m.Name == name).FirstOrDefaultAsync();
                case "B":
                    return await _bContext.Movies.Where(m => m.Name == name).FirstOrDefaultAsync();
                default:
                    _logger.LogError($"Invalid source {source}. Supported sources: A, B.");
                    break;
            }
            return null;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Api.DataCollectors;
using Movies.Api.Infrastructure.DbContexts;
using Movies.Api.Infrastructure.Entities;

namespace Movies.Api.Infrastructure.Repositories
{
    public class OmDbMoviesRepository : IMoviesRepository<OmDbMovieEntity>
    {
        private readonly IMoviesDataCollector _collector;
        private readonly IMapper _mapper;

        private readonly MoviesContext _context;

        public OmDbMoviesRepository(IMoviesDataCollector collector, IMapper mapper,
            MoviesContext context)
        {
            _collector = collector;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<OmDbMovieEntity>> GetMoviesAsync()
        {
            MovieContextSeeder.SeedOmDb(_context);
            return _context.MoviesFromOmDb;
        }

        public async Task<OmDbMovieEntity?> GetMovieByTitleAsync(string title)
        {
            var movie = await _collector.FetchMovieDataFromOmDbAsync(title);
            return _mapper.Map<OmDbMovieEntity>(movie);
        }

        public async Task<OmDbMovieEntity?> GetMovieByIdAsync(int id)
        {
            return await _context.MoviesFromOmDb.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> MovieExistsAsync(int id)
        {
            var movie = await _context.MoviesFromOmDb.FirstOrDefaultAsync(g => g.Id == id);
            return movie != null;
        }

        public async Task<bool> SaveChangesAsync(MoviesContext context)
        {
            return (await context.SaveChangesAsync() >= 0);
        }

        public async Task DeleteMovie(OmDbMovieEntity movie)
        {
            _context.MoviesFromOmDb.Remove(movie);
            await SaveChangesAsync(_context);
        }
    }
}

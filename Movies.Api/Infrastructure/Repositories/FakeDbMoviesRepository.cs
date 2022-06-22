using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Api.DataCollectors;
using Movies.Api.Infrastructure.DbContexts;
using Movies.Api.Infrastructure.Entities;

namespace Movies.Api.Infrastructure.Repositories
{
    public class FakeDbMoviesRepository : IMoviesRepository<FakeDbMovieEntity>
    {
        private readonly IMoviesDataCollector _collector;
        private readonly IMapper _mapper;

        private readonly MoviesContext _context;

        public FakeDbMoviesRepository(IMoviesDataCollector collector, IMapper mapper,
            MoviesContext context)
        {
            _collector = collector;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<FakeDbMovieEntity>> GetMoviesAsync()
        {
            MovieContextSeeder.SeedFakeDb(_context);
            return _context.MoviesFromFakeDb;
        }

        public async Task<FakeDbMovieEntity?> GetMovieByTitleAsync(string title)
        {
            var movies = await _collector.FetchMovieDataFromFakeDbAsync(title);
            if (movies == null)
            {
                return null;
            }
            return _mapper.Map<FakeDbMovieEntity>(movies);
        }

        public async Task<FakeDbMovieEntity?> GetMovieByIdAsync(int id)
        {
            return await _context.MoviesFromFakeDb.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> MovieExistsAsync(int id)
        {
            var movie = await _context.MoviesFromFakeDb.FirstOrDefaultAsync(g => g.Id == id);
            return movie != null;
        }

        public async Task<bool> SaveChangesAsync(MoviesContext context)
        {
            return (await context.SaveChangesAsync() >= 0);
        }

        public async Task DeleteMovie(FakeDbMovieEntity movie)
        {
            _context.MoviesFromFakeDb.Remove(movie);
            await SaveChangesAsync(_context);
        }
    }
}

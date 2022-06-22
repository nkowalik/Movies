using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Rest.TransientFaultHandling;
using Movies.Api.ConnectionHandlers;
using Movies.Api.DataCollectors;
using Movies.Api.Infrastructure.DbContexts;
using Movies.Api.Infrastructure.Entities;
using System.Diagnostics;

namespace Movies.Api.Infrastructure.Repositories
{
    public class FakeDbMoviesRepository : IMoviesRepository<FakeDbMovieEntity>
    {
        private readonly IMoviesDataCollector _collector;
        private readonly IMapper _mapper;

        private readonly MoviesContext _context;
        private readonly RetryPolicy _retryPolicy;

        public FakeDbMoviesRepository(IMoviesDataCollector collector, IMapper mapper,
            MoviesContext context)
        {
            _collector = collector;
            _mapper = mapper;
            _context = context;

            _retryPolicy = new RetryPolicy<MoviesTransientErrorDetectionStrategy>
                (new IncrementalRetryStrategy(5, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1.5))
                {
                    FastFirstRetry = true
                });

            _retryPolicy.Retrying += (s, e) =>
            Trace.TraceWarning("An error occurred in attempt number {1} to create geolocation: {0}",
            e.LastException.Message, e.CurrentRetryCount);
        }

        public async Task<IEnumerable<FakeDbMovieEntity>> GetMoviesAsync()
        {
            MovieContextSeeder.SeedFakeDb(_context);
            return _context.MoviesFromFakeDb;
        }

        public async Task<FakeDbMovieEntity?> GetMovieByTitleAsync(string title)
        {
            var movieDto = await _retryPolicy.ExecuteAction(() => 
                _collector.FetchMovieDataFromFakeDbAsync(title));
            if (movieDto == null)
            {
                return null;
            }
            var movieEntity = _mapper.Map<FakeDbMovieEntity>(movieDto);

            _context.MoviesFromFakeDb.Add(movieEntity);
            await SaveChangesAsync(_context);

            return movieEntity;
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

        public async Task DeleteMovieAsync(FakeDbMovieEntity movie)
        {
            _context.MoviesFromFakeDb.Remove(movie);
            await SaveChangesAsync(_context);
        }
    }
}

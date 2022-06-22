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
    public class OmDbMoviesRepository : IMoviesRepository<OmDbMovieEntity>
    {
        private readonly IMoviesDataCollector _collector;
        private readonly IMapper _mapper;

        private readonly MoviesContext _context;
        private readonly RetryPolicy _retryPolicy;

        public OmDbMoviesRepository(IMoviesDataCollector collector, IMapper mapper,
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

        public async Task<IEnumerable<OmDbMovieEntity>> GetMoviesAsync()
        {
            MovieContextSeeder.SeedOmDb(_context);
            return _context.MoviesFromOmDb;
        }

        public async Task<OmDbMovieEntity?> GetMovieByTitleAsync(string title)
        {
            var movieDto = await _retryPolicy.ExecuteAction(() => 
                _collector.FetchMovieDataFromOmDbAsync(title));
            var movieEntity = _mapper.Map<OmDbMovieEntity>(movieDto);

            _context.MoviesFromOmDb.Add(movieEntity);
            await SaveChangesAsync(_context);

            return movieEntity;
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

        public async Task DeleteMovieAsync(OmDbMovieEntity movie)
        {
            _context.MoviesFromOmDb.Remove(movie);
            await SaveChangesAsync(_context);
        }
    }
}

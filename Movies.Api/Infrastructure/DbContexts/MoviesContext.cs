using Microsoft.EntityFrameworkCore;

namespace Movies.Api.Infrastructure.DbContexts
{
    /// <summary>
    /// A common db context for all sources
    /// </summary>
    public class MoviesContext : DbContext
    {
        public DbSet<Entities.OmDbMovieEntity> MoviesFromOmDb { get; set; }
        public DbSet<Entities.FakeDbMovieEntity> MoviesFromFakeDb { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {}
    }
}

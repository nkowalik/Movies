using Microsoft.EntityFrameworkCore;

namespace Movies.Api.Infrastructure.DbContexts
{
    /// <summary>
    /// A common db context for all sources
    /// </summary>
    public class MoviesContext : DbContext
    {
        /// <summary>
        /// Set of movies from OmDb source
        /// </summary>
        public DbSet<Entities.OmDbMovieEntity> MoviesFromOmDb { get; set; }

        /// <summary>
        /// Set of movies from FakeDb source
        /// </summary>
        public DbSet<Entities.FakeDbMovieEntity> MoviesFromFakeDb { get; set; }

        /// <summary>
        /// A constructor for MoviesContext
        /// </summary>
        /// <param name="options">DbContextOptions</param>
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {}
    }
}

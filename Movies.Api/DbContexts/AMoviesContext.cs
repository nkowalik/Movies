using Microsoft.EntityFrameworkCore;

namespace Movies.Api.DbContexts
{
    public class AMoviesContext : DbContext
    {
        public DbSet<Entities.MovieEntity> Movies { get; set; } = null!;

        public AMoviesContext(DbContextOptions<AMoviesContext> options) : base(options)
        { }
    }
}

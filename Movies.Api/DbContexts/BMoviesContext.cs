using Microsoft.EntityFrameworkCore;

namespace Movies.Api.DbContexts
{
    public class BMoviesContext : DbContext
    {
        public DbSet<Entities.MovieEntity> Movies { get; set; } = null!;

        public BMoviesContext(DbContextOptions<AMoviesContext> options) : base(options)
        { }
    }
}

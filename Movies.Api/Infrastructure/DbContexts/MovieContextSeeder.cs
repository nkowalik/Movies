namespace Movies.Api.Infrastructure.DbContexts
{
    public static class MovieContextSeeder
    {
        public static void SeedOmDb(MoviesContext context)
        {
            if (context.MoviesFromOmDb.Any(m => m.Title == "The Room"))
            {
                return;
            }

            context.MoviesFromOmDb.Add(new Entities.OmDbMovieEntity("The Room")
            {
                Year = "2003",
                Genre = "Drama",
                Director = "Tommy Wiseau",
                Plot = "Johnny is a successful bank executive who lives quietly in a San Francisco " +
                "townhouse with his fiancée, Lisa. One day, putting aside any scruple, " +
                "she seduces Johnny's best friend, Mark. From there, nothing will be the same again."
            });

            Task.WaitAll();
            context.SaveChanges();
        }

        public static void SeedFakeDb(MoviesContext context)
        {
            const int testMovieId = 123;
            if (context.MoviesFromFakeDb.Any(m => m.Id == testMovieId))
            {
                return;
            }

            var fakeDbMovieEntity = new Entities.FakeDbMovieEntity()
            {
                Id = testMovieId,
                MovieDetails = new List<Entities.FakeDbMovieDetailsEntity>
                {
                    new Entities.FakeDbMovieDetailsEntity
                    {
                        Title = "Game of Thrones",
                        Year = "2011-2019",
                        Poster = "some url",
                        FakeDbMovieId = testMovieId
                    }
                }
            };

            context.MoviesFromFakeDb.Add(fakeDbMovieEntity);

            Task.WaitAll();
            context.SaveChanges();
        }
    }
}

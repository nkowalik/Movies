namespace Movies.Api.Models
{
    /// <summary>
    /// A model for a movie from FakeDb repository
    /// </summary>
    public class FakeDbMovieDto
    {
        /// <summary>
        /// Movie id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A list of movies that are found in FakeDb repository by title
        /// </summary>
        public IEnumerable<FakeDbMovieDetailsDto> Search { get; set; }
    }
}

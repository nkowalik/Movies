namespace Movies.Api.Models
{
    /// <summary>
    /// A model for movie details that are found in FakeDb repository
    /// </summary>
    public class FakeDbMovieDetailsDto
    {
        public int Id { get; set; }
        public string? ImdbID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
    }
}

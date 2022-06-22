namespace Movies.Api.Models
{
    /// <summary>
    /// A model for a movie from OmDb repository
    /// </summary>
    public class OmDbMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; 
        public string Year { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string Plot { get; set; } = string.Empty;
    }    
}

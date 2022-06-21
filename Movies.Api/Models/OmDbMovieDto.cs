namespace Movies.Api.Models
{
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

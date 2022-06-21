namespace Movies.Api.Models
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string? Description { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Movies.Api.Models
{
    public class FakeDbMovieDto
    {
        public int Id { get; set; }

        [JsonPropertyName("Search")]
        public IEnumerable<FakeDbMovieDetailsDto> MovieDetails { get; set; }
    }
}

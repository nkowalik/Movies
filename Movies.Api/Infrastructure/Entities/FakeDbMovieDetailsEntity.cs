using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Api.Infrastructure.Entities
{
    public class FakeDbMovieDetailsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? ImdbID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(10)]
        public string Year { get; set; }

        [Required]
        public string Poster { get; set; }

        [ForeignKey("FakeDbMovieId")]
        public int FakeDbMovieId { get; set; }
    }
}

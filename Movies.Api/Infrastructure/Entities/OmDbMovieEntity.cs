using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Infrastructure.Entities
{
    /// <summary>
    /// The entity for a movie from OMDb data source
    /// </summary>
    public class OmDbMovieEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(10)]
        public string Year { get; set; }

        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Director { get; set; }

        [Required]
        public string Plot { get; set; }

        public OmDbMovieEntity(string title)
        {
            Title = title;
        }
    }
}

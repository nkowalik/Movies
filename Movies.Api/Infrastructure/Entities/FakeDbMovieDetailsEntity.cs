using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Api.Infrastructure.Entities
{
    /// <summary>
    /// Entity for a movie details with FakeDb structure
    /// </summary>
    public class FakeDbMovieDetailsEntity
    {
        /// <summary>
        /// An entity id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// ImDb id
        /// </summary>
        public string? ImdbID { get; set; }

        /// <summary>
        /// A movie title
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// A year of the movie production
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Year { get; set; }

        /// <summary>
        /// A link to the movie poster
        /// </summary>
        [Required]
        public string Poster { get; set; }

        /// <summary>
        /// A foreign key to FakeDbMovieEntity
        /// </summary>
        [ForeignKey("FakeDbMovieId")]
        public int FakeDbMovieId { get; set; }
    }
}

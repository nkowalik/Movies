using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Infrastructure.Entities
{
    /// <summary>
    /// The entity for a movie from OMDb data source
    /// </summary>
    public class OmDbMovieEntity
    {
        /// <summary>
        /// An entity id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        /// A movie genre
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }

        /// <summary>
        /// A movie director
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Director { get; set; }

        /// <summary>
        /// A movie plot
        /// </summary>
        [Required]
        public string Plot { get; set; }

        /// <summary>
        /// Constructor for OmDbMovieEntity with required title
        /// </summary>
        /// <param name="title">A movie title</param>
        public OmDbMovieEntity(string title)
        {
            Title = title;
        }
    }
}

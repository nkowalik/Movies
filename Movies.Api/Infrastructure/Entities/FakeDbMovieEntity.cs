using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Infrastructure.Entities
{
    /// <summary>
    /// The entity for a movie from FakeDb data source
    /// </summary>
    public class FakeDbMovieEntity
    {
        /// <summary>
        /// An entity id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// A list of movies that are found in FakeDb repository by title
        /// </summary>
        public IEnumerable<FakeDbMovieDetailsEntity> Search { get; set; }
    }
}

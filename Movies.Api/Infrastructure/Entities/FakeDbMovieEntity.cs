using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Infrastructure.Entities
{
    /// <summary>
    /// The entity for a movie from FakeDb data source
    /// </summary>
    public class FakeDbMovieEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public IEnumerable<FakeDbMovieDetailsEntity> MovieDetails { get; set; }
    }
}

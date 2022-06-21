using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Entities
{
    public class MovieEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public MovieEntity(string name)
        {
            Name = name;
        }
    }
}

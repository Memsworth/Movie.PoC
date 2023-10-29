using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Film : BaseEntity
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}

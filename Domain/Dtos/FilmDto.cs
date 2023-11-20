using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class FilmDto
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
    }
}

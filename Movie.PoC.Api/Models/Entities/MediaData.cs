using Movie.PoC.Api.Models.Enums;

namespace Movie.PoC.Api.Models.Entities
{
    public class MediaData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ContentRating Rated { get; set; }
        public DateOnly Released { get; set; }
        public TimeSpan Runtime { get; set; }
        public Genre Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public Language Language { get; set; }
        public Country Country { get; set; }
        public string Poster { get; set; }
        public int Metascore { get; set; }
        public double imdbRating { get; set; }
        public int imdbVotes { get; set; }
        public string imdbID { get; set; }
        public ContentType Type { get; set; }
        public Film AssociatedFilm { get; set; }
    }
}

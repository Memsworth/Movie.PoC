namespace Movie.PoC.Api.Models.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public int ReviewScore { get; set; }
        public string ReviewContent { get; set; }

        public Guid FilmId { get; set; }
        public Film AssociatedFilm { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}

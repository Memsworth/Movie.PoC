namespace Movie.PoC.Api.Entities
{
    public class ReviewModel
    {
        public Guid Id { get; set; }
        public int ReviewScore { get; set; }
        public string ReviewContent { get; set; }

        public Guid FilmId { get; set; }
        public FilmModel AssociatedFilm { get; set; }

        public Guid UserId { get; set; }
        public UserModel User { get; set; }
    }
}

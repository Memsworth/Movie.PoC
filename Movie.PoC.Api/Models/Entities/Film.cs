namespace Movie.PoC.Api.Models.Entities
{
    public class Film
    {
        public Guid Id { get; set; }
        public bool IsDisabled { get; set; }

        public Guid FilmDataId { get; set; }
        public virtual FilmData AssociatedFilmData { get; set; }
    }
}

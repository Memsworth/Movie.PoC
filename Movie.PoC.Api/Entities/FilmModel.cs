namespace Movie.PoC.Api.Entities
{
    public class FilmModel
    {
        public Guid Id { get; set; }
        public bool IsDisabled { get; set; }
        public FilmDataModel AssociatedFilmData { get; set; }
    }
}

namespace Movie.PoC.Api.Contracts.Requests
{
    public class CreateFilmRequest
    {
        public bool IsDisabled { get; set; }
        public Guid FilmDataId { get; set; }
    }
}

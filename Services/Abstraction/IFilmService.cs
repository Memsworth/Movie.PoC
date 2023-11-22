using Domain.Dtos;

namespace Services.Abstraction
{
    public interface IFilmService
    {
        Task AddFilm(FilmDto filmDto, string imageUrl);
    }
}

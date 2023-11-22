using Domain.Abstractions;
using Domain.Dtos;
using Domain.Models;
using Services.Abstraction;

namespace Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FilmService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddFilm(FilmDto filmDto, string imageUrl)
        {
            var film = new Film()
            {
                Title = filmDto.Title,
                ImageUrl = imageUrl,
                ReleaseDate = filmDto.ReleaseDate,
                Genre = filmDto.Genre,
            };

            await _unitOfWork.FilmRepository.AddAsync(film);
            await _unitOfWork.CommitAsync();
        }
    }
}

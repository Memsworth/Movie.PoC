﻿using MediatR;
using Movie.PoC.Api.Database;
using Movie.PoC.Api.Entities;
using SimpleResults;
using System.Globalization;

namespace Movie.PoC.Api.Features.FilmsData
{
    public record CreateFilmDataCommand(FilmDataRaw filmData) : IRequest<Result<Guid>>;

    public class CreateFilmDataCommandHandler : IRequestHandler<CreateFilmDataCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public CreateFilmDataCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(CreateFilmDataCommand request, CancellationToken cancellationToken)
        {
            var data = CreateFilmData(request.filmData);
            _context.FilmDatas.Add(data);
            await _context.SaveChangesAsync();
            return Result.CreatedResource().ToResult(data.Id);
        }

        private FilmDataModel CreateFilmData(FilmDataRaw filmData) 
        {
            return new FilmDataModel
            {
                Title = filmData.Title,
                Rated = Helper.ParseEnum<ContentRating>(filmData.Rated),
                Released = DateOnly.FromDateTime(DateTime.Parse(filmData.Released)),
                Genre = Helper.ParseEnum<MediaGenre>(filmData.Genre.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()),
                Director = filmData.Director,
                Writer = filmData.Writer,
                Actors = filmData.Actors,
                Plot = filmData.Plot,
                Language = Helper.ParseEnum<MediaLanguage>(filmData.Language),
                Country = Helper.ParseEnum<MediaCountry>(filmData.Country),
                Poster = filmData.Poster,
                Metascore = int.Parse(filmData.Metascore),
                imdbRating = double.Parse(filmData.imdbRating),
                imdbID = filmData.imdbID,
                imdbVotes = int.Parse(filmData.imdbVotes, NumberStyles.AllowThousands),
                Type = Helper.ParseEnum<MediaType>(filmData.Type),
                Production = filmData.Production == "N/A" ? null : filmData.Production,
                Website = filmData.Website == "N/A" ? null : filmData.Website,
            };
        }
    }

}

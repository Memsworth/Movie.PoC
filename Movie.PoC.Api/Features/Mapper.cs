using System.Globalization;
using Movie.PoC.Api.DTOs;
using Movie.PoC.Api.Models.Entities;
using Movie.PoC.Api.Requests;
using BC = BCrypt.Net.BCrypt;


namespace Movie.PoC.Api.Features;

public static class Mapper
{
    public static User MapToUserModel(this UserRegisterRequest request)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = BC.HashPassword(request.Password),
            LastModifiedDate = DateTime.Now,
            BirthDay = request.Birthday,
            Role = Role.User
        };
    }

    public static UserTokenDto MapToUserToken(this User userModel)
    {
        return new UserTokenDto
        {
            Id = userModel.Id.ToString(),
            Email = userModel.Email,
            Name = userModel.FirstName,
            Role = userModel.Role.ToString(),
        };
    }

    public static FilmData MapToFilmData(this FilmDataRaw filmData)
    {
        return new FilmData
        {
            Id = Guid.NewGuid(),
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
        };
    }
}
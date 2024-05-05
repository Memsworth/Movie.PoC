using System.Globalization;
using FluentValidation;
using Movie.PoC.Api.Contracts.DTOs;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Entities;
using Movie.PoC.Api.Features;
using BC = BCrypt.Net.BCrypt;


namespace Movie.PoC.Api.Contracts;

public static class Mapper
{
    public static UserModel MapToUserModel(this UserRegisterRequest request)
    {
        return new UserModel
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = BC.HashPassword(request.Password),
            BirthDay = request.Birthday,
            Role = Role.User
        };
    }

    public static UserTokenDto MapToUserToken(this UserModel userModel)
    {
        return new UserTokenDto
        {
            Id = userModel.Id.ToString(),
            Email = userModel.Email,
            Name = userModel.FirstName,
            Role = userModel.Role.ToString(),
        };
    }

    public static FilmDataModel MapToFilmData(this FilmDataRaw filmData)
    {
        return new FilmDataModel
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

    public static Dictionary<string, string[]> MapToResponse(this ValidationException exceptions)
    {
        var errorObject = new Dictionary<string, string[]>();
        foreach (var exception in exceptions.Errors)
        {
            errorObject.Add(exception.PropertyName, new string[] { exception.ErrorMessage });
        }

        return errorObject;
    }
}
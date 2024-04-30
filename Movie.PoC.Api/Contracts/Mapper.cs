using FluentValidation;
using Movie.PoC.Api.Contracts.DTOs;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Entities;
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
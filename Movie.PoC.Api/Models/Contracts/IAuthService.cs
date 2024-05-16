using LanguageExt.Common;
using Movie.PoC.Api.Requests;

namespace Movie.PoC.Api.Models.Contracts;

public interface IAuthService
{
    public Task<Result<Guid>> RegisterUser(UserRegisterRequest userRegistrationData);
    public Task<Result<string?>> LoginUser(LoginRequest loginRequestData);
}
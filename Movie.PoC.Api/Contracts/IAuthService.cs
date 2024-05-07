using LanguageExt.Common;
using Movie.PoC.Api.Contracts.Requests;

namespace Movie.PoC.Api.Contracts;

public interface IAuthService
{
    public Task<Result<Guid>> RegisterUser(UserRegisterRequest userRegistrationData);
    public Task<Result<string?>> LoginUser(LoginRequest loginRequestData);
}
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Movie.PoC.Api.Models.Contracts;
using Movie.PoC.Api.Requests;

namespace Movie.PoC.Api.Features.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest requestData)
        {
            var serviceResponse = await _authService.RegisterUser(requestData);
            return serviceResponse.Match<IActionResult>(
                id => CreatedAtAction(nameof(Register), new { id }),
                err => BadRequest(err.ToString())
            );
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest requestData)
        {
            var query = await _authService.LoginUser(requestData);
            return query.Match<IActionResult>(
                token => token is null ? NotFound() : Ok(new { token }),
                err => BadRequest(err.ToString())
                );
        }
    }
}

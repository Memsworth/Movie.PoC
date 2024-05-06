using Microsoft.AspNetCore.Mvc;
using Movie.PoC.Api.Contracts;
using Movie.PoC.Api.Contracts.Requests;

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

        /*[HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest requestData)
        {
            var query = new LoginQuery(requestData);
            var result = await _mediator.Send(query);
            return result.Match<IActionResult>(
                token => Ok(token.ToString()),
                err => BadRequest(err.ToString())
                );
        }*/
    }
}

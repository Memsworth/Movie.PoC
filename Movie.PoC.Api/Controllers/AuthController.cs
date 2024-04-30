using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Features.Auth;


namespace Movie.PoC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest requestData)
        {
            var command = new RegisterUserCommand(requestData);
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(
                id => CreatedAtAction(nameof(Register), new { id }),
                err => BadRequest(err.ToString())
            );
        }

        /*[HttpPost("login")]
        public async Task<ActionResult<Result<string>>> Login(string email, string password)
        {
            var request = new LoginRequest { Email = email, Password = password };
            var query = new LoginQuery(request);
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }*/
    }
}

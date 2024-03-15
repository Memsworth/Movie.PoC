using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Features.Auth;
using SimpleResults;

namespace Movie.PoC.Api.Controllers
{
    [TranslateResultToActionResult]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Result>> Register(CreateUserRequest request)
        {
            var command = new RegisterUserCommand(request);
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpPost("login")]
        public async Task<ActionResult<Result<string>>> Login(string email, string password)
        {
            var request = new LoginRequest { Email = email, Password = password };
            var Query = new LoginQuery(request);
            var result = await _mediator.Send(Query);
            return result.ToActionResult();
        }
    }
}

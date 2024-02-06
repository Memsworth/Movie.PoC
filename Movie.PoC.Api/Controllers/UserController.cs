using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Features.Users;

namespace Movie.PoC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var command = new CreateUser(request);
            var result = await _mediator.Send(command);
            return result == true ? Ok(request) : BadRequest();
        }
    }
}

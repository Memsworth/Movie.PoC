using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Features.Films;

namespace Movie.PoC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilmController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFilm(CreateFilmRequest request)
        {
            var command = new CreateFilmCommand(request);
            var commandResult = await _mediator.Send(command);
            return commandResult is true ? Ok(commandResult) : BadRequest(commandResult);
        }
    }
}

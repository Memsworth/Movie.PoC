using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.PoC.Api.Features.Films;
using Movie.PoC.Api.Features.FilmsData;

namespace Movie.PoC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilmDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovieData(string request)
        {
            var query = new FilmService.GetFilmDataQuery(request);
            var queryResult = await _mediator.Send(query);
            if (queryResult is null) 
                return NotFound("Movie Data not found");
            var command = new CreateFilmDataCommand(queryResult);
            var commandResult = await _mediator.Send(command);
            return commandResult is true ? Ok(command.filmData) : BadRequest();
        }
    }
}

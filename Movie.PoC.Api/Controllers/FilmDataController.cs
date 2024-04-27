using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.PoC.Api.Entities;
using Movie.PoC.Api.Features.Films;
using Movie.PoC.Api.Features.FilmsData;
using SimpleResults;

namespace Movie.PoC.Api.Controllers
{
    [TranslateResultToActionResult]
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
        public async Task<ActionResult<Result<CreatedGuid>>> CreateMovieData(string imdbId)
        {
            var query = new GetFilmDataQuery(imdbId);
            var queryResult = await _mediator.Send(query);
            
            if (queryResult is null) 
                return NotFound();

            var command = new CreateFilmDataCommand(queryResult);
            var commandResult = await _mediator.Send(command);
            return commandResult.ToActionResult();
        }
    }
}

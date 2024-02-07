using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetMovieData(string request)
        {
            var query = new FilmService.GetFilmDataQuery(request);
            var result = await _mediator.Send(query);
            return result is not null ? Ok(result) : BadRequest();
        }
    }
}

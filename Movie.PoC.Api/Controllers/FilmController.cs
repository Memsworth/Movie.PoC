using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public
    }
}

/*using FluentValidation;
using LanguageExt.Common;
using MediatR;
using Movie.PoC.Api.Database;
using Movie.PoC.Api.Entities;

namespace Movie.PoC.Api.Features.Films
{
    public record CreateFilmCommand(CreateFilmRequest CreateFilmRequest) : IRequest<Result<Guid>>
    {
    }

    public class CreateFilmCommandHandler : IRequestHandler<CreateFilmCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;
        public CreateFilmCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateFilmCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                return false;

            var data = new FilmModel()
            {
                IsDisabled = request.CreateFilmRequest.IsDisabled,
                FilmDataId = request.CreateFilmRequest.FilmDataId
            };

            _context.Films.Add(data);
            await _context.SaveChangesAsync();
            return true;
        }
    }


    public class CreateFilmCommandValidator : AbstractValidator<CreateFilmCommand>
    {
        public CreateFilmCommandValidator()
        {
            RuleFor(request => request.CreateFilmRequest.FilmDataId).NotEmpty().WithMessage("FilmId can't be empty.");
            RuleFor(request => request.CreateFilmRequest.IsDisabled).NotEmpty().WithMessage("Enable or Disable the film");
        }
    }
}*/

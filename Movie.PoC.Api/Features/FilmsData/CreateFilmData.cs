using FluentValidation;
using MediatR;

namespace Movie.PoC.Api.Features.FilmsData
{
    public record CreateFilmDataCommand(string ImdbId) : IRequest
    {
    }

    public class CreateFilmDataCommandHandler : IRequestHandler<CreateFilmDataCommand>
    {

    }


    public class CreateFilmDataCommandValidator : AbstractValidator<CreateFilmDataCommand>
    {
        public CreateFilmDataCommandValidator()
        {
            RuleFor(request => request.ImdbId)
                .NotEmpty()
                .WithMessage("The ID can't be empty. Please provide an movie id");
        }
    }
}

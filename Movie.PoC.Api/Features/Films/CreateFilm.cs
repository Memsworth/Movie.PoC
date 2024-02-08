using FluentValidation;
using MediatR;

namespace Movie.PoC.Api.Features.Films
{
    public class CreateFilmCommand : IRequest
    {
    }

    public class CreateFilmCommandHandler : IRequestHandler<CreateFilmCommand>
    {
        public Task Handle(CreateFilmCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


    public class CreateFilmCommandValidator : AbstractValidator<CreateFilmCommand>
    {

    }
}

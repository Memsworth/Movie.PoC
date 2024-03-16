using FluentValidation;

namespace Movie.PoC.Api.Features.FilmsData
{
    public class GetFilmDataQueryValidation : AbstractValidator<string>
    {
        public GetFilmDataQueryValidation()
        {
            RuleFor(movieId => movieId).NotEmpty().WithMessage("Movie Id cannot be empty");
        }
    }
}

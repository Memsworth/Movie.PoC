using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Options;
using Movie.PoC.Api.Entities;
using Movie.PoC.Api.Settings;
using SimpleResults;
using System.Text.Json;

namespace Movie.PoC.Api.Features.FilmsData
{
    public record GetFilmDataQuery(string ImdbId) : IRequest<Result?>;

    public class GetFilmDataQueryHandler : IRequestHandler<GetFilmDataQuery, Result?>
    {
        private IValidator<string> _validator;
        private IOptions<OmDbSettings> _settings;

        public GetFilmDataQueryHandler(IValidator<string> validator, IOptions<OmDbSettings> settings)
        {
            _validator = validator;
            _settings = settings;
        }

        public async Task<Result?> Handle(GetFilmDataQuery request, CancellationToken cancellationToken)
        {
            var httpclient = new HttpClient()
            {
                BaseAddress = new Uri("https://www.omdbapi.com/")
            };
            var validationResults = await _validator.ValidateAsync(request.ImdbId, cancellationToken);
            
            if(validationResults.IsFailed())
            {
                return Result.Invalid(validationResults.AsErrors());
            }

            string url = $"?i={request.ImdbId}&apikey={_settings.Value.ApiKey}".ToString();

            var response = await httpclient.GetAsync(url, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                validationResults.Errors.Add(new ValidationFailure
                ("BaseUri", "could not collect data"));
                return Result.Failure(validationResults.AsErrors());
            }

            var jsonFile = await response.Content.ReadAsStreamAsync(cancellationToken);
            var jsonContent = await JsonSerializer.DeserializeAsync<FilmDataRaw>(jsonFile, cancellationToken: cancellationToken);

            if (jsonContent is null)
                return null;
            
            return Result.Success();

        }
    }
}

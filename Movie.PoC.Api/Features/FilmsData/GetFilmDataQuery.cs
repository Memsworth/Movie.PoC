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
    public record GetFilmDataQuery(string ImdbId) : IRequest<FilmDataRaw?>;

    public class GetFilmDataQueryHandler : IRequestHandler<GetFilmDataQuery, FilmDataRaw?>
    {
        private readonly IValidator<string> _validator;
        private readonly IOptions<OmDbSettings> _settings;
        private readonly IHttpClientFactory _httpClientFactory;
        public GetFilmDataQueryHandler(IValidator<string> validator, IOptions<OmDbSettings> settings, IHttpClientFactory httpClientFactory)
        {
            _validator = validator;
            _settings = settings;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FilmDataRaw?> Handle(GetFilmDataQuery request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("OmDbApi");
            var validationResults = await _validator.ValidateAsync(request.ImdbId, cancellationToken);
            
            if(validationResults.IsFailed())
            {
                return null;
            }

            string url = $"?i={request.ImdbId}&apikey={_settings.Value.ApiKey}".ToString();

            var response = await httpClient.GetAsync(url, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonFile = await response.Content.ReadAsStreamAsync(cancellationToken);
            var jsonContent = await JsonSerializer.DeserializeAsync<FilmDataRaw>(jsonFile, cancellationToken: cancellationToken);

            if (jsonContent is null || jsonContent.Response is "False")
                return null;
            
            return jsonContent;

        }
    }
}

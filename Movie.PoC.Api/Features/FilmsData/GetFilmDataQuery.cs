/*using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Options;
using Movie.PoC.Api.Entities;
using Movie.PoC.Api.Settings;
using System.Text.Json;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Database;

namespace Movie.PoC.Api.Features.FilmsData
{
    public record GetFilmDataQuery(string ImdbId) : IRequest<Result<FilmDataRaw?>>;

    public class GetFilmDataQueryHandler : IRequestHandler<GetFilmDataQuery, Result<FilmDataRaw?>>
    {
        private readonly IValidator<string> _validator;
        private readonly IOptions<OmDbSettings> _settings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationDbContext _dbContext;
        public GetFilmDataQueryHandler(IValidator<string> validator, IOptions<OmDbSettings> settings,
            IHttpClientFactory httpClientFactory, ApplicationDbContext dbContext)
        {
            _validator = validator;
            _settings = settings;
            _httpClientFactory = httpClientFactory;
            _dbContext = dbContext;
        }

        public async Task<Result<FilmDataRaw?>> Handle(GetFilmDataQuery request, CancellationToken cancellationToken)
        {
            //fix an issue. Should I check if an item exist before we enter? separation of concerns is a factor here
            //for now check if the item exist here first before creating
            //later change or find a better way

            var businessValidation = new ValidationResult();
            var itemExist = await _dbContext.FilmDatas.AnyAsync(x => x.imdbID == request.ImdbId,
                cancellationToken: cancellationToken);
            if (itemExist)
            {
                businessValidation.Errors.Add(new ValidationFailure(nameof(request.ImdbId), "FilmData exists"));
                return Result.Invalid(businessValidation.AsErrors());
            }
            
            var httpClient = _httpClientFactory.CreateClient("OmDbApi");

            string url = $"?i={request.ImdbId}&apikey={_settings.Value.ApiKey}".ToString();

            var response = await httpClient.GetAsync(url, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                businessValidation.Errors.Add(new ValidationFailure("BaseUri", "can't connect to server"));
                return Result.Failure(businessValidation.AsErrors());
            }

            var jsonFile = await response.Content.ReadAsStreamAsync(cancellationToken);
            var jsonContent = await JsonSerializer.DeserializeAsync<FilmDataRaw>(jsonFile, cancellationToken: cancellationToken);

            if (jsonContent is null || jsonContent.Response is "False")
            {
                businessValidation.Errors.Add(new ValidationFailure("FilmData", "FilmData doesn't exist"));
                return Result.NotFound(businessValidation.AsErrors());
            }
            
            return Result.Success(jsonContent);

        }
    }
}*/

/*using MediatR;
using Movie.PoC.Api.Entities;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Movie.PoC.Api.Settings;

namespace Movie.PoC.Api.Features.Films
{
    public static class FilmService
    {
        public record GetFilmDataQuery(string ImdbId) : IRequest<FilmDataRaw?>;

        internal sealed class GetFilmDataHandler(HttpClient client,IOptions<OmDbSettings> settings) : IRequestHandler<GetFilmDataQuery, FilmDataRaw?>
        {
            public async Task<FilmDataRaw?> Handle(GetFilmDataQuery request, CancellationToken cancellationToken)
            {
                string url = $"?i={request.ImdbId}&apikey={settings.Value.ApiKey}".ToString();

                var response = await client.GetAsync(url, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var jsonFile = await response.Content.ReadAsStreamAsync(cancellationToken);
                var jsonContent = await JsonSerializer.DeserializeAsync<FilmDataRaw>(jsonFile, cancellationToken: cancellationToken);
                return jsonContent;
            }
        }
    }
}
*/
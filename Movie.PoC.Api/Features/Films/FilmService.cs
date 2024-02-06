using MediatR;
using Movie.PoC.Api.Entities;
using System.Text.Json;

namespace Movie.PoC.Api.Features.Films
{
    public static class FilmService
    {
        public record GetFilmDataQuery(string ImdbId) : IRequest<FilmDataRaw?>;

        internal sealed class GetFilmDataHandler : IRequestHandler<GetFilmDataQuery, FilmDataRaw?>
        {
            private readonly HttpClient _httpClient;

            public GetFilmDataHandler() => _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.omdbapi.com/")
            };


            public async Task<FilmDataRaw?> Handle(GetFilmDataQuery request, CancellationToken cancellationToken)
            {
                string url = $"?i={request.ImdbId}&apikey=66b0c81f".ToString();

                var response = await _httpClient.GetAsync(url, cancellationToken);
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

using MediatR;
using Movie.PoC.Api.Database;
using Movie.PoC.Api.Entities;
using LanguageExt.Common;
using Movie.PoC.Api.Contracts;

namespace Movie.PoC.Api.Features.FilmsData
{
    public record CreateFilmDataCommand(FilmDataRaw parsedData) : IRequest<Result<Guid>>;

    public class CreateFilmDataCommandHandler : IRequestHandler<CreateFilmDataCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public CreateFilmDataCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(CreateFilmDataCommand request, CancellationToken cancellationToken)
        {
            var data = request.parsedData.MapToFilmData();
            await _context.FilmDatas.AddAsync(data, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return data.Id;
        }
    }
}

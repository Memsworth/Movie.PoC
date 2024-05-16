using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.PoC.Api.Models.Entities;

namespace Movie.PoC.Api.Features.FilmsData
{
    public class FilmDataModelConfiguration : IEntityTypeConfiguration<FilmData>
    {
        public void Configure(EntityTypeBuilder<FilmData> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.imdbID).IsUnique();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.PoC.Api.Entities;

namespace Movie.PoC.Api.Features.FilmsData
{
    public class FilmDataModelConfiguration : IEntityTypeConfiguration<FilmDataModel>
    {
        public void Configure(EntityTypeBuilder<FilmDataModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.imdbID).IsUnique();
        }
    }
}

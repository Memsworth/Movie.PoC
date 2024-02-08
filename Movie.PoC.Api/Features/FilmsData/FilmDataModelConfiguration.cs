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
            builder.HasOne(x => x.AssociatedFilm)
                .WithOne(y => y.AssociatedFilmData)
                .HasForeignKey<FilmDataModel>(x => x.FilmId);
        }
    }
}

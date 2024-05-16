using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.PoC.Api.Models.Entities;

namespace Movie.PoC.Api.Features.Films
{
    public class FilmModelConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsDisabled).IsRequired();
            builder.HasOne(x => x.AssociatedFilmData)
                .WithOne(f => f.AssociatedFilm)
                .HasForeignKey<Film>(x => x.FilmDataId);
        }
    }
}

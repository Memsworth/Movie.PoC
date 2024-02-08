using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.PoC.Api.Entities;

namespace Movie.PoC.Api.Features.Films
{
    public class FilmModelConfiguration : IEntityTypeConfiguration<FilmModel>
    {
        public void Configure(EntityTypeBuilder<FilmModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsDisabled).IsRequired();
        }
    }
}

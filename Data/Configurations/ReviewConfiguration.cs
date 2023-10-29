using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(review => review.Id);
            builder.Property(review => review.ReviewText).IsRequired();
            builder.Property(review => review.Rating).IsRequired();

            builder.HasIndex(review => new { review.UserId, review.FilmId }).IsUnique();

            builder.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId);
            builder.HasOne(r => r.Film).WithMany(f => f.Reviews).HasForeignKey(r => r.FilmId);
        }
    }
}

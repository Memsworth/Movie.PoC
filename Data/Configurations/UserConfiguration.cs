using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.HashedPassowrd).IsRequired();
            builder.Property(x => x.Role).IsRequired().HasDefaultValue(Role.User);

            builder.HasMany(user => user.Reviews).WithOne(review => review.User).HasForeignKey(review => review.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(user => user.Email).IsUnique();
        }
    }
}

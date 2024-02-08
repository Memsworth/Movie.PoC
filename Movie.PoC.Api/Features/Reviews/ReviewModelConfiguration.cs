using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.PoC.Api.Entities;

namespace Movie.PoC.Api.Features.Reviews
{
    public class ReviewModelConfiguration : IEntityTypeConfiguration<ReviewModel>
    {
        public void Configure(EntityTypeBuilder<ReviewModel> builder)
        {
            throw new NotImplementedException();
        }
    }
}

using Cot.Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cot.Data.Persistence.EntityConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Code)
                .IsRequired();

            builder.HasIndex(e => e.Code)
                .IsUnique();

            builder.Property(e => e.Title)
                .IsRequired();

            builder.HasIndex(e => e.Title)
                .IsUnique();
        }
    }
}

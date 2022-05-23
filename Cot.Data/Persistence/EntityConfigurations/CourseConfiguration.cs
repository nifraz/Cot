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
                .IsRequired()
                .HasMaxLength(32);

            builder.HasIndex(e => e.Code)
                .IsUnique();

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasIndex(e => e.Title)
                .IsUnique();

            builder.Property(e => e.Notes)
                .HasMaxLength(512);
        }
    }
}

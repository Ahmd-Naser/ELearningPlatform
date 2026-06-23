using ELearningPlatform.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearningPlatform.Persistence.EntitiesConfigurations;

public class CourseConfigurations : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.Description).HasMaxLength(1000);

        builder.HasMany(c => c.Enrollments)
           .WithOne(e => e.Course)
           .HasForeignKey(e => e.CourseId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
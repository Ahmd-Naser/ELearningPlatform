using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearningPlatform.Persistence.EntitiesConfigurations;

public class EnrollmentConfigurations : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasIndex(x => new { x.LearnerId, x.CourseId })
            .IsUnique();
    }
}

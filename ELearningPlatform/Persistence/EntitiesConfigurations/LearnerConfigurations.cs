using ELearningPlatform.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearningPlatform.Persistence.EntitiesConfigurations;

public class LearnerConfigurations : IEntityTypeConfiguration<Learner>
{
    public void Configure(EntityTypeBuilder<Learner> builder)
    {
        builder.Property(x => x.NationalId)
         .IsRequired()
         .HasMaxLength(14); 

        builder.HasIndex(x => x.NationalId)
               .IsUnique();
    }
}
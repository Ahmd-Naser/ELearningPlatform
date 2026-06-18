using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearningPlatform.Persistence.EntitiesConfigurations;

public class DecisionConfigurations : IEntityTypeConfiguration<Decision>
{
    public void Configure(EntityTypeBuilder<Decision> builder)
    {
        builder.Property(x => x.Type)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x=>x.Reason)
                .IsRequired()
               .HasMaxLength(1000);
    }
}

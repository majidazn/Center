using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.CenterVariableAggregate.ValueObjects;
using Core.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Center.DataAccess.Configurations.CenterVariable
{
    public class CenterVariableConfiguration : IEntityTypeConfiguration<Domain.CenterVariableAggregate.Entities.CenterVariable>
    {
        public void Configure(EntityTypeBuilder<Domain.CenterVariableAggregate.Entities.CenterVariable> builder)
        {
            builder.ToTable("CenterVariables", "Center");

            builder.Property(x => x.Id).HasConversion(
             new ValueConverter<CenterVariableId, int>(centerVariableId => centerVariableId.Value,
                                value => CenterVariableId.Create(value))).ValueGeneratedNever();

            builder.Property(c => c.Name).HasMaxLength(150).IsRequired();
            builder.Property(c => c.EnName).HasMaxLength(80);

            builder.HasOne(x => x.Parent)
               .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                 .IsRequired(false);

            builder.HasMany(c => c.Activities)
                .WithOne(x => x.CenterVariable)
                .HasForeignKey(x => x.CenterVariableId)
                .HasPrincipalKey(x => x.Id);

            builder.OwnsOne(p => p.AuditBase);
            //builder.HasAlternateKey(a => a.AuditId);
            //builder.Property(o => o.AuditId).UseHiLo($"{nameof(Domain.CenterVariableAggregate.Entities.CenterVariable)}SequenceHiLo", "Center");

            builder.HasIndex(p => p.Status);
            builder.Ignore(p => p.TenantId);
        }
    }
}

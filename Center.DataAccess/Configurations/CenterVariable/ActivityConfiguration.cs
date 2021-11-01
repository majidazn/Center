using Center.Domain.CenterAggregate.Entities;
using Center.Domain.CenterVariableAggregate.ValueObjects;
using Center.Domain.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Center.DataAccess.Configurations.CenterVariable
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities", "Center");

            builder.HasKey(d => d.Id);

            builder.Property(p => p.ValidFrom).IsRequired();

            builder.OwnsOne(p => p.AuditBase);
            //builder.HasAlternateKey(a => a.AuditId);
            //builder.Property(o => o.AuditId).UseHiLo($"{nameof(Activity)}SequenceHiLo", "Center");

            builder.HasIndex(p => p.Status);
            builder.Ignore(p => p.TenantId);
        }
    }
}

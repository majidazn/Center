using Center.Domain.CenterAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Center.DataAccess.Configurations.Center
{
    public class CenterTelecomConfiguration : IEntityTypeConfiguration<CenterTelecom>
    {
        public void Configure(EntityTypeBuilder<CenterTelecom> builder)
        {
            builder.ToTable("CenterTelecoms", "Center");

            builder.HasKey(d => d.Id);

            builder.Property(p => p.CenterId).IsRequired();
            builder.Property(p => p.TellNo).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Comment).HasMaxLength(300);

            builder.OwnsOne(p => p.AuditBase);
            //builder.HasAlternateKey(a => a.AuditId);
            //builder.Property(o => o.AuditId).UseHiLo($"{nameof(Domain.CenterAggregate.Entities.CenterTelecom)}SequenceHiLo", "Center");

            builder.HasIndex(p => p.Status);
            builder.Ignore(p => p.TenantId);
        }
    }
}

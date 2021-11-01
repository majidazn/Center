using Center.Domain.CenterAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Center.DataAccess.Configurations.Center
{
    public class CenterCodeConfiguration : IEntityTypeConfiguration<CenterCode>
    {
        public void Configure(EntityTypeBuilder<CenterCode> builder)
        {
            builder.ToTable("CenterCodes", "Center");

            builder.HasKey(d => d.Id);
            //builder.Property(o => o.Id).UseHiLo($"{nameof(CenterCode)}SequenceHiLo");

            builder.Property(p => p.Code).HasMaxLength(30).IsRequired();
            builder.Property(p => p.Insur).IsRequired();
            builder.Property(p => p.TenantId).IsRequired();

            builder.OwnsOne(p => p.AuditBase);
            //builder.HasAlternateKey(a => a.AuditId);
            //builder.Property(o => o.AuditId).UseHiLo($"{nameof(Domain.CenterAggregate.Entities.CenterCode)}SequenceHiLo", "Center");

            builder.HasIndex(p => p.Status);

        }
    }
}
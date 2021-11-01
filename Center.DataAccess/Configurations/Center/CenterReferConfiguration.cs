using Center.Domain.CenterAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Center.DataAccess.Configurations.Center
{
    public class CenterReferConfiguration : IEntityTypeConfiguration<CenterRefer>
    {
        public void Configure(EntityTypeBuilder<CenterRefer> builder)
        {
            builder.ToTable("CenterRefers", "Center");

            builder.HasKey(d => d.Id);

            builder.Property(p => p.CenterId).IsRequired();
            builder.Property(p => p.Address).HasMaxLength(300);

            builder.OwnsOne(p => p.AuditBase);
            //builder.HasAlternateKey(a => a.AuditId);
            // builder.Property(o => o.AuditId).UseHiLo($"{nameof(Domain.CenterAggregate.Entities.CenterRefer)}SequenceHiLo", "Center");

            builder.HasIndex(p => p.Status);
            builder.Ignore(p => p.TenantId);       
        }
    }
}

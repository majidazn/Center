using Center.Domain.CenterAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Center.DataAccess.Configurations.Center
{
    public class ElectronicAddressConfiguration : IEntityTypeConfiguration<ElectronicAddress>
    {
        public void Configure(EntityTypeBuilder<ElectronicAddress> builder)
        {
            builder.ToTable("ElectronicAddresses", "Center");

            builder.HasKey(d => d.Id);

            builder.Property(c => c.EAddress).HasMaxLength(200).IsRequired();

            builder.OwnsOne(p => p.AuditBase);
            //builder.HasAlternateKey(a => a.AuditId);
            //builder.Property(o => o.AuditId).UseHiLo($"{nameof(Domain.CenterAggregate.Entities.ElectronicAddress)}SequenceHiLo", "Center");

            builder.HasIndex(p => p.Status);
            builder.Ignore(p => p.TenantId);
        }
    }
}

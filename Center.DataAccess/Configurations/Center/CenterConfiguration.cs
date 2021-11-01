using Center.Domain.CenterAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Center.DataAccess.Configurations.Center
{
    public class CenterConfiguration : IEntityTypeConfiguration<Domain.CenterAggregate.Entities.Center>
    {
        public void Configure(EntityTypeBuilder<Domain.CenterAggregate.Entities.Center> builder)
        {
            builder.ToTable("Centers", "Center");

            builder.HasKey(d => d.Id);

            builder.Property(p => p.Name).HasMaxLength(300).IsRequired();
            builder.Property(p => p.EnName).HasMaxLength(200);
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.CenterGroup).IsRequired();
            builder.Property(p => p.City).IsRequired();
            builder.Property(p => p.HostName).HasMaxLength(200);
            builder.Property(p => p.Logo).HasMaxLength(100000);
            builder.Property(p => p.NationalCode).HasMaxLength(20);
            builder.Property(p => p.FinanchialCode).HasMaxLength(20);
            builder.Property(p => p.SepasCode).HasMaxLength(20);
            builder.Property(p => p.ZipCode).HasMaxLength(20);
            builder.Property(p => p.ValidFrom).IsRequired();

            builder.OwnsOne(p => p.AuditBase);

            builder.Property(x => x.TenantId).HasConversion(
                new ValueConverter<TenantId, int>(tenantId => tenantId.Value,
                           value => TenantId.Create(value)));

            builder.HasIndex(x => x.TenantId).IsUnique();
            builder.HasIndex(p => p.Status);
        }
    }
}

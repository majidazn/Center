using Center.DataAccess.Configurations.Center;
using Center.DataAccess.Configurations.CenterVariable;
using Center.Domain.CenterAggregate.Entities;
using Center.Domain.CenterAggregate.ValueObjects;
using Center.Domain.CenterVariableAggregate.Entities;
using Center.Domain.CenterVariableAggregate.ValueObjects;
using Center.Domain.SharedKernel.Entities;
using Core.Common.Enums;
using Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Center.DataAccess.Context
{
    // Add-Migration NewMigration -Project src\Infrastrutures\DataAccess\Center.DataAccess -Context CenterBoundedContextCommand
    //update-database -Context CenterBoundedContextCommand
    public class CenterBoundedContextCommand : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CenterBoundedContextCommand(DbContextOptions<CenterBoundedContextCommand> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        #region DbSets
        public DbSet<Domain.CenterAggregate.Entities.Center> Centers { get; set; }
        public DbSet<CenterVariable> CenterVariables { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<CenterCode> CenterCodes { get; set; }
        public DbSet<CenterRefer> CenterRefers { get; set; }
        public DbSet<CenterTelecom> CenterTelecoms { get; set; }
        public DbSet<ElectronicAddress> ElectronicAddresses { get; set; }
        //public DbSet<SlogFieldViewModel> Slogs { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //  modelBuilder.Entity<CenterVariable>().HasNoKey();

            //modelBuilder.Entity<SlogFieldViewModel>().HasNoKey();
            //modelBuilder.Ignore<SlogFieldViewModel>();

            modelBuilder.AddRestrictDeleteBehaviorConvention();

            //modelBuilder.HasSequence($"{nameof(Domain.CenterAggregate.Entities.Center)}SequenceHiLo", "Center").StartsAt(2).IncrementsBy(1);
            //modelBuilder.HasSequence($"{nameof(CenterCode)}SequenceHiLo", "Center").StartsAt(1).IncrementsBy(1);
            //modelBuilder.HasSequence($"{nameof(CenterRefer)}SequenceHiLo", "Center").StartsAt(1).IncrementsBy(1);
            //modelBuilder.HasSequence($"{nameof(CenterTelecom)}SequenceHiLo", "Center").StartsAt(1).IncrementsBy(1);
            //modelBuilder.HasSequence($"{nameof(ElectronicAddress)}SequenceHiLo", "Center").StartsAt(1).IncrementsBy(1);

            //modelBuilder.HasSequence($"{nameof(CenterVariable)}SequenceHiLo", "Center").StartsAt(19).IncrementsBy(1);
            //modelBuilder.HasSequence($"{nameof(Activity)}SequenceHiLo", "Center").StartsAt(1).IncrementsBy(1);

            modelBuilder.ApplyConfiguration(new CenterConfiguration());
            modelBuilder.ApplyConfiguration(new CenterVariableConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityConfiguration());
            modelBuilder.ApplyConfiguration(new CenterCodeConfiguration());
            modelBuilder.ApplyConfiguration(new CenterReferConfiguration());
            modelBuilder.ApplyConfiguration(new CenterTelecomConfiguration());
            modelBuilder.ApplyConfiguration(new ElectronicAddressConfiguration());

            //     var audit = new Framework.AuditBase.DomainDrivenDesign.AuditBase{ DateTimeOffset.Now, DateTimeOffset.Now, "", 67, "");

            modelBuilder.Entity<Domain.CenterAggregate.Entities.Center>()
                .HasQueryFilter(p => p.Status != EntityStateType.Deleted && (p.ValidTo == null || p.ValidTo > DateTimeOffset.Now))
                // // uncomment just when generating migration
                .HasData(
                new
                {
                    Id = 1,
                    Name = "شرکت اطلاع رسانی پیوند داده ها",
                    EnName = "EPD2",
                    Title = 116,
                    CenterGroup = 131,
                    City = 1406,
                    HostName = "localhost:44305",
                    NationalCode = "461685665",
                    FinanchialCode = "411131341944",
                    Address = "تهران - ضلع شمال غربی میدان فردوسی ساختمان شهد پلاک 21",
                    ZipCode = "1599945549",
                    ValidFrom = DateTimeOffset.Now,
                    Status = EntityStateType.Default,
                    TenantId = TenantId.Create(1),
                    AuditId = Guid.NewGuid()
                    //  AuditBase = audit
                });

            modelBuilder.Entity<CenterVariable>().HasQueryFilter(p => p.Status != EntityStateType.Deleted)
             //    // uncomment just when generating migration
             .HasData(
              new
              {
                  Id = CenterVariableId.Create(1),
                  Name = "Root",
                  EnName = "Root",
                  Code = 0,
                  InternalUsage = 0,
                  Sort = 0,
                  Status = EntityStateType.Deleted,
                  TenantId = 0,
                  ParentId = CenterVariableId.Create(1),
                  AuditId = Guid.NewGuid()
              },
                   new { Id = CenterVariableId.Create(2), AuditId = Guid.NewGuid(), Name = "مورد استفاده در برنامه", EnName = "Used in the App", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(1) },
                   new { Id = CenterVariableId.Create(3), AuditId = Guid.NewGuid(), Name = "پیوند", EnName = "EPD", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(2) },
                   new { Id = CenterVariableId.Create(4), AuditId = Guid.NewGuid(), Name = "مراکز", EnName = "Centers", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(2) },
                   new { Id = CenterVariableId.Create(5), AuditId = Guid.NewGuid(), Name = "بیمار", EnName = "Patient", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(2) },
                   new { Id = CenterVariableId.Create(6), AuditId = Guid.NewGuid(), Name = "پیوند-مراکز", EnName = "Centers-EPD", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(2) },
                   new { Id = CenterVariableId.Create(7), AuditId = Guid.NewGuid(), Name = "گزارش عملکرد", EnName = "WorkHour", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(2) },

                   new { Id = CenterVariableId.Create(10), AuditId = Guid.NewGuid(), Name = "گروه اصلی برنامه ها", EnName = "Main Group Application", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(1) },
                   new { Id = CenterVariableId.Create(20), AuditId = Guid.NewGuid(), Name = "گروه مرکز", EnName = "Center Group", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(1) },
                   new { Id = CenterVariableId.Create(22), AuditId = Guid.NewGuid(), Name = "ماهیت مرکز", EnName = "Center Title", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(1) },
                   new { Id = CenterVariableId.Create(23), AuditId = Guid.NewGuid(), Name = "HIS Web", EnName = "HIS Web", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(10) },
                   new { Id = CenterVariableId.Create(24), AuditId = Guid.NewGuid(), Name = "HIS Cloud", EnName = "HIS Cloud", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(10) },
                   new { Id = CenterVariableId.Create(25), AuditId = Guid.NewGuid(), Name = "برنامه های کمکی تحت Cloud", EnName = "Utilities under the Cloud", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(10) },
                   new { Id = CenterVariableId.Create(26), AuditId = Guid.NewGuid(), Name = "HIS Windows Base", EnName = "HIS Windows Base", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(10) },
                   new { Id = CenterVariableId.Create(27), AuditId = Guid.NewGuid(), Name = "MIS", EnName = "MIS", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(10) },
                   new { Id = CenterVariableId.Create(28), AuditId = Guid.NewGuid(), Name = "Pacs", EnName = "Pacs", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(10) },

                   new { Id = CenterVariableId.Create(116), AuditId = Guid.NewGuid(), Name = "سایر", EnName = "Other", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(22) },
                   new { Id = CenterVariableId.Create(131), AuditId = Guid.NewGuid(), Name = "سایر مراکز دانشگاهی", EnName = "Other centers", Code = 0, InternalUsage = 0, Sort = 0, Status = EntityStateType.Default, TenantId = 0, ParentId = CenterVariableId.Create(20) }
              );

            modelBuilder.Entity<CenterVariable>().HasQueryFilter(p => p.Status != EntityStateType.Deleted);
            modelBuilder.Entity<Activity>().HasQueryFilter(p => p.Status != EntityStateType.Deleted && (p.ValidTo == null || p.ValidTo >= DateTimeOffset.Now));
            modelBuilder.Entity<CenterCode>().HasQueryFilter(p => p.Status != EntityStateType.Deleted);
            modelBuilder.Entity<CenterRefer>().HasQueryFilter(p => p.Status != EntityStateType.Deleted);
            modelBuilder.Entity<CenterTelecom>().HasQueryFilter(p => p.Status != EntityStateType.Deleted);
            modelBuilder.Entity<ElectronicAddress>().HasQueryFilter(p => p.Status != EntityStateType.Deleted);
        }
    }
}

using Center.Common.Extensions;
using Center.Domain.CenterAggregate.DomainServices;
using Center.Domain.CenterAggregate.Dtos.CenterTelecom;
using Center.Domain.CenterAggregate.Dtos.ElectronicAddress;
using Center.Domain.CenterAggregate.Rules;
using Center.Domain.CenterAggregate.ValueObjects;
using Center.Domain.SharedKernel.Entities;
using Core.Common.Enums;
using Framework.AuditBase.DomainDrivenDesign;
using Framework.Auditing.Contracts;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Center.Domain.CenterAggregate.Entities
{
    public class Center : Entity, IAuditable, IAggregateRoot
    {

        #region Constructor

        public Center()
        {

        }

        private Center(ICenterDomainServices centerDomainServices,
                      string name,
                      string enName,
                      int title,
                      int centerGroup,
                      int city,
                      string hostName,
                      byte[] logo,
                      string nationalCode,
                      string financhialCode,
                      string sepasCode,
                      string address,
                      string zipCode,
                      DateTimeOffset validFrom,
                      DateTimeOffset? validTo)
        {
            Name = name;
            EnName = enName;
            Title = title;
            CenterGroup = centerGroup;
            City = city;
            HostName = hostName;
            Logo = logo;
            NationalCode = nationalCode;
            FinanchialCode = financhialCode;
            SepasCode = sepasCode;
            Address = address;
            ZipCode = zipCode;
            ValidFrom = validFrom;
            ValidTo = validTo;
            TenantId = ValueObjects.TenantId.Create(centerDomainServices);
            Status = EntityStateType.Default;
        }
        #endregion

        #region Properties
        public new int Id { get; private set; }
        public string Name { get; private set; }
        public string EnName { get; private set; }
        public int Title { get; private set; }
        public int CenterGroup { get; private set; }
        public int City { get; private set; }
        public new TenantId TenantId { get; private set; }
        public string HostName { get; private set; }
        public byte[] Logo { get; private set; }
        public string NationalCode { get; private set; }
        public string FinanchialCode { get; private set; }
        public string SepasCode { get; private set; }
        public string Address { get; private set; }
        public string ZipCode { get; private set; }
        public DateTimeOffset ValidFrom { get; private set; }
        public DateTimeOffset? ValidTo { get; private set; }
        public Guid AuditId { get; private set; }
        public AuditBase AuditBase { get; private set; }
        public EntityStateType Status { get; private set; }

        private readonly List<CenterTelecom> _centerTelecoms = new List<CenterTelecom>();
        public IReadOnlyCollection<CenterTelecom> CenterTelecoms => _centerTelecoms.AsReadOnly();

        private readonly List<ElectronicAddress> _electronicAddresses = new List<ElectronicAddress>();
        public IReadOnlyCollection<ElectronicAddress> ElectronicAddresses => _electronicAddresses.AsReadOnly();

        private readonly List<CenterCode> _centerCodes = new List<CenterCode>();
        public IReadOnlyCollection<CenterCode> CenterCodes => _centerCodes.AsReadOnly();

        private readonly List<CenterRefer> _centerRefers = new List<CenterRefer>();
        public IReadOnlyCollection<CenterRefer> CenterRefers => _centerRefers.AsReadOnly();

        private readonly List<Activity> _activities = new List<Activity>();
        public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();

        #endregion

        #region Behaviors

        public static async System.Threading.Tasks.Task<Center> Create(ICenterDomainServices centerDomainServices, string name, string enName, int title, int centerGroup,
            int city, string hostName, byte[] logo, string nationalCode, string financhialCode, string sepasCode, string address,
            string zipCode, DateTimeOffset validFrom, DateTimeOffset? validTo,
            List<CreateTelecomDto> telecoms, List<CreateElectronicAddressDto> electronicAddresses 
            )
        {
            var center = new Center(centerDomainServices, name, enName, title, centerGroup, city, hostName, logo, nationalCode, financhialCode
                , sepasCode, address, zipCode, validFrom, validTo);

            CheckRule(new TenantIdMustBeUniqueRule(centerDomainServices, center.TenantId.Value));
            CreateEnforceInvariants(centerDomainServices, name, enName, title, centerGroup, city, hostName, logo, nationalCode, financhialCode,
                sepasCode, address, zipCode, validFrom, validTo, center.TenantId.Value, telecoms, electronicAddresses);

            center.AddTelecoms(telecoms);
            center.AddElectronicAddresses(electronicAddresses);

            return center;
        }

        public void EditCenter(ICenterDomainServices centerDomainServices, int centerId, string name, string enName, int title, int centerGroup,
            int city, string hostName, byte[] logo, string nationalCode, string financhialCode, string sepasCode, string address,
            string zipCode, DateTimeOffset validFrom, DateTimeOffset? validTo,
            List<CreateTelecomDto> telecoms, List<CreateElectronicAddressDto> electronicAddresses)
        {
            EditEnforceInvariants(centerDomainServices, centerId, name, enName, title, centerGroup, city, hostName, logo, nationalCode, financhialCode,
                sepasCode, address, zipCode, validFrom, validTo, telecoms, electronicAddresses);

            Name = name;
            EnName = enName;
            Title = title;
            CenterGroup = centerGroup;
            City = city;
            HostName = hostName;
            Logo = logo;
            NationalCode = nationalCode;
            FinanchialCode = financhialCode;
            SepasCode = sepasCode;
            Address = address;
            ZipCode = zipCode;
            ValidFrom = validFrom;
            ValidTo = validTo;

            EditTelecoms(telecoms);
            EditElectronicAddresses(electronicAddresses);
        }

        public void RemoveCenter(ICenterDomainServices centerDomainServices, int centerId)
        {
            CheckRule(new CenterMustHasNotActivityRule(centerDomainServices, centerId));

            Status = EntityStateType.Deleted;
        }

        private void AddElectronicAddresses(List<CreateElectronicAddressDto> electronicAddresses)
        {
            foreach (var electronicAddress in electronicAddresses)
                _electronicAddresses.Add(ElectronicAddress.Create(electronicAddress.EType, electronicAddress.EAddress));
        }

        private void EditElectronicAddresses(List<CreateElectronicAddressDto> electronicAddresses)
        {
            foreach (var electronicAddress in _electronicAddresses)
            {
                var findElectronicAddress = electronicAddresses.Find(i => i.Id == electronicAddress.Id);

                if (findElectronicAddress == null)
                    electronicAddress.ChangeStatus(EntityStateType.Deleted);
                else
                    electronicAddress.Edit(electronicAddress.EType, electronicAddress.EAddress);
            }

            foreach (var electronicAddress in electronicAddresses.Where(t => t.Id == 0))
                _electronicAddresses.Add(ElectronicAddress.Create(electronicAddress.EType, electronicAddress.EAddress));
        }

        private void AddTelecoms(List<CreateTelecomDto> telecoms)
        {
            foreach (var telecom in telecoms)
                _centerTelecoms.Add(CenterTelecom.Create(telecom.Type, telecom.Section, telecom.TellNo, telecom.Comment));
        }

        private void EditTelecoms(List<CreateTelecomDto> telecoms)
        {
            foreach (var telecom in _centerTelecoms)
            {
                var findTelecom = telecoms.Find(i => i.CreateTelecomId == telecom.Id);

                if (findTelecom == null)
                    telecom.ChangeStatus(EntityStateType.Deleted);
                else
                    telecom.Edit(findTelecom.Type, findTelecom.Section, findTelecom.TellNo, findTelecom.Comment);
            }

            foreach (var telecom in telecoms.Where(t => t.CreateTelecomId == 0))
                _centerTelecoms.Add(CenterTelecom.Create(telecom.Type, telecom.Section, telecom.TellNo, telecom.Comment));
        }

        private static void CreateEnforceInvariants(ICenterDomainServices centerDomainServices, string name, string enName, int title, int centerGroup,
            int city, string hostName, byte[] logo, string nationalCode, string financhialCode, string sepasCode, string address,
            string zipCode, DateTimeOffset validFrom, DateTimeOffset? validTo, int tenantId,
            List<CreateTelecomDto> telecoms, List<CreateElectronicAddressDto> electronicAddresses)
        {
            EnforceInvariants(name, enName, title, centerGroup, city, hostName, logo, nationalCode, financhialCode,
             sepasCode, address, zipCode, validFrom, validTo, telecoms, electronicAddresses);

            if (!string.IsNullOrWhiteSpace(name))
                CheckRule(new CenterNameMustBeUniqueRule(centerDomainServices, name, city, 0));
        }

        private static void EditEnforceInvariants(ICenterDomainServices centerDomainServices, int centerId, string name, string enName, int title, int centerGroup,
                int city, string hostName, byte[] logo, string nationalCode, string financhialCode, string sepasCode, string address,
                string zipCode, DateTimeOffset validFrom, DateTimeOffset? validTo,
                List<CreateTelecomDto> telecoms, List<CreateElectronicAddressDto> electronicAddresses)
        {

            EnforceInvariants(name, enName, title, centerGroup, city, hostName, logo, nationalCode, financhialCode,
           sepasCode, address, zipCode, validFrom, validTo, telecoms, electronicAddresses);

            if (!string.IsNullOrWhiteSpace(name))
                CheckRule(new CenterNameMustBeUniqueRule(centerDomainServices, name, city, centerId));
        }
        private static void EnforceInvariants(string name, string enName, int title, int centerGroup,
        int city, string hostName, byte[] logo, string nationalCode, string financhialCode, string sepasCode, string address,
        string zipCode, DateTimeOffset validFrom, DateTimeOffset? validTo,
        List<CreateTelecomDto> telecoms, List<CreateElectronicAddressDto> electronicAddresses)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new AppException("نام اجباری می باشد");

            if (name?.Length > 300)
                throw new AppException("نام حداکثر 300 کاراکتر می باشد");

            if (enName?.Length > 200)
                throw new AppException("نام انگلیسی حداکثر 200 کاراکتر می باشد");

            if (title <= 0)
                throw new AppException("مقدار عنوان مرکز اجباری است");

            if (city <= 0)
                throw new AppException("مقدار شهر اجباری است");

            if (centerGroup <= 0)
                throw new AppException("مقدار گروه مرکز اجباری است");

            if (hostName?.Length > 200)
                throw new AppException("آدرس اینترنتی حداکثر 200 کاراکتر می باشد");

            if (nationalCode?.Length > 20)
                throw new AppException("کد ملی مرکز حداکثر 20 کاراکتر می باشد");

            if (financhialCode?.Length > 20)
                throw new AppException("کد اقتصادی مرکز حداکثر 20 کاراکتر می باشد");

            if (sepasCode?.Length > 20)
                throw new AppException("کد سپاس مرکز حداکثر 20 کاراکتر می باشد");

            if (zipCode?.Length > 20)
                throw new AppException("کد پستی مرکز حداکثر 20 کاراکتر می باشد");

            if (validFrom <= DateTimeOffset.MinValue)
                throw new AppException("تاریخ شروع اجباری می باشد");

            if (telecoms == null)
                throw new AppException("حداقل یک شماره موبایل اجاری می باشد");

            if (logo != null && (logo.Length / 1024) > 200)
                throw new AppException("لوگو نباید از 200 کیلو بایت بیشتر باشد");
        }

        #endregion
    }
}

using Center.Domain.CenterAggregate.DomainServices;
using Center.Domain.CenterAggregate.Rules;
using Center.Domain.CenterVariableAggregate.DomainServices;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.CenterVariableAggregate.Rules;
using Center.Domain.CenterVariableAggregate.ValueObjects;
using Center.Domain.SharedKernel.Entities;
using Core.Common.Enums;
using Framework.AuditBase.DomainDrivenDesign;
using Framework.Auditing.Contracts;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using Framework.Exceptions;
using Framework.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Center.Domain.CenterVariableAggregate.Entities
{
    public class CenterVariable : Entity, IAuditable, IAggregateRoot
    {
        #region Constructor
        public CenterVariable() { }
        private CenterVariable(
                           string name,
                           string enName,
                           int? parentId,
                           int? code,
                           int sort,
                           int internalUsage,
                           string address,
                           string shortKey,
                           byte[]? icon)
        {
            Name = name;
            EnName = enName;
            ParentId = CenterVariableId.Create(parentId.ToInteger(0));
            Code = code;
            Sort = sort;
            InternalUsage = internalUsage;
            Address = address;
            ShortKey = shortKey;
            Icon = icon;
            Status = EntityStateType.Default;
        }
        #endregion

        #region Properties
        public new CenterVariableId Id { get; private set; }
        public string Name { get; private set; }
        public string EnName { get; private set; }
        public CenterVariableId ParentId { get; private set; }
        public CenterVariable Parent { get; private set; }
        public int? Code { get; private set; }
        public int Sort { get; private set; }
        public int InternalUsage { get; private set; }
        public string Address { get; set; }
        public byte[] Icon { get; private set; }
        public string ShortKey { get; private set; }
        public Guid AuditId { get; private set; }
        public AuditBase AuditBase { get; private set; }
        public EntityStateType Status { get; private set; }

        private readonly List<Activity> _activities = new List<Activity>();
        public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();

        private readonly List<CenterVariable> _centerVariables = new List<CenterVariable>();
        public IReadOnlyCollection<CenterVariable> Children => _centerVariables;

        #endregion

        #region Behaviors
        public static CenterVariable Create(ICenterVariableDomainServices centerVariableDomainServices, string name, string enName,
          int? parentId, int? code, int sort, int internalUsage, string address, string shortKey, byte[]? icon)
        {
            CenterVariable centerVariable = new CenterVariable(name, enName, parentId, code, sort, internalUsage, address, shortKey, icon);
            centerVariable.Id = CenterVariableId.Create(centerVariableDomainServices);

            EnforceInvariants(centerVariableDomainServices, name, enName, parentId, code, sort, internalUsage, centerVariable.Id.Value, address, shortKey, icon);
            CheckRule(new CenterVariableNameMustBeUniqueRule(centerVariableDomainServices, name, code, 0, parentId));
            CheckRule(new CenterVariableNameMustHaveUniqueCode(centerVariableDomainServices, code.ToInteger(0)));

            return centerVariable;
        }

        public void EditCenterVariable(ICenterVariableDomainServices centerVariableDomainServices, string name, string enName,
          int? parentId, int? code, int sort, int internalUsage, string address, string shortKey, byte[]? icon, int centerVariableId)
        {
            EnforceInvariants(centerVariableDomainServices, name, enName, parentId, code, sort, internalUsage, centerVariableId, address, shortKey, icon);
            CheckRule(new CenterVariableNameMustBeUniqueRule(centerVariableDomainServices, name, code, centerVariableId, parentId));
            CheckRule(new CenterVariableNameMustHaveUniqueCode(centerVariableDomainServices, code.ToInteger(0)));

            Name = name;
            EnName = enName;
            ParentId = CenterVariableId.Create(parentId.ToInteger(0));
            Code = code;
            Sort = sort;
            InternalUsage = internalUsage;
            Address = address;
            ShortKey = shortKey;
            Icon = icon;
        }

        public void RemoveCenterVariable(ICenterVariableDomainServices centerVariableDomainServices,
            ICenterDomainServices centerDomainServices, int centerVariableId)
        {
            CheckRule(new CenterVariableMustHasNotActivityRule(centerVariableDomainServices, centerVariableId));
            CheckRule(new CenterVariableMustHasNotChildRule(centerVariableDomainServices, centerVariableId));
            CheckRule(new CenterMustHasNotCenterVariables(centerDomainServices, centerVariableId));
            CheckRule(new CenterVariableMustNotDeletedRule(centerVariableDomainServices, centerVariableId));

            Status = EntityStateType.Deleted;
        }

        public void SortCenterVariable(int sort) => this.Sort = sort;

        private static void EnforceInvariants(ICenterVariableDomainServices centerVariableDomainServices, string name, string enName,
          int? parentId, int? code, int sort, int internalUsage, int centerVariableId, string address, string shortKey, byte[]? icon)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new AppException("نام اجباری می باشد");

            if (string.IsNullOrWhiteSpace(enName))
                throw new AppException("نام انگلیسی اجباری می باشد");

            if (name?.Length > 150)
                throw new AppException("نام حداکثر 150 کاراکتر می باشد");

            if (enName?.Length > 80)
                throw new AppException("نام انگلیسی حداکثر 80 کاراکتر می باشد");

            if (address?.Length > 150)
                throw new AppException("آدرس حداکثر 150 کاراکتر می باشد");

            if (shortKey?.Length > 30)
                throw new AppException("میانبر حداکثر 30 کاراکتر می باشد");

            if (parentId <= 0)
                throw new AppException("مقدار parentId  اجباری می باشدس");

            if (icon != null && (icon.Length / 1024) > 200)
                throw new AppException("آیکن نباید از 200 کیلو بایت بیشتر باشد");
        }
        #endregion
    }
}

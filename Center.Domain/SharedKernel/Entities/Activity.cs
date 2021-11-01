using Center.Domain.CenterVariableAggregate.DomainServices;
using Center.Domain.CenterVariableAggregate.Entities;
using Center.Domain.CenterVariableAggregate.ValueObjects;
using Center.Domain.SharedKernel.Rules;
using Core.Common.Enums;
using Framework.AuditBase.DomainDrivenDesign;
using Framework.Auditing.Contracts;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using Framework.Exceptions;
using System;

namespace Center.Domain.SharedKernel.Entities
{
    public class Activity : Entity, IAuditable
    {
        #region Constructor
        private Activity() { }
        private Activity(int centerVariableId, int centerId, DateTimeOffset validFrom, DateTimeOffset? validTo)
        {
            CenterVariableId = CenterVariableId.Create(centerVariableId);
            CenterId = centerId;
            ValidFrom = validFrom;
            ValidTo = validTo;
            Status = EntityStateType.Default;

        }
        #endregion

        #region Properties
        public new int Id { get; private set; }
        public CenterVariableId CenterVariableId { get; private set; }
        public CenterVariable CenterVariable { get; private set; }
        public int CenterId { get; private set; }
        public DateTimeOffset ValidFrom { get; private set; }
        public DateTimeOffset? ValidTo { get; private set; }
        public Guid AuditId { get; private set; }
        public AuditBase AuditBase { get; private set; }
        public EntityStateType Status { get; private set; }

        #endregion

        #region Behaviors

        public static Activity Create(ICenterVariableDomainServices centerVariableDomainServices,
            int centerVariableId, int centerId, DateTimeOffset validFrom, DateTimeOffset? validTo)
        {

            CreateEnforceInvariants(centerVariableDomainServices, centerVariableId, centerId, validFrom, validTo);
            Activity activity = new Activity(centerVariableId, centerId, validFrom, validTo);
            return activity;
        }



        private static void CreateEnforceInvariants(ICenterVariableDomainServices centerVariableDomainServices,
            int centerVariableId, int centerId, DateTimeOffset validFrom, DateTimeOffset? validTo)
        {
            if (validFrom <= DateTimeOffset.MinValue)
                throw new AppException("تاریخ شروع اجباری می باشد");

            CheckRule(new OnlyApplicationCanInsertAsActivityRule(centerVariableDomainServices, centerVariableId));
            CheckRule(new ActivityMustBeUnique(centerVariableDomainServices, centerVariableId, centerId));
            CheckRule(new AssignableWithInternalUsageRule(centerVariableDomainServices, centerVariableId, centerId));
        }

        public void RemoveActivity(ICenterVariableDomainServices centerVariableDomainServices, int activityId)
        {
            Status = EntityStateType.Deleted;
        }


        #endregion
    }
}

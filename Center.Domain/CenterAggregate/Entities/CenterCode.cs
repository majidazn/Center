using Core.Common.Enums;
using Framework.AuditBase.DomainDrivenDesign;
using Framework.Auditing.Contracts;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Center.Domain.CenterAggregate.Entities
{
    public class CenterCode : Entity, IAuditable
    {
        #region Constructor
        private CenterCode(int insur, string code, int tenantId)
        {
            Insur = insur;
            Code = code;
            TenantId = tenantId;
            Status = EntityStateType.Default;
        }
        #endregion

        #region Properties
        public new int Id { get; private set; }
        public int CenterId { get; private set; }
        public int Insur { get; private set; }
        public string Code { get; private set; }

        public Guid AuditId { get; private set; }
        public AuditBase AuditBase { get; private set; }
        public EntityStateType Status { get; private set; }
        #endregion

        #region Behaviors
        #endregion
    }
}

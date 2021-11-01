using Core.Common.Enums;
using Framework.AuditBase.DomainDrivenDesign;
using Framework.Auditing.Contracts;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using static Center.Common.Enums.Enums;

namespace Center.Domain.CenterAggregate.Entities
{
    public class CenterRefer : Entity, IAuditable
    {

        #region Constructor
        private CenterRefer(AddressType urlType, string address)
        {
            UrlType = urlType;
            Address = address;
            Status = EntityStateType.Default;
        }
        #endregion

        #region Properties
        public new int Id { get; private set; }
        public int CenterId { get; private set; }
        public AddressType UrlType { get; private set; }
        public string Address { get; private set; }
        public Guid AuditId { get; private set; }
        public AuditBase AuditBase { get; private set; }
        public EntityStateType Status { get; private set; }
        #endregion

        #region Behaviors
        #endregion
    }
}

using Core.Common.Enums;
using Framework.AuditBase.DomainDrivenDesign;
using Framework.Auditing.Contracts;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Center.Domain.CenterAggregate.Entities
{
    public class ElectronicAddress : Entity, IAuditable
    {
        #region Constructor
        private ElectronicAddress(int eType, string eAddress)
        {
            EType = eType;
            EAddress = eAddress;
            Status = EntityStateType.Default;
        }
        #endregion

        #region Properties
        public new int Id { get; private set; }
        public int CenterId { get; private set; }
        public int EType { get; private set; }
        public string EAddress { get; private set; }
        public Guid AuditId { get; private set; }
        public AuditBase AuditBase { get; private set; }
        public EntityStateType Status { get; private set; }
        #endregion

        #region Behaviors

        public static ElectronicAddress Create(int eType, string eAddress)
        {
            EnforceInvariants(eType, eAddress);
            return new ElectronicAddress(eType, eAddress);
        }
        public void Edit(int eType, string eAddress)
        {
            EnforceInvariants(eType, eAddress);
            this.EType = eType;
            this.EAddress = eAddress;
        }
        public void ChangeStatus(EntityStateType status) => this.Status = status;
        private static void EnforceInvariants(int eType, string eAddress)
        {
            if (string.IsNullOrWhiteSpace(eAddress))
                throw new AppException("آدرس اجباری می باشد");

            if (eAddress?.Length >= 200)
                throw new AppException("آدرس حداکثر 200 کاراکتر می باشد");
            if (eType <= 0)
                throw new AppException("نوع آدرس اجباری می باشد");

        }
        #endregion
    }
}

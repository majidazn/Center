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
    public class CenterTelecom : Entity, IAuditable
    {
        #region Constructor
        private CenterTelecom(int type, int section, string tellNo, string comment)
        {
            Type = type;
            Section = section;
            TellNo = tellNo;
            Comment = comment;
            Status = EntityStateType.Default;
        }
        #endregion

        #region Properties
        public new int Id { get; private set; }
        public int CenterId { get; private set; }
        public int Type { get; private set; }
        public int Section { get; private set; }
        public string TellNo { get; private set; }
        public string Comment { get; private set; }
        public Guid AuditId { get; private set; }
        public AuditBase AuditBase { get; private set; }
        public EntityStateType Status { get; private set; }
        #endregion

        #region Behaviors
        public static CenterTelecom Create(int type, int section, string tellNo, string comment)
        {
            EnforceInvariants(type, section, tellNo, comment);
            return new CenterTelecom(type, section, tellNo, comment);
        }
        public void Edit(int type, int section, string tellNo, string comment)
        {
            EnforceInvariants(type, section, tellNo, comment);
            this.Type = type;
            this.Section = section;
            this.TellNo = tellNo;
            this.Comment = comment;
        }

        public void ChangeStatus(EntityStateType status) => this.Status = status;

        private static void EnforceInvariants(int type, int section, string tellNo, string comment)
        {
            if (string.IsNullOrWhiteSpace(tellNo))
                throw new AppException("شماره موبایل/تلفن اجباری می باشد");


            if (tellNo?.Length >= 15)
                throw new AppException("شماره موبایل/تلفن حداکثر 15 کاراکتر می باشد");

            if (comment?.Length >= 300)
                throw new AppException("توضیحات اطلاعات تماس حداکثر 300 کاراکتر می باشد");

            //    if (type == (int)Enums.TelKind.Mobile || type == (int)Enums.TelKind.RelativesMobile)
            //        CheckRule(new PhoneNumberMustBeValidRule(tellNo));

            //    if (type == (int)Enums.TelKind.Work || type == (int)Enums.TelKind.Home || type == (int)Enums.TelKind.RelativesFixed)
            //        CheckRule(new PrefixMustBeValidRule(prefix));
            //}
        }
        #endregion
    }
}
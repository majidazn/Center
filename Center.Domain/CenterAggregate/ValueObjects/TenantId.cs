using Center.Domain.CenterAggregate.DomainServices;
using Framework.Auditing.Contracts;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.ValueObjects
{
    public class TenantId : ValueObject, IAuditableReference
    {
        #region Properties
        public int Value { get; private set; }
        #endregion

        #region Constructors
        private TenantId(int value)
        {
            this.Value = value;
        }
        #endregion

        #region Behaviors
        public static implicit operator int(TenantId tenantId) => tenantId.Value;
        public static TenantId Create(int tenantId)
        {
            return new TenantId(tenantId);
        }
        public static TenantId Create(ICenterDomainServices centerDomainServices)
        {
            var result = centerDomainServices.GenerateTenantId().Result;

            if (result == 0)
                result = 10000;

            result++;

            return new TenantId(result);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        #endregion
    }
}

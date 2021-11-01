using Center.Domain.CenterAggregate.DomainServices;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Rules
{
    public class TenantIdMustBeUniqueRule : IBusinessRule
    {
        private readonly ICenterDomainServices _centerDomainServices;
        private readonly CancellationToken _cancellationToken;
        private readonly int _tenantId;
        public TenantIdMustBeUniqueRule(
               ICenterDomainServices centerDomainServices,
              int tenantId,
            CancellationToken cancellationToken = default
            )
        {
            _centerDomainServices = centerDomainServices;
            _cancellationToken = cancellationToken;
            _tenantId = tenantId;
        }

        public string Message => "TenantId تکراری می باشد";

        public bool IsBroken() => _centerDomainServices.IsTenantIdUnique(_tenantId, _cancellationToken).Result;


    }
}

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
    public class CenterMustHasNotActivityRule : IBusinessRule
    {
        private readonly ICenterDomainServices _centerDomainServices;
        private readonly CancellationToken _cancellationToken;
        private readonly int _centerId;

        public CenterMustHasNotActivityRule(
             ICenterDomainServices centerDomainServices,
            int centerId,
            CancellationToken cancellationToken = default
            )
        {
            _centerDomainServices = centerDomainServices;
            _cancellationToken = cancellationToken;

            _centerId = centerId;
        }
        public string Message => "این مرکز دارای برنامه فعال می باشد";

        public bool IsBroken() => _centerDomainServices.HasCenterActivity(_centerId, _cancellationToken).Result;
    }
}

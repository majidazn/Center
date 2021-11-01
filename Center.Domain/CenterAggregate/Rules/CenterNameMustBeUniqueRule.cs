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
    public class CenterNameMustBeUniqueRule : IBusinessRule
    {
        private readonly ICenterDomainServices _centerDomainServices;
        private readonly CancellationToken _cancellationToken;
        private readonly string _centerName;
        private readonly int _city;
        private readonly int _centerId;

        public CenterNameMustBeUniqueRule(
            ICenterDomainServices centerDomainServices, 
            string centerName,
            int city,
            int centerId,
            CancellationToken cancellationToken=default
            )
        {
            _centerDomainServices = centerDomainServices;
            _cancellationToken = cancellationToken;
            _centerName = centerName;
            _city = city;
            _centerId = centerId;
        }
        public string Message => "این مرکز قبلا ثبت شده است";

        public bool IsBroken() => _centerDomainServices.IsCenterNameUnique(_centerName, _city,_centerId ,_cancellationToken).Result;
       
    }
}

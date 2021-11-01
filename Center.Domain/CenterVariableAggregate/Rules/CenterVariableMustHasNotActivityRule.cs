using Center.Domain.CenterVariableAggregate.DomainServices;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Rules
{
    public class CenterVariableMustHasNotActivityRule : IBusinessRule
    {
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;
        private readonly CancellationToken _cancellationToken;
        private readonly int _centerVariableId;


        public CenterVariableMustHasNotActivityRule(

                 ICenterVariableDomainServices centerVariableDomainServices,
            int centerVariableId,
        CancellationToken cancellationToken = default
            )
        {
            _cancellationToken = cancellationToken;
            _centerVariableDomainServices = centerVariableDomainServices;
            _centerVariableId = centerVariableId;
        }

        public string Message => "این متغیر به مرکزی تخصیص داده شده است";

        public bool IsBroken() => _centerVariableDomainServices.IsAssignedThisVariableToACenter(_centerVariableId, _cancellationToken).Result;
    }
}

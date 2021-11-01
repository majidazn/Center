using Center.Domain.CenterVariableAggregate.DomainServices;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.Domain.SharedKernel.Rules
{
    public class ActivityMustBeUnique : IBusinessRule
    {
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;
        private readonly CancellationToken _cancellationToken;
        private readonly int _centerVariableId;
        private readonly int _centerId;


        public ActivityMustBeUnique(

             ICenterVariableDomainServices centerVariableDomainServices,
    int centerVariableId,
    int centerId,
        CancellationToken cancellationToken = default
            )
        {
            _cancellationToken = cancellationToken;
            _centerVariableDomainServices = centerVariableDomainServices;
            _centerVariableId = centerVariableId;
            _centerId = centerId;

        }
        public string Message => "این فعالیت قبلا به این مرکز تخصیص داده شده است";

        public bool IsBroken() => _centerVariableDomainServices.IsAssignedThisApplicationToThisCenter(_centerVariableId, _centerId);
       
    }
}

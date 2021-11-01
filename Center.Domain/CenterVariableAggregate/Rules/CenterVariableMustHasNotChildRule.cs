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
    public class CenterVariableMustHasNotChildRule : IBusinessRule
    {
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;
        private readonly CancellationToken _cancellationToken;
        private readonly int _centerVariableId;


        public CenterVariableMustHasNotChildRule(

                 ICenterVariableDomainServices centerVariableDomainServices,
            int centerVariableId,
        CancellationToken cancellationToken = default
            )
        {
            _cancellationToken = cancellationToken;
            _centerVariableDomainServices = centerVariableDomainServices;
            _centerVariableId = centerVariableId;
        }
        public string Message => "این متغیر دارای فرزند است، بنارابن امکان حذف وجود ندارد";

        public bool IsBroken() => _centerVariableDomainServices.HasCenterVariableAnyChild(_centerVariableId, _cancellationToken).Result;

    }
}

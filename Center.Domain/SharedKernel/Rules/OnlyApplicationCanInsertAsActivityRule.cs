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
    public class OnlyApplicationCanInsertAsActivityRule : IBusinessRule
    {
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;
        private readonly CancellationToken _cancellationToken;
        private readonly int _centerVariableId;
        
        public OnlyApplicationCanInsertAsActivityRule(

        ICenterVariableDomainServices centerVariableDomainServices,
        int centerVariableId,
       
        CancellationToken cancellationToken = default
    )
        {
            _cancellationToken = cancellationToken;
            _centerVariableDomainServices = centerVariableDomainServices;
            _centerVariableId = centerVariableId;
           

        }

        public string Message => "فقط برنامه ها قابلت تخصیص به مراکز را دارند";

        public bool IsBroken() => _centerVariableDomainServices.IsCenterVariableAppliction(_centerVariableId);
    }
}

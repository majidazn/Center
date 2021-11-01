using Center.Domain.CenterVariableAggregate.DomainServices;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Rules
{
    public class CenterVariableMustNotDeletedRule : IBusinessRule
    {
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;
        private readonly int centerVariableId;

        public CenterVariableMustNotDeletedRule(ICenterVariableDomainServices centerVariableDomainServices,
                    int centerVariableId)
        {
            _centerVariableDomainServices = centerVariableDomainServices;
            this.centerVariableId = centerVariableId;
        }
        public string Message => "این متغیر قابل حذف نمی باشد";

        public bool IsBroken() => _centerVariableDomainServices.IsCenterVariableInternalUsage(centerVariableId);
    }
}

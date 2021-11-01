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
    public class CenterMustHasNotCenterVariables : IBusinessRule
    {
        private readonly ICenterDomainServices centerDomainServices;
        private readonly int centerVariableId;

        public CenterMustHasNotCenterVariables(ICenterDomainServices centerDomainServices, 
                      int centerVariableId)
        {
            this.centerDomainServices = centerDomainServices;
            this.centerVariableId = centerVariableId;
        }

        public string Message => "این متغیر در مرکز استفاده میشود";

        public bool IsBroken() => centerDomainServices.HasCenterVariable(centerVariableId).Result;
       
    }
}

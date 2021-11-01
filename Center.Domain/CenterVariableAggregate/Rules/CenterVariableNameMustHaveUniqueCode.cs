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
    public class CenterVariableNameMustHaveUniqueCode : IBusinessRule
    {
        public string Message => "کد تکراری میباشد";
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;
        private readonly int _code;
        public bool IsBroken()
        {
            return _centerVariableDomainServices.IsDuplicateCode(_code);
        }

        public CenterVariableNameMustHaveUniqueCode(
            ICenterVariableDomainServices centerVariableDomainServices,
             int code)
        {
            _centerVariableDomainServices = centerVariableDomainServices;
            _code = code;
        }
    }
}

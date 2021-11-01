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
    public class CenterVariableNameMustBeUniqueRule : IBusinessRule
    {
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;
        private readonly CancellationToken _cancellationToken;
        private readonly string _name;
        private readonly int? _code;
        private readonly int _centerVariableId;
        private readonly int? _parentId;

        public CenterVariableNameMustBeUniqueRule(
            ICenterVariableDomainServices centerVariableDomainServices,
             string name,
             int? code,
             int centerVariableId,
             int? parentId,
             CancellationToken cancellationToken = default
            )
        {
            _cancellationToken = cancellationToken;
            _centerVariableDomainServices = centerVariableDomainServices;
            _name = name;
            _code = code;
            _centerVariableId = centerVariableId;
            _parentId = parentId;
        }

        public string Message => "این مورد قبلا ثبت شده است";

        public bool IsBroken() => _centerVariableDomainServices.IsDublicatedName(_name, _code, _parentId
            , _centerVariableId, _cancellationToken);
    }
}

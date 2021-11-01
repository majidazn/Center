using Center.ApplicationServices.CommonServices.AccountService;
using Center.Domain.CenterAggregate.DomainServices;
using Center.Domain.CenterVariableAggregate.DomainServices;
using Center.Domain.CenterVariableAggregate.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.Commands.RemoveCenterVariableCommand
{
    public class RemoveCenterVariableCommandHandler : IRequestHandler<RemoveCenterVariableCommand, bool>
    {
        #region Fields
        private readonly ICenterVariableRepositoryCommand _CenterVariableRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterVariableDomainServices _CenterVariableDomainServices;
        private readonly ICenterDomainServices _CenterDomainServices;
        #endregion

        #region Constructors
        public RemoveCenterVariableCommandHandler(
            ICenterVariableRepositoryCommand CenterVariableRepositoryCommand,

            ICenterVariableDomainServices CenterVariableDomainServices,
            ICenterDomainServices CenterDomainServices
            )
        {
            _CenterVariableRepositoryCommand = CenterVariableRepositoryCommand;

            _CenterVariableDomainServices = CenterVariableDomainServices;
            _CenterDomainServices = CenterDomainServices;
        }
        #endregion

        public async Task<bool> Handle(RemoveCenterVariableCommand request, CancellationToken cancellationToken)
        {
            var centerVariable = await _CenterVariableRepositoryCommand.GetCenterVariableById(request.CenterVariableId, cancellationToken);
            centerVariable.RemoveCenterVariable(_CenterVariableDomainServices, _CenterDomainServices, centerVariable.Id.Value);

            return (await _CenterVariableRepositoryCommand.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}

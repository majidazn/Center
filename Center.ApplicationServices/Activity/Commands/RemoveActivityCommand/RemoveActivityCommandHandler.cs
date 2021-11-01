using Center.ApplicationServices.CommonServices.AccountService;
using Center.Domain.CenterVariableAggregate.DomainServices;
using Center.Domain.CenterVariableAggregate.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Activity.Commands.RemoveActivityCommand
{
    public class RemoveActivityCommandHandler : IRequestHandler<RemoveActivityCommand, bool>
    {

        #region Fields
        private readonly ICenterVariableRepositoryCommand _CenterVariableRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterVariableDomainServices _CenterVariableDomainServices;
        #endregion

        #region Constructors
        public RemoveActivityCommandHandler(
            ICenterVariableRepositoryCommand CenterVariableRepositoryCommand,
            IAccountServices accountServices,
            ICenterVariableDomainServices CenterVariableDomainServices
            )
        {
            _CenterVariableRepositoryCommand = CenterVariableRepositoryCommand;
            _accountServices = accountServices;
            _CenterVariableDomainServices = CenterVariableDomainServices;
        }
        #endregion
        public async Task<bool> Handle(RemoveActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _CenterVariableRepositoryCommand.GetActivityById(request.ActivityId, cancellationToken);
            activity.RemoveActivity(_CenterVariableDomainServices, activity.Id);
            return (await _CenterVariableRepositoryCommand.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}

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

namespace Center.ApplicationServices.Activity.Commands.CreateActivityCommand
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, long>
    {
        #region Fields

        private readonly ICenterVariableRepositoryCommand _centerVariableRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;

        #endregion

        #region Constructors
        public CreateActivityCommandHandler(
            ICenterVariableRepositoryCommand centerVariableRepositoryCommand,
            IAccountServices accountServices,
            ICenterVariableDomainServices centerVariableDomainServices
            )
        {
            _centerVariableRepositoryCommand = centerVariableRepositoryCommand;
            _accountServices = accountServices;
            _centerVariableDomainServices = centerVariableDomainServices;
        }
        #endregion

        public async Task<long> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            Domain.SharedKernel.Entities.Activity activity =
                Domain.SharedKernel.Entities.Activity.Create(
                              _centerVariableDomainServices,
                              request.ActivityDto.CenterVariableId,
                              request.ActivityDto.CenterId,
                              request.ActivityDto.ValidFrom,
                              request.ActivityDto.ValidTo);

            await _centerVariableRepositoryCommand.CreateActivity(activity);

            await _centerVariableRepositoryCommand.SaveChangesAsync(cancellationToken);






            return activity.Id;
        }
    }
}

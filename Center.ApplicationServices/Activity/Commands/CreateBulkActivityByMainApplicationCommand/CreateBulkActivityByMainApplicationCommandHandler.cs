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

namespace Center.ApplicationServices.Activity.Commands.CreateBulkActivityByMainApplicationCommand
{
    public class CreateBulkActivityByMainApplicationCommandHandler : IRequestHandler<CreateBulkActivityByMainApplicationCommand, long>
    {
        #region Fields

        private readonly ICenterVariableRepositoryCommand _centerVariableRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;

        #endregion

        #region Constructors
        public CreateBulkActivityByMainApplicationCommandHandler(
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

        public async Task<long> Handle(CreateBulkActivityByMainApplicationCommand request, CancellationToken cancellationToken)
        {
            // گرفتن تمامی زیرگروه های موجود این گروه فعالیت
            var centerVariables = await _centerVariableRepositoryCommand.FetchCenterVariablesAggregate(request.ActivityDto.ParentId, cancellationToken);

            var activityList = new List<Domain.SharedKernel.Entities.Activity>();

            foreach (var cv in centerVariables)
            {
                Domain.SharedKernel.Entities.Activity activity =
                         Domain.SharedKernel.Entities.Activity.Create(
                          _centerVariableDomainServices,
                          cv.Id.Value,
                          request.ActivityDto.CenterId,
                          request.ActivityDto.ValidFrom,
                          request.ActivityDto.ValidTo
                          );

                activityList.Add(activity);
            }

            await _centerVariableRepositoryCommand.CreateActivities(activityList);
            return await _centerVariableRepositoryCommand.SaveChangesAsync(cancellationToken);
        }
    }
}

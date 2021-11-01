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

namespace Center.ApplicationServices.CenterVariable.Commands.CreateCenterVariableCommand
{
    public class CreateCenterVariableCommandHandler : IRequestHandler<CreateCenterVariableCommand, long>
    {
        #region Fields

        private readonly ICenterVariableRepositoryCommand _centerVariableRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterVariableDomainServices _centerVariableDomainServices;

        #endregion

        #region Constructors
        public CreateCenterVariableCommandHandler(
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

        public async Task<long> Handle(CreateCenterVariableCommand request, CancellationToken cancellationToken)
        {
            Domain.CenterVariableAggregate.Entities.CenterVariable centerVariable =
                   Domain.CenterVariableAggregate.Entities.CenterVariable.Create(
                       _centerVariableDomainServices,
                       request.CreateCenterVariableDto.Name,
                       request.CreateCenterVariableDto.EnName,
                       request.CreateCenterVariableDto.ParentId,
                       request.CreateCenterVariableDto.Code,
                       request.CreateCenterVariableDto.Sort,
                       request.CreateCenterVariableDto.InternalUsage,
                       request.CreateCenterVariableDto.Address,
                       request.CreateCenterVariableDto.ShortKey,
                       request.CreateCenterVariableDto.Icon
                       );

            await _centerVariableRepositoryCommand.CreateAsyncUoW(centerVariable, cancellationToken);

            await _centerVariableRepositoryCommand.SaveChangesAsync(cancellationToken);

            return centerVariable.Id.Value;
        }
    }
}

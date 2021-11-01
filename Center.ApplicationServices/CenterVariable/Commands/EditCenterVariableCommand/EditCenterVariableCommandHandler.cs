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

namespace Center.ApplicationServices.CenterVariable.Commands.EditCenterVariableCommand
{
    public class EditCenterVariableCommandHandler : IRequestHandler<EditCenterVariableCommand, bool>
    {
        #region Fields
        private readonly ICenterVariableRepositoryCommand _CenterVariableRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterVariableDomainServices _CenterVariableDomainServices;
        #endregion

        #region Constructors
        public EditCenterVariableCommandHandler(
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

        public async Task<bool> Handle(EditCenterVariableCommand request, CancellationToken cancellationToken)
        {
            var centervariable = await _CenterVariableRepositoryCommand.FetchCenterVariableAggregate(request.EditCenterVariableDto.Id, cancellationToken);

            centervariable.EditCenterVariable(_CenterVariableDomainServices,
                request.EditCenterVariableDto.Name,
                request.EditCenterVariableDto.EnName,
                request.EditCenterVariableDto.ParentId,
                request.EditCenterVariableDto.Code,
                request.EditCenterVariableDto.Sort,
                request.EditCenterVariableDto.InternalUsage,
                request.EditCenterVariableDto.Address,
                request.EditCenterVariableDto.ShortKey,
                request.EditCenterVariableDto.Icon,
                centervariable.Id.Value);

            return (await _CenterVariableRepositoryCommand.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}

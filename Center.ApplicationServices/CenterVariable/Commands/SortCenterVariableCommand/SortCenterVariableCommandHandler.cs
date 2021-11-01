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

namespace Center.ApplicationServices.CenterVariable.Commands.SortCenterVariableCommand
{
    public class SortCenterVariableCommandHandler : IRequestHandler<SortCenterVariableCommand, bool>
    {
        #region Fields
        private readonly ICenterVariableRepositoryCommand _CenterVariableRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterVariableDomainServices _CenterVariableDomainServices;
        #endregion

        #region Constructors
        public SortCenterVariableCommandHandler(
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
        public async Task<bool> Handle(SortCenterVariableCommand request, CancellationToken cancellationToken)
        {
            var parentId = request.SortDtos.FirstOrDefault().ParentId;

            var centerVariables = await _CenterVariableRepositoryCommand.FetchCenterVariablesAggregate(parentId, cancellationToken);
            var orderetCenterVariables = centerVariables.OrderBy(o => o.Id);
            var orderedClientList = request.SortDtos.OrderBy(o => o.CentervariableId);

            var bothList = orderedClientList.Zip(orderetCenterVariables, (sortDto, cVariable) => new { FromClient = sortDto, FromDB = cVariable });

            foreach (var item in bothList)
                item.FromDB.SortCenterVariable(item.FromClient.Sort);

            return (await _CenterVariableRepositoryCommand.SaveChangesAsync()) > 0;
        }
    }
}

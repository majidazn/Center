using Center.ApplicationServices.CommonServices.AccountService;
using Center.Domain.CenterAggregate.DomainServices;
using Center.Domain.CenterAggregate.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Commands.RemoveCenterCommand
{
    public class RemoveCenterCommandHandler : IRequestHandler<RemoveCenterCommand, bool>
    {
        #region Fields
        private readonly ICenterRepositoryCommand _centerRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterDomainServices _centerDomainServices;
        #endregion

        #region Constructors
        public RemoveCenterCommandHandler(
            ICenterRepositoryCommand centerRepositoryCommand,
            IAccountServices accountServices,
            ICenterDomainServices centerDomainServices
            )
        {
            _centerRepositoryCommand = centerRepositoryCommand;
            _accountServices = accountServices;
            _centerDomainServices = centerDomainServices;
        }
        #endregion
        public async Task<bool> Handle(RemoveCenterCommand request, CancellationToken cancellationToken)
        {
            var center = await _centerRepositoryCommand.GetCenterById(request.CenterId, cancellationToken);

            center.RemoveCenter(_centerDomainServices, center.Id);
            return (await _centerRepositoryCommand.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}

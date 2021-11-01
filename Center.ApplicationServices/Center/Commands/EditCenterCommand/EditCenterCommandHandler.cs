using Center.ApplicationServices.CommonServices.AccountService;
using Center.Common.Extensions;
using Center.Domain.CenterAggregate.DomainServices;
using Center.Domain.CenterAggregate.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Commands.EditCenterCommand
{
    public class EditCenterCommandHandler : IRequestHandler<EditCenterCommand, bool>
    {
        #region Fields
        private readonly ICenterRepositoryCommand _centerRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterDomainServices _centerDomainServices;
        #endregion

        #region Constructors
        public EditCenterCommandHandler(
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

        public async Task<bool> Handle(EditCenterCommand request, CancellationToken cancellationToken)
        {
            var center = await _centerRepositoryCommand.FetchCenterAggregate(request.EditCenterDto.CenterId, cancellationToken);

              center.EditCenter(
                  _centerDomainServices,
                  center.Id,
                  request.EditCenterDto.Name,
                  request.EditCenterDto.EnName,
                  request.EditCenterDto.Title,
                  request.EditCenterDto.CenterGroup,
                  request.EditCenterDto.City,
                  request.EditCenterDto.HostName,
                  request.EditCenterDto.Logo,
                  request.EditCenterDto.NationalCode,
                  request.EditCenterDto.FinanchialCode,
                  request.EditCenterDto.SepasCode,
                  request.EditCenterDto.Address,
                  request.EditCenterDto.ZipCode,
                  request.EditCenterDto.ValidFrom,
                  request.EditCenterDto.Validto,
                  request.EditCenterDto.Telecoms,
                  request.EditCenterDto.ElectronicAddresses);

            return (await _centerRepositoryCommand.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}

using Center.ApplicationServices.CommonServices.AccountService;
using Center.Common.Extensions;
using Center.Domain.CenterAggregate.DomainServices;
using Center.Domain.CenterAggregate.Repositories;
using Framework.DomainDrivenDesign.Domain.SeedWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Commands.CreateCenterCommand
{
    public class CreateCenterCommandHandler : IRequestHandler<CreateCenterCommand, long>
    {

        #region Fields

        private readonly ICenterRepositoryCommand _centerRepositoryCommand;
        private readonly IAccountServices _accountServices;
        private readonly ICenterDomainServices _centerDomainServices;

        #endregion

        #region Constructors
        public CreateCenterCommandHandler(
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

        public async Task<long> Handle(CreateCenterCommand request, CancellationToken cancellationToken)
        {

            var center = await Domain.CenterAggregate.Entities.Center.Create(
                _centerDomainServices,
                request.CreateCenterDto.Name,
                request.CreateCenterDto.EnName,
                request.CreateCenterDto.Title,
                request.CreateCenterDto.CenterGroup,
                request.CreateCenterDto.City,
                request.CreateCenterDto.HostName,
                request.CreateCenterDto.Logo,
                request.CreateCenterDto.NationalCode,
                request.CreateCenterDto.FinanchialCode,
                request.CreateCenterDto.SepasCode,
                request.CreateCenterDto.Address,
                request.CreateCenterDto.ZipCode,
                request.CreateCenterDto.ValidFrom,
                request.CreateCenterDto.Validto,
               
                request.CreateCenterDto.Telecoms,
                request.CreateCenterDto.ElectronicAddresses);

            await _centerRepositoryCommand.CreateAsyncUoW(center, cancellationToken);
            await _centerRepositoryCommand.SaveChangesAsync(cancellationToken);

            return center.Id;
        }
    }
}

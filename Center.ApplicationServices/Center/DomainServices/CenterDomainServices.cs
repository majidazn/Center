using Center.Domain.CenterAggregate.DomainServices;
using Center.Domain.CenterAggregate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.DomainServices
{
    public class CenterDomainServices : ICenterDomainServices
    {
        private readonly ICenterRepositoryCommand _centerRepositoryCommand;

        public CenterDomainServices(ICenterRepositoryCommand centerRepositoryCommand)
        {
            _centerRepositoryCommand = centerRepositoryCommand;
        }

        public async Task<bool> IsCenterNameUnique(string name, int city, int centerId, CancellationToken cancellationToken)
        => await _centerRepositoryCommand.AnyAsync(p => p.Name.Replace(" ", "") == name.Replace(" ", "") && p.City == city
        && (centerId == 0 || p.Id != centerId)
        , cancellationToken);

        public async Task<bool> IsTenantIdUnique(int tenantId, CancellationToken cancellationToken)
        => await _centerRepositoryCommand.AnyAsync(p => p.TenantId  == tenantId, cancellationToken);

        public async Task<int> GenerateTenantId(CancellationToken cancellationToken = default) =>
            await _centerRepositoryCommand.GenerateTenantId(cancellationToken);

        public async Task<bool> HasCenterActivity(int centerId, CancellationToken cancellationToken)
        => await _centerRepositoryCommand.HasCenterActivity(centerId, cancellationToken);

        public async Task<bool> HasCenterVariable(int centerVariableId, CancellationToken cancellationToken = default)
       => await _centerRepositoryCommand.HasCenterVariable(centerVariableId, cancellationToken);
    }
}

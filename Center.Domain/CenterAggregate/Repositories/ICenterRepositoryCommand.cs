using Center.Common.Enums;
using Framework.AuditBase.ViewModel;
using Framework.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Repositories
{
    public interface ICenterRepositoryCommand : IRepository<Entities.Center>
    {
        Task<int> GenerateTenantId(CancellationToken cancellationToken=default);
        Task<Domain.CenterAggregate.Entities.Center> FetchCenterAggregate(long centerId, CancellationToken cancellationToken = default);
        Task<Domain.CenterAggregate.Entities.Center> GetCenterById(int centerId, CancellationToken cancellationToken = default);
        Task<bool> HasCenterActivity(int centerId, CancellationToken cancellationToken = default);
        Task<bool> HasCenterVariable(int centerVariableId, CancellationToken cancellationToken);
    }
}

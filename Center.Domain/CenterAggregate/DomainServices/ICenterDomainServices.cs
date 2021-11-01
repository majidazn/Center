using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.DomainServices
{
    public interface ICenterDomainServices
    {
        Task<bool> IsCenterNameUnique(string name, int city,int centerId, CancellationToken cancellationToken = default);
        Task<bool> IsTenantIdUnique(int tenantId, CancellationToken cancellationToken = default);
        Task<int> GenerateTenantId(CancellationToken cancellationToken = default);
        Task<bool> HasCenterActivity(int centerId, CancellationToken cancellationToken);
        Task<bool> HasCenterVariable(int centerVariableId, CancellationToken cancellationToken= default);
    }
}

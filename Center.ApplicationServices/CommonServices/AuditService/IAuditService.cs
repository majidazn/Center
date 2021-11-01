using Framework.AuditBase.ViewModel;
using Center.Common.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CommonServices.AuditService
{
    public interface IAuditService
    {
        Task<List<SlogUIViewModel>> GetEntityRawSlogsAsync(CenterDomainEntitiesType centerDomainEntitiesType, long primaryKey, CancellationToken cancellationToken = default);
    }
}

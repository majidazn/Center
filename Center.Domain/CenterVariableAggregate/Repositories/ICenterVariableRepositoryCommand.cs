using Center.Domain.CenterVariableAggregate.Entities;
using Center.Domain.SharedKernel.Entities;
using Framework.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Repositories
{
    public interface ICenterVariableRepositoryCommand : IRepository<CenterVariable>
    {
        bool IsDublicatedCode(int code, CancellationToken cancellationToken = default);
        bool IsDublicatedName(string name, int? code, int? parentId, int centervariableId, CancellationToken cancellationToken = default);
        bool IsAssignedThisApplicationToThisCenter(int centerVariableId, int centerId);
        Task<Domain.CenterVariableAggregate.Entities.CenterVariable> FetchCenterVariableAggregate(long centerVariableId, CancellationToken cancellationToken);
        Task<bool> IsAssignedThisVariableToACenter(int centerVariableId, CancellationToken cancellationToken = default);
        Task<Domain.CenterVariableAggregate.Entities.CenterVariable> GetCenterVariableById(int centerVariableId, CancellationToken cancellationToken = default);
        Task<int> GenerateCenterVariableId(CancellationToken cancellationToken = default);
        Task<List<Domain.CenterVariableAggregate.Entities.CenterVariable>> FetchCenterVariablesAggregate(int parentId, CancellationToken cancellationToken = default);
        bool IsAssignableApplicationToCenter(int centerVariableId, int centerId);
        Task CreateActivity(Activity activity);
        Task<Domain.SharedKernel.Entities.Activity> GetActivityById(int activityId, CancellationToken cancellationToken = default);
        Task CreateActivities(List<Activity> activities);
        Task<bool> HasCenterVariableAnyChild(int centerVariableId, CancellationToken cancellationToken);
    }
}

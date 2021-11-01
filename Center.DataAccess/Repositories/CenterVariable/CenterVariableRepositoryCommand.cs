using Center.Common.Enums;
using Center.DataAccess.Context;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.CenterVariableAggregate.Enums;
using Center.Domain.CenterVariableAggregate.Repositories;
using Center.Domain.SharedKernel.Entities;
using Framework.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.DataAccess.Repositories.CenterVariable
{
    public class CenterVariableRepositoryCommand : Repository<Domain.CenterVariableAggregate.Entities.CenterVariable>, ICenterVariableRepositoryCommand
    {
        private readonly CenterBoundedContextCommand _centerBoundedContextCommand;
        public CenterVariableRepositoryCommand(CenterBoundedContextCommand dbContext, IHttpContextAccessor httpContextAccessor = null)
            : base(dbContext, httpContextAccessor)
        {
            _centerBoundedContextCommand = dbContext;
        }

        public bool IsDublicatedName(string name, int? code, int? parentId, int centervariableId, CancellationToken cancellationToken)
        {
            return (from c in _centerBoundedContextCommand.CenterVariables
                    where c.ParentId == parentId && c.Id != centervariableId
                    && (c.Name.Replace(" ", "").Equals(name.Replace(" ", "")))
                    && (c.Code != null ? c.Code == code : true)
                    select c).Any();
        }

        public bool IsAssignedThisApplicationToThisCenter(int centerVariableId, int centerId) =>
         _centerBoundedContextCommand.Activities.Any(a => a.CenterVariableId == centerVariableId && a.CenterId == centerId);

        public async Task<Domain.CenterVariableAggregate.Entities.CenterVariable> FetchCenterVariableAggregate(long centerVariableId, CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.CenterVariables.
                FirstOrDefaultAsync(e => e.Id == centerVariableId, cancellationToken);
        }

        public async Task<bool> IsAssignedThisVariableToACenter(int centerVariableId, CancellationToken cancellationToken)
         => await _centerBoundedContextCommand.Activities.AnyAsync(a => a.CenterVariableId == centerVariableId, cancellationToken);

        public async Task<Domain.CenterVariableAggregate.Entities.CenterVariable> GetCenterVariableById(int centerVariableId, CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.CenterVariables.FirstOrDefaultAsync(q => q.Id == centerVariableId, cancellationToken);
        }

        public async Task<int> GenerateCenterVariableId(CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.CenterVariables.IgnoreQueryFilters()
                        .Select(x => x.Id).MaxAsync(cancellationToken);
        }

        public async Task<List<Domain.CenterVariableAggregate.Entities.CenterVariable>> FetchCenterVariablesAggregate(int parentId, CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.CenterVariables
                .Where(e => e.ParentId == parentId).ToListAsync(cancellationToken);
        }

        public bool IsAssignableApplicationToCenter(int centerVariableId, int centerId)
        {
            var isCenterPayvand = _centerBoundedContextCommand.Centers.Any(a => a.Id == centerId && a.TenantId == (byte)Tenants.EpdPayvand);

            var centerVariable = _centerBoundedContextCommand.CenterVariables.
                FirstOrDefault(q => q.Id == centerVariableId);

            if (!isCenterPayvand && centerVariable.InternalUsage == (int)InternalUsage.EPD)
                return true;
            return false;
        }

        public async Task CreateActivity(Activity activity)
        {
            await _centerBoundedContextCommand.Activities.AddAsync(activity);
        }

        public async Task CreateActivities(List<Activity> activities)
        {
            await _centerBoundedContextCommand.Activities.AddRangeAsync(activities);
        }

        public async Task<Activity> GetActivityById(int activityId, CancellationToken cancellationToken)
        {
            return await _centerBoundedContextCommand.Activities.FirstOrDefaultAsync(q => q.Id == activityId, cancellationToken);
        }

        public async Task<bool> HasCenterVariableAnyChild(int centerVariableId, CancellationToken cancellationToken) =>
            await _centerBoundedContextCommand.CenterVariables.AnyAsync(a => a.ParentId == centerVariableId, cancellationToken);

        public bool IsDublicatedCode(int code, CancellationToken cancellationToken = default)
        {
            return (from c in _centerBoundedContextCommand.CenterVariables
                    where c.Code == code
                    select c).Any();
        }
    }
}

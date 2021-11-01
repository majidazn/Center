using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.CenterVariableAggregate.Enums;
using Center.Domain.SharedKernel.Dtos.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Repositories
{
    public interface ICenterVariableRepositoryQuery
    {
        IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> GetCenterVariables();
        Task<List<CenterVariableResultSearchDto>> CachedCenterVariables(int centerVariableParentId);
        Task<List<ActivityDto>> GetActivitiesByCenterId(int centerId);
        Task<List<ActivityDto>> GetActivitiesByTenantId(int tenantId, int parentId);
        Task<List<ActivityParentDto>> GetActivitiesParentByCenterId(int centerId);
        Task<List<ActivityDto>> GetActivitiesByParentAndCenterId(int centerId, int parentId);
        Task<List<CenterVariableCodeDto>> GetApplications();
        Task<List<ApplicationDto>> GetActiveApplications(int tenantId);
        Task<List<ApplicationDto>> GetActiveCloudApplications(int tenantId);
        Task<List<ApplicationDto>> GetCenterApplications(int tenantId);
        Task<List<CenterVariableResultSearchDto>> GetCenterVariablesByParentAndInternalUsage(int internalUsage, int centerVariableParentId );
        Task<List<CenterVariableResultSearchDto>> GetCenterVariablesByParentId(int centerVariableParentId );
        Task<List<CenterVariableResultSearchDto>> GetActiveApplicationsByInernalUsage(int tenantId, int appType);
        Task<List<CenterVariablesWithActiveApplicationsDto>> GetCenterVariablesWithActiveApplications(int parentId, int centerId, int tenantId);
        Task<List<CenterVariableResultSearchDto>> GetCenterVariablesWithParentByParentId(int centerVariableParentId);
        Task<List<CenterVariableResultSearchDto>> GetCenterVariablesWithChildrenByParentId(int centerVariableParentId);
    }
}

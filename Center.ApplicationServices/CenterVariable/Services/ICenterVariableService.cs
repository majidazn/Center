using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.SharedKernel.Dtos.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.Services
{
    public interface ICenterVariableService
    {
        Task<List<CenterVariableResultSearchDto>> GetCachedCenterVariablesByParentId(int parentId);
        Task<List<ApplicationDto>> GetActiveApplications(int tenantId);
        Task<List<ActivityDto>> GetActiveApplications(int tenantId, int parentId);
        Task<List<CenterVariableResultSearchDto>> GetCenterVariablesByParentIdAndInternalUsage(int internalUsage,int parentId);
        Task<List<CenterVariableResultSearchDto>> GetCenterVariablesByParentId(int parentId);
        Task<List<CenterVariableResultSearchDto>> GetCenterVariables(int internalUsage);
        Task<List<CenterVariableResultSearchDto>> GetActiveApplicationsByInernalUsage(int tenantId, int appType);
        Task<List<CenterVariablesWithActiveApplicationsDto>> GetCenterVariablesWithActiveApplications(int parentId, int centerId, int tenantId);
        Task<List<CenterVariableResultSearchDto>> GetCenterVariablesWithParentByParentId(int centerVariableParentId);
        Task<List<CenterVariableResultSearchDto>> GetCenterVariablesWithChildrenByParentId(int centerVariableParentId);
    }
}

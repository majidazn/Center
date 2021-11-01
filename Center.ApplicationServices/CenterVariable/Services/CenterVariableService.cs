using Center.ApplicationServices.CommonServices.GeneralVariableServices;
using Center.Common.Dtos.GeneralVariablesDto;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.CenterVariableAggregate.Repositories;
using Center.Domain.SharedKernel.Dtos.Activity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.Services
{
    public class CenterVariableService : ICenterVariableService
    {
        private readonly ICenterVariableRepositoryQuery _centerVariableRepositoryQuery;

        public CenterVariableService(ICenterVariableRepositoryQuery centerVariableRepositoryQuery)
        {
            _centerVariableRepositoryQuery = centerVariableRepositoryQuery;
        }

        public async Task<List<CenterVariableResultSearchDto>> GetCachedCenterVariablesByParentId(int parentId)
        {
            return await _centerVariableRepositoryQuery.CachedCenterVariables(parentId);
        }
        public Task<List<CenterVariableResultSearchDto>> GetCenterVariablesByParentIdAndInternalUsage(int internalUsage, int parentId)
        {
            return _centerVariableRepositoryQuery.GetCenterVariablesByParentAndInternalUsage(internalUsage, parentId);
        }
        public Task<List<CenterVariableResultSearchDto>> GetCenterVariablesByParentId(int parentId)
        {
            return _centerVariableRepositoryQuery.GetCenterVariablesByParentId(parentId);
        }
        public Task<List<CenterVariableResultSearchDto>> GetCenterVariables(int internalUsage)
        {
            return _centerVariableRepositoryQuery.GetCenterVariablesByParentId(internalUsage);
        }

        public Task<List<CenterVariablesWithActiveApplicationsDto>> GetCenterVariablesWithActiveApplications(int parentId, int centerId, int tenantId)
        {
            return _centerVariableRepositoryQuery.GetCenterVariablesWithActiveApplications(parentId, centerId, tenantId);
        }

        public Task<List<ActivityDto>> GetActiveApplications(int tenantId, int parentId)
        {
            return _centerVariableRepositoryQuery.GetActivitiesByTenantId(tenantId, parentId);
        }
        public Task<List<ApplicationDto>> GetActiveApplications(int tenantId)
        {
            return _centerVariableRepositoryQuery.GetActiveApplications(tenantId);
        }
        public Task<List<CenterVariableResultSearchDto>> GetActiveApplicationsByInernalUsage(int tenantId, int appType)
        {
            return _centerVariableRepositoryQuery.GetActiveApplicationsByInernalUsage(tenantId, appType);
        }
        public Task<List<CenterVariableResultSearchDto>> GetCenterVariablesWithParentByParentId(int centerVariableParentId)
        {
            return _centerVariableRepositoryQuery.GetCenterVariablesWithParentByParentId(centerVariableParentId);
        }
        public Task<List<CenterVariableResultSearchDto>> GetCenterVariablesWithChildrenByParentId(int centerVariableParentId)
        {
            return _centerVariableRepositoryQuery.GetCenterVariablesWithChildrenByParentId(centerVariableParentId);
        }
    }
}

using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.CenterVariableAggregate.Repositories;
using Center.Domain.SharedKernel.Dtos.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Activity.Serices
{
    public class ActivityService : IActivityService
    {
        private readonly ICenterVariableRepositoryQuery _centerVariableRepositoryQuery;
        public ActivityService(ICenterVariableRepositoryQuery centerVariableRepositoryQuery)
        {
            _centerVariableRepositoryQuery = centerVariableRepositoryQuery;
        }

        public async Task<List<ApplicationDto>> GetActiveApplications(int tenantId)
        {
            return await _centerVariableRepositoryQuery.GetActiveApplications(tenantId);
        }
        public async Task<List<ApplicationDto>> GetActiveCloudApplications(int tenantId)
        {
            return await _centerVariableRepositoryQuery.GetActiveCloudApplications(tenantId);
        }
        
        public async Task<List<ApplicationDto>> GetCenterApplications(int tenantId)
        {
            return await _centerVariableRepositoryQuery.GetCenterApplications(tenantId);
        }

        public async Task<List<ActivityDto>> GetActivitiesByCenterId(int centerId)
        {
            return await _centerVariableRepositoryQuery.GetActivitiesByCenterId(centerId);
        }
        public async Task<List<ActivityParentDto>> GetActivitiesParentByCenterId(int centerId)
        {
            return await _centerVariableRepositoryQuery.GetActivitiesParentByCenterId(centerId);
        }
        public async Task<List<ActivityDto>> GetActivitiesByParentAndCenterId(int centerId, int parentId) 
        {
            return await _centerVariableRepositoryQuery.GetActivitiesByParentAndCenterId(centerId, parentId);
        }
        public async Task<List<CenterVariableCodeDto>> GetApplications()
        {
            return await _centerVariableRepositoryQuery.GetApplications();
        }

    }
}

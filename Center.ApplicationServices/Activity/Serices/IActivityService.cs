using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.SharedKernel.Dtos.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Activity.Serices
{
    public interface IActivityService
    {
        Task<List<ActivityDto>> GetActivitiesByCenterId(int centerId);
        Task<List<ActivityParentDto>> GetActivitiesParentByCenterId(int centerId);
        Task<List<ActivityDto>> GetActivitiesByParentAndCenterId(int centerId, int parentId);
        Task<List<ApplicationDto>> GetActiveApplications(int tenantId);
        Task<List<ApplicationDto>> GetActiveCloudApplications(int tenantId);
        Task<List<CenterVariableCodeDto>> GetApplications();
        Task<List<ApplicationDto>> GetCenterApplications(int tenantId);
    }
}

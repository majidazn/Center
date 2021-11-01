using Center.Common.Dtos.GeneralVariablesDto;
using Center.Domain.CenterAggregate.Dtos.Center;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Repositories
{
    public interface ICenterRepositoryQuery
    {
        //Task<List<CenterFullDto>> GetCenters();
        IQueryable<Domain.CenterAggregate.Entities.Center> GetCenters();
        Task<CenterFullDto> GetCenterById(int centerId);
        Task<List<int>> GetTenantsByCenterIds(List<int> centerIds);
        Task<byte[]> GetCenterLogoById(int centerId);
        Task<CenterInLoginPageDto> GetCenterByHostName(string hostname);
        Task<List<CenterInLoginPageDto>> GetCentersWithTitleAndCity(int centerGroupId);
        Task<List<int>> GetTenantIdsByCenterGroup(int centerGroupId);
        Task<List<CenterFullDto>> GetCenterList();
        Task<byte[]> GetCenterLogoByTenantId(int tenantId);
        Task<List<CenterFullDto>> GetCenters(int centerGroupId, List<long> cities);
        Task<List<SelectListItemDto>> GetCentersName();
        Task<List<CenterFullDto>> GetAllCenters();
        Task<SelectListItemDto> GetCenterGroupByCenterId(int centerId);
        Task<List<CenterGroupDto>> GetCenterGroupByCenterIds(List<int> centerIds);
    }
}

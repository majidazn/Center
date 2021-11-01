using Center.ApplicationServices.CommonServices.GeneralVariableServices;
using Center.ApplicationServices.Infrastrutures.Mapper;
using Center.Common.Dtos.GeneralVariablesDto;
using Center.Domain.CenterAggregate.Dtos.Center;
using Center.Domain.CenterAggregate.Repositories;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Services
{
    public class CenterService : ICenterService
    {
        private readonly ICenterRepositoryQuery _centerRepositoryQuery;
        private readonly IGeneralVariableServices _genralVariableServices;

        public CenterService(ICenterRepositoryQuery centerRepositoryQuery,
                             IGeneralVariableServices genralVariableServices)
        {
            _centerRepositoryQuery = centerRepositoryQuery;
            _genralVariableServices = genralVariableServices;
        }

        public async Task<CenterInLoginPageDto> GetCenterByHostName(string hostname)
        {
            var center = await _centerRepositoryQuery.GetCenterByHostName(hostname);
            if (center == null)
                return null;

            var general = await _genralVariableServices.FillStandardVariables();

            return DtoMappers.MapCenterInLogginPageDto(general, center);
        }
        public async Task<List<CenterInLoginPageDto>> GetCentersWithTitleAndCity(int centerGroupId)
        {
            var center = await _centerRepositoryQuery.GetCentersWithTitleAndCity(centerGroupId);
            if (center == null)
                return null;

            var general = await _genralVariableServices.FillStandardVariables();

            return DtoMappers.MapCentersWithTitleAndCity(general, center);
        }
        public async Task<CenterFullDto> GetCenterById(int centerId)
        {
            var center = await _centerRepositoryQuery.GetCenterById(centerId);
            if (center == null)
                throw new AppException("این مرکز موجود نیست");

            var general = await _genralVariableServices.FillStandardVariables();

            return DtoMappers.MapCenterDto(general, center);
        }
        public Task<List<int>> GetTenantsByCenterIds(List<int> centerIds)
        {
            return _centerRepositoryQuery.GetTenantsByCenterIds(centerIds);
        }

        public async Task<byte[]> GetCenterLogoById(int centerId)
        {
            return await _centerRepositoryQuery.GetCenterLogoById(centerId);
        }

        public Task<byte[]> GetCenterLogoByTenantId(int tenantId)
        {
            return _centerRepositoryQuery.GetCenterLogoByTenantId(tenantId);
        }

        public Task<List<int>> GetTenantIdsByCenterGroup(int centerGroupId)
        {
            return _centerRepositoryQuery.GetTenantIdsByCenterGroup(centerGroupId);
        }

        public Task<List<CenterFullDto>> GetCenterList()
        {
            return _centerRepositoryQuery.GetCenterList();
        }

        public async Task<List<CenterFullDto>> GetCenters(int stateId, int centerGroupId)
        {
            var cities = await _genralVariableServices.CitiesByStateId(stateId);
            var cityIds = cities.SelectListDtos.Select(s => s.Value).ToList();
            return await _centerRepositoryQuery.GetCenters(centerGroupId, cityIds);
        }

        public Task<List<SelectListItemDto>> GetCentersName()
        {
            return _centerRepositoryQuery.GetCentersName();
        }

        public async Task<List<CenterFullDto>> GetAllCenters()
        {
            var general = await _genralVariableServices.FillStandardVariables();
            var centers = await _centerRepositoryQuery.GetAllCenters();

            return DtoMappers.MapCenterDtos(general, centers);
        }

        public Task<SelectListItemDto> GetCenterGroupByCenterId(int centerId)
        {
            return _centerRepositoryQuery.GetCenterGroupByCenterId(centerId);
        }

        public Task<List<CenterGroupDto>> GetCenterGroupByCenterIds(List<int> centerIds)
        {
            return _centerRepositoryQuery.GetCenterGroupByCenterIds(centerIds);
        }
    }
}

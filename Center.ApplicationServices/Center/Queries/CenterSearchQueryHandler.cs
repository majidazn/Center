using Center.ApplicationServices.CommonServices.AccountService;
using Center.ApplicationServices.CommonServices.GeneralVariableServices;
using Center.ApplicationServices.Infrastrutures.Mapper;
using Center.Common.Dtos;
using Center.Common.Extensions;
using Center.Domain.CenterAggregate.Dtos.Center;
using Center.Domain.CenterAggregate.Repositories;
using Center.Domain.CenterVariableAggregate.Enums;
using Center.Domain.CenterVariableAggregate.Repositories;
using Core.Common.ViewModels.KendoGrid;
using Core.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Queries
{
    public class CenterSearchQueryHandler : IRequestHandler<CenterSearchQuery, GridResultDto<CenterResultSearchDto>>
    {
        private readonly ICenterRepositoryQuery _centerRepositoryQuery;
        private readonly ICenterVariableRepositoryQuery _centerVariableRepositoryQuery;
        private readonly IAccountServices _accountServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGeneralVariableServices _generalVariableSerivces;

        public CenterSearchQueryHandler(ICenterRepositoryQuery centerRepositoryQuery, IAccountServices accountServices,
            IHttpContextAccessor httpContextAccessor, IGeneralVariableServices generalVariableSerivces,
             ICenterVariableRepositoryQuery centerVariableRepositoryQuery
            )
        {
            _centerRepositoryQuery = centerRepositoryQuery;
            _centerVariableRepositoryQuery = centerVariableRepositoryQuery;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
            _generalVariableSerivces = generalVariableSerivces;
        }

        public async Task<GridResultDto<CenterResultSearchDto>> Handle(CenterSearchQuery request, CancellationToken cancellationToken)
        {
            var model = request.SearchDto;
            var centers = _centerRepositoryQuery.GetCenters();
            var roleId = _accountServices.GetHighestRoles(_httpContextAccessor);

            var centerGroups = await _centerVariableRepositoryQuery.CachedCenterVariables((int)CenterVariableType.CenterGroup);
            var centerTitles = await _centerVariableRepositoryQuery.CachedCenterVariables((int)CenterVariableType.CenterTitle);

            if (!string.IsNullOrEmpty(model.Name))
                centers = centers.Where(it => it.Name.Contains(model.Name));

            if (model.Title > 0)
                centers = centers.Where(it => it.Title.Equals(model.Title));

            if (model.CenterGroup > 0)
                centers = centers.Where(it => it.CenterGroup.Equals(model.CenterGroup));

            if (model.City > 0)
                centers = centers.Where(it => it.City.Equals(model.City));

            var count = await centers.CountAsync();
            var result = await centers.ApplyPagingAsync(model.gridState, count);

            var variables = await _generalVariableSerivces.FillStandardVariables();

            var lstCenterSearchDto = new List<CenterResultSearchDto>();
            string cityName, stateName = string.Empty;
            foreach (var center in result.data)
            {
                var centerTitleItem = centerTitles.FirstOrDefault(q => q.CenterVariableId == center.Title);
                var centerGroupItem = centerGroups.FirstOrDefault(q => q.CenterVariableId == center.CenterGroup);
                (cityName, stateName) = DtoMappers.MapCenterInSearchResultDto(variables, center.City).Result;

                lstCenterSearchDto.Add(new CenterResultSearchDto
                {
                    Name = center.Name,
                    EnName = center.EnName,
                    Address = center.Address,
                    CenterGroup = center.CenterGroup,
                    CenterId = center.Id,
                    City = center.City,
                    FinanchialCode = center.FinanchialCode,
                    HostName = center.HostName,
                    NationalCode = center.NationalCode,
                    SepasCode = center.SepasCode,
                    TenantId = center.TenantId,
                    Title = center.Title,
                    ValidFrom = center.ValidFrom,
                    ValidTo = center.ValidTo,
                    ZipCode = center.ZipCode,
                    ValidFromPersian = center.ValidFrom.ToShamsiDateWithPersianNumber(),
                    ValidToPersian = center.ValidTo != null ? center.ValidTo.ToShamsiDateWithPersianNumber() : "",
                    CenterGroupString = centerGroupItem != null ? centerGroupItem.Name : "",
                    TitleString = centerTitleItem != null ? centerTitleItem.Name : "",
                    CityString = cityName,
                    StateString = stateName
                    //CenterGroupString= variables.Where(x => x.SystemCodeId == (int)SystemCodes.CenterGroup).FirstOrDefault().SelectListDtos.FirstOrDefault(x => x.Value == center.CenterGroup).Text,
                });
            }

            var gridModel = new GridResultDto<CenterResultSearchDto>()
            {
                data = lstCenterSearchDto,
                GridState = model.gridState,
                total = result.total,
            };

            return gridModel;
        }
    }
}

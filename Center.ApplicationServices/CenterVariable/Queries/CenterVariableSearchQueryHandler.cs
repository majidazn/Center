using Center.ApplicationServices.CenterVariable.Queries;
using Center.ApplicationServices.CommonServices.AccountService;
using Center.ApplicationServices.CommonServices.GeneralVariableServices;
using Center.Common.Dtos;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using Center.Domain.CenterVariableAggregate.Enums;
using Center.Domain.CenterVariableAggregate.Repositories;
using Core.Common.ViewModels.KendoGrid;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Application.Queries
{
    public class CenterVariableSearchQueryHandler : IRequestHandler<CenterVariableSearchQuery, GridResultDto<CenterVariableResultSearchDto>>
    {

        private readonly ICenterVariableRepositoryQuery _centerVariableRepositoryQuery;


        public CenterVariableSearchQueryHandler(ICenterVariableRepositoryQuery centerVariableRepositoryQuery, IAccountServices accountServices,
            IHttpContextAccessor httpContextAccessor, IGeneralVariableServices generalVariableSerivces)
        {
            _centerVariableRepositoryQuery = centerVariableRepositoryQuery;
        }

        public async Task<GridResultDto<CenterVariableResultSearchDto>> Handle(CenterVariableSearchQuery request, CancellationToken cancellationToken)
        {
            var model = request.SearchDto;

            var centerVariables = _centerVariableRepositoryQuery.GetCenterVariables();

            centerVariables = NameFilter(model, centerVariables);
            centerVariables = EnNameFilter(model, centerVariables);
            centerVariables = CodeFilter(model, centerVariables);
            centerVariables = InternalUsageFilter(model, centerVariables);
            centerVariables = ParentFilter(model, centerVariables);

            var count = await centerVariables.CountAsync();

            try
            {
                var result = await centerVariables
                    .ApplyPagingAsync(model.gridState, count);

                var lstCenterVariableSearchDto = new List<CenterVariableResultSearchDto>();
                foreach (var centerVariable in result.data)
                    lstCenterVariableSearchDto.Add(new CenterVariableResultSearchDto
                    {
                        Name = centerVariable.Name,
                        EnName = centerVariable.EnName,
                        ParentId = centerVariable.ParentId.Value,
                        Sort = centerVariable.Sort,
                        Code = centerVariable.Code,
                        InternalUsage = centerVariable.InternalUsage,
                        CenterVariableId = centerVariable.Id.Value,
                        InternalUsageString = Enum.GetName(typeof(InternalUsage), centerVariable.InternalUsage)
                        //CenterGroupString= variables.Where(x => x.SystemCodeId == (int)SystemCodes.CenterGroup).FirstOrDefault().SelectListDtos.FirstOrDefault(x => x.Value == center.CenterGroup).Text,
                    });

                var gridModel = new GridResultDto<CenterVariableResultSearchDto>()
                {
                    data = lstCenterVariableSearchDto,
                    GridState = model.gridState,
                    total = result.total,
                };

                return gridModel;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> ParentFilter(SearchCenterVariableDto model, IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> centerVariables)
        {
            if (model.ParentId > 0)
                centerVariables = centerVariables.Where(it => it.ParentId == (model.ParentId));
            return centerVariables;
        }

        private static IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> InternalUsageFilter(SearchCenterVariableDto model, IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> centerVariables)
        {
            if (model.InternalUsage > 0)
                centerVariables = centerVariables.Where(it => it.InternalUsage.Equals(model.InternalUsage));
            return centerVariables;
        }

        private static IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> CodeFilter(SearchCenterVariableDto model, IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> centerVariables)
        {
            if (model.Code != null)
                centerVariables = centerVariables.Where(it => it.Code.Equals(model.Code));
            return centerVariables;
        }

        private IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> EnNameFilter(SearchCenterVariableDto model, IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> centerVariables)
        {
            if (!string.IsNullOrEmpty(model.EnName))
                centerVariables = centerVariables.Where(it => it.EnName.Contains(model.EnName));
            return centerVariables;
        }

        private IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> NameFilter(SearchCenterVariableDto model, IQueryable<Domain.CenterVariableAggregate.Entities.CenterVariable> centerVariables)
        {
            if (!string.IsNullOrEmpty(model.Name))
                centerVariables = centerVariables.Where(it => it.Name.Contains(model.Name));
            return centerVariables;
        }
    }
}

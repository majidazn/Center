using Center.Common.Dtos;
using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using MediatR;

namespace Center.ApplicationServices.CenterVariable.Queries
{
    public class CenterVariableSearchQuery : IRequest<GridResultDto<CenterVariableResultSearchDto>>
    {
        public SearchCenterVariableDto SearchDto { get; set; }
    }
}

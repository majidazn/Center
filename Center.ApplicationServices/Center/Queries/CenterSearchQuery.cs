using Center.Common.Dtos;
using Center.Domain.CenterAggregate.Dtos.Center;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Queries
{
    public class CenterSearchQuery : IRequest<GridResultDto<CenterResultSearchDto>>
    {
        public SearchDto SearchDto { get; set; }
    }
}

using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.Commands.SortCenterVariableCommand
{
    public class SortCenterVariableCommand : IRequest<bool>
    {
        #region Constructors
        public SortCenterVariableCommand(List<SortDto> sortDtos)
        {
            SortDtos = sortDtos;
        }
        #endregion

        #region Properties
        //public int CenterVariableId { get; set; }
        //public int Sort { get; set; }
        //public int ParentId { get; set; }
        public List<SortDto> SortDtos { get; private set; }

        #endregion
    }
}

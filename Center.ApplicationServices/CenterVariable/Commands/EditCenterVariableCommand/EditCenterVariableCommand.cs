using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.Commands.EditCenterVariableCommand
{
    public class EditCenterVariableCommand : IRequest<bool>
    {
        #region Constructors
        public EditCenterVariableCommand(CreateCenterVariableDto editCenterVariableDto)
        {
            EditCenterVariableDto = editCenterVariableDto;
        }
        #endregion

        #region Properties
        public CreateCenterVariableDto EditCenterVariableDto { get; private set; }
        #endregion
    }
}

using Center.Domain.CenterVariableAggregate.Dtos.CenterVariable;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.Commands.CreateCenterVariableCommand
{
    public class CreateCenterVariableCommand : IRequest<long>
    {
        #region Constructors
        public CreateCenterVariableCommand(CreateCenterVariableDto  createCenterVariableDto)
        {
            CreateCenterVariableDto = createCenterVariableDto;
        }

        #endregion

        #region Properties

        public CreateCenterVariableDto CreateCenterVariableDto { get; private set; }

        #endregion
    }
}

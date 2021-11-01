using Center.Domain.CenterAggregate.Dtos.Center;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Commands.EditCenterCommand
{
    public class EditCenterCommand : IRequest<bool>
    {
        #region Constructors
        public EditCenterCommand(CreateCenterDto editCenterDto)
        {
            EditCenterDto = editCenterDto;
        }
        #endregion

        #region Properties
        public CreateCenterDto EditCenterDto { get; private set; }
        #endregion
    }
}

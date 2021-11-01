using Center.Domain.CenterAggregate.Dtos.Center;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Commands.CreateCenterCommand
{
    public class CreateCenterCommand : IRequest<long>
    {

        #region Constructors
        public CreateCenterCommand(CreateCenterDto createCenterDto)
        {
            CreateCenterDto = createCenterDto;
        }

        #endregion

        #region Properties

        public CreateCenterDto CreateCenterDto { get; private set; }

        #endregion
    }
}

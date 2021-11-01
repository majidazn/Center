using Center.Domain.SharedKernel.Dtos.Activity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Activity.Commands.CreateActivityCommand
{
    public class CreateActivityCommand : IRequest<long>
    {
        #region Constructors
        public CreateActivityCommand(ActivityDto activityDto)
        {
            ActivityDto = activityDto;
        }

        #endregion

        #region Properties
        public ActivityDto ActivityDto { get; private set; }
        #endregion
    }
}

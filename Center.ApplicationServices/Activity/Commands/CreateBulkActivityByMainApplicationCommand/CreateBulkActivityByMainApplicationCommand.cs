using Center.Domain.SharedKernel.Dtos.Activity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Activity.Commands.CreateBulkActivityByMainApplicationCommand
{
    public class CreateBulkActivityByMainApplicationCommand : IRequest<long>
    {

        #region Constructors
        public CreateBulkActivityByMainApplicationCommand(ActivityDto activityDto)
        {
            ActivityDto = activityDto;
        }

        #endregion

        #region Properties

         public ActivityDto ActivityDto { get; private set; }
        //public int ParentId { get; set; }
        //public int CenterId { get; set; }

        #endregion
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Activity.Commands.RemoveActivityCommand
{
    public class RemoveActivityCommand : IRequest<bool>
    {
        #region Constructors
        public RemoveActivityCommand(int activityId)
        {
            ActivityId = activityId;
        }
        #endregion

        #region Properties
        public int ActivityId { get; set; }

        #endregion
    }
}

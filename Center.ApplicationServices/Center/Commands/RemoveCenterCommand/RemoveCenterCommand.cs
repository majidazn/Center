using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Center.Commands.RemoveCenterCommand
{
    public class RemoveCenterCommand : IRequest<bool>
    {
        #region Constructors
        public RemoveCenterCommand(int centerId)
        {
            CenterId = centerId;
        }
        #endregion

        #region Properties
        public int CenterId { get;  set; }

        #endregion
    }
}

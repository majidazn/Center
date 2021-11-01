using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.CenterVariable.Commands.RemoveCenterVariableCommand
{
    public class RemoveCenterVariableCommand : IRequest<bool>
    {
        #region Constructors
        public RemoveCenterVariableCommand(int centerVariableId)
        {
            CenterVariableId = centerVariableId;
        }
        #endregion

        #region Properties
        public int CenterVariableId { get; set; }

        #endregion
    }
}

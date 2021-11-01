using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Dtos.CenterVariable
{
    public class CenterVariablesWithActiveApplicationsDto
    {
        public int CenterId { get; set; }
        public int CenterVariableId { get; set; }
        public int ActivityId { get; set; }
        public bool IsAssined { get; set; }
        public int TenantId { get; set; }
        public string CenterVariableName { get; set; }
    }
}

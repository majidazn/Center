using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Dtos.CenterVariable
{
    public class SortDto
    {
        public int CentervariableId { get; set; }
        public int ParentId { get; set; }
        public int Sort { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Dtos.CenterVariable
{
    public class CenterVariableCodeDto
    {
        public int? Code { get; set; }
        public int CenterVariableId { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public int? ParentId { get; set; }
    }
}

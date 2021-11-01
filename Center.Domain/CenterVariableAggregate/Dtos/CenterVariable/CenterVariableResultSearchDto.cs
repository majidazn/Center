using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Dtos.CenterVariable
{
    public class CenterVariableResultSearchDto
    {
        public int CenterVariableId { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public int? ParentId { get; set; }
        // public CenterVariable Parent { get; private set; }
        public int? Code { get; set; }
        public int Sort { get; set; }
        public int InternalUsage { get; set; }
        public string InternalUsageString { get; set; }
        public string Address { get; set; }
        public string ShortKey { get; set; }
        public byte[]? Icon { get; set; }
    }
}

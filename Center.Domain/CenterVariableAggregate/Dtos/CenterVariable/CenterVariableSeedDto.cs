using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Dtos.CenterVariable
{
    public class CenterVariableSeedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public int Code { get; set; }
        public int InternalUsage { get; set; }
        public int Sort { get; set; }
        public EntityStateType Status { get; set; }
        public int TenantId { get; set; }
        public int ParentId { get; set; }
    }
}

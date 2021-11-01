using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.SharedKernel.Dtos.Activity
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public int CenterVariableId { get; set; }
        public string CenterVariableName { get; set; }
        public int CenterId { get; set; }
        public int ParentId { get; set; }
        public int TenantId { get; set; }
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset? ValidTo { get; set; }
        public string ValidFromPersian { get; set; }
        public string ValidToPersian { get; set; }
    }
}

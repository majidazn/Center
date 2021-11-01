using Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.SharedKernel.Dtos.Activity
{
    public class ApplicationDto
    {
        public int CenterId { get; set; }
        public int CenterVariableId { get; set; }
        public int ActivityId { get; set; }
        public int TenantId { get; set; }
        public string CenterVariableName { get; set; }
        public string CenterVariableEnName { get; set; }
        public string ShortKey { get; set; }
        public string Address { get; set; }
        public int? Code { get; set; }
        public int Sort { get; set; }
        public int CenterVariableParentId { get; set; }
        public byte[] Icon { get; set; }
        public EntityStateType Status { get; set; }
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset? ValidTo { get; set; }
    }
}

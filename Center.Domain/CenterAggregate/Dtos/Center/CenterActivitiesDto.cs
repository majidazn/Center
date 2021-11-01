using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Dtos.Center
{
    public class CenterActivitiesDto
    {
        public int CenterId { get; set; }
        public int TenantId { get; set; }
        public int ParentId { get; set; }
        public bool IsAssigned { get; set; }
        public int InternalUsage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

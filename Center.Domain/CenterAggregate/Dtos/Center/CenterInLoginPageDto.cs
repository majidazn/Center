using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Dtos.Center
{
    public class CenterInLoginPageDto
    {
        public int CenterId { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public string HostName { get; set; }
        public byte[] Logo { get; set; }
        public string CenterGroup { get; set; }
    }
}
    
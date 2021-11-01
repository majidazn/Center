using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Dtos.CenterTelecom
{
    public class CreateTelecomDto
    {
        public  int CreateTelecomId { get;  set; }
        public int CenterId { get;  set; }
        public int Type { get;  set; }
        public int Section { get;  set; }
        public string TellNo { get;  set; }
        public string Comment { get;  set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Center.Common.Enums.Enums;

namespace Center.Domain.CenterAggregate.Dtos.CenterRefer
{
    public class CenterReferDto
    {
        public int CenterReferId { get; set; }
        public string Address { get; set; }
        public int CenterId { get; set; }
        public byte Status { get; set; }
        public AddressType UrlType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Dtos.ElectronicAddress
{
    public class CreateElectronicAddressDto
    {
        public int Id { get; set; }
        public int CenterId { get; set; }
        public int EType { get; set; }
        public string EAddress { get; set; }
    }
}

using Center.Domain.CenterAggregate.Dtos.CenterTelecom;
using Center.Domain.CenterAggregate.Dtos.ElectronicAddress;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Dtos.Center
{
    public class CenterFullDto
    {
        public int CenterId { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public int Title { get; set; }
        public int CenterGroup { get; set; }
        public int City { get; set; }
        public int CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string HostName { get; set; }
        public byte[] Logo { get; set; }
        public string NationalCode { get; set; }
        public string FinanchialCode { get; set; }
        public string SepasCode { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int TenantId { get;  set; }
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset? ValidTo { get; set; }
        public string ValidtoPersian { get; set; }
        public string ValidFromPersian { get; set; }
        public IEnumerable<CreateTelecomDto> Telecoms { get; set; }
        public IEnumerable<CreateElectronicAddressDto> ElectronicAddresses { get; set; }
    }
}

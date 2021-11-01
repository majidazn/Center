using Common.ViewModels.KendoGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Dtos.Center
{
    public class SearchDto
    {

        public int CenterId { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public int Title { get; set; }
        public int CenterGroup { get; set; }
        public int City { get; set; }
        public string HostName { get; set; }
        //public IFormFile Logo { get; set; }
        public string NationalCode { get; set; }
        public string FinanchialCode { get; set; }
        public string SepasCode { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int TenantId { get; set; }
        public DateTimeOffset? ValidFrom { get; set; }
        public DateTimeOffset? Validto { get; set; }
        //public List<CreateTelecomDto> Telecoms { get; set; }
        //public List<CreateElectronicAddressDto> ElectronicAddresses { get; set; }
        public GridState gridState { get; set; }
    }
}

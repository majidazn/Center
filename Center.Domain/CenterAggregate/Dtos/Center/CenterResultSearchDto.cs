using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterAggregate.Dtos.Center
{
    public class CenterResultSearchDto
    {
        public int CenterId { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public int Title { get; set; }
        public string TitleString { get; set; }
        public int CenterGroup { get; set; }
        public string CenterGroupString { get; set; }
        public int City { get; set; }
        public string CityString { get; set; }
        public string StateString { get; set; }
        public string HostName { get; set; }
        //public IFormFile Logo { get; set; }
        public string NationalCode { get; set; }
        public string FinanchialCode { get; set; }
        public string SepasCode { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int TenantId { get; set; }
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset? ValidTo { get; set; }
        public string ValidFromPersian { get; set; }
        public string ValidToPersian { get; set; }
    }
}

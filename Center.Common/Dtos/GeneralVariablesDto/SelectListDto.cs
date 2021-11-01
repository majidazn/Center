using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Common.Dtos.GeneralVariablesDto
{
    public class SelectListDto
    {
        [JsonIgnore]
        public long? ParentId { get; set; }

        [JsonIgnore]
        public long? VariableId { get; set; }

        public int SystemCodeId { get; set; }
        public string SystemCodeName { get; set; }
        public List<SelectListItemDto> SelectListDtos { get; set; }
    }
}

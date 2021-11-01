using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Common.Dtos.GeneralVariablesDto
{
    public class SelectListItemDto
    {
        public long Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }

        [JsonIgnore]
        public long? Parent { get; set; }
    }
}

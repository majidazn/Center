using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Common.Dtos.GeneralVariablesDto
{
    public class StandardVariablesDto
    {
        public long StandardVariableId { get; set; }

        public int? Type { get; set; }

        public int? DomainId { get; set; }

        public long? Parent { get; set; }

        public string Name { get; set; }

        public string EnglishName { get; set; }

        public int Sort { get; set; }

        public string Code { get; set; }

        public bool Status { get; set; }

        public StandardVariablesDto Child { get; set; }

        public int SystemCodeId { get; set; }
    }

    public class StandardVariablesDtoModel
    {
        public List<StandardVariablesDto> data { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

}

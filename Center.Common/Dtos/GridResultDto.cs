using Common.ViewModels.KendoGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Common.Dtos
{
    public class GridResultDto<T>
    {
        public List<T> data { get; set; }
        public decimal total { get; set; }
        public GridState GridState { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Domain.CenterVariableAggregate.Enums
{
    public enum InternalUsage : byte
    {
        [Display(Name = "")]
        NoData = 0,

        [Display(Name = "پیوند")]
        EPD = 3,

        [Display(Name = "مراکز")]
        Centers = 4,

        [Display(Name = "بیمار")]
        Patient = 5,

        [Display(Name = "پیوند-مراکز")]
        CentersEPD = 6,

        [Display(Name = "گزارش عملکرد")]
        WorkHour = 7
    }
}

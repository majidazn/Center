using System.ComponentModel.DataAnnotations;

namespace Center.Domain.CenterAggregate.Enums
{
    public enum TelKind : int
    {
        [Display(Name = "موبایل")]
        Mobile = 1486,

        [Display(Name = "منزل")]
        Home = 1487,

        [Display(Name = "محل کار")]
        Work = 2364,

        [Display(Name = "بستگان-ثابت")]
        RelativesFixed = 2365,

        [Display(Name = "بستگان-موبایل")]
        RelativesMobile = 2366,

    }

}

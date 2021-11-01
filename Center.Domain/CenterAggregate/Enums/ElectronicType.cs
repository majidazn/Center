using System.ComponentModel.DataAnnotations;

namespace Center.Domain.CenterAggregate.Enums
{
    public enum ElectronicType : byte
    {
        [Display(Name = "سایر")]
        Others = 0,

        [Display(Name = "تلگرام")]
        Telegram = 1,

        [Display(Name = "اینستاگرام")]
        Instagram = 2,

        [Display(Name = "لاین")]
        Line = 3,

        [Display(Name = "واتس اپ")]
        WhatsApp = 4,

        [Display(Name = "توییتر")]
        Twitter = 5,

        [Display(Name = "فیسبوک")]
        Facebook = 6,

        [Display(Name = "ایمو")]
        Imo = 7,

        [Display(Name = "پست الکترونیک")]
        Email = 8,

        [Display(Name = "لینکدین")]
        LinkedIn = 9
    }
}

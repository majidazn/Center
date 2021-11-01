using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Center.Common.Enums
{
    public static class Enums
    {
        public static string DisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
        public enum AddressType : byte
        {
            Default = 0,

            [Display(Name = "کنسول پزشک")]
            DoctorConsole = 1,

            [Display(Name = "کنسول بیمار")]
            PatientConsole = 2
        }
    }
}

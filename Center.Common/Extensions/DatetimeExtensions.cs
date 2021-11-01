using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Common.Extensions
{
    public static class DatetimeExtensions
    {
        public static string ToShamsiDateWithPersianNumber(this DateTimeOffset dt)
        {
            //if (dt == DateTime.MinValue)
            //    return string.Empty;
            //var pc = new PersianCalendar();
            //return
            //    $"{pc.GetYear(dt)}/{pc.GetMonth(dt).ToString().PadLeft(2, '0')}/{pc.GetDayOfMonth(dt).ToString().PadLeft(2, '0')}";

            return dt.ToString();
        }

        public static string ToShamsiDateWithPersianNumber(this DateTimeOffset? date)
        {
            //if (date == null)
            //    return string.Empty;
            //var dt = date.Value;
            //if (dt == DateTime.MinValue)
            //    return string.Empty;
            //var pc = new PersianCalendar();
            //return
            //    $"{pc.GetYear(dt)}/{pc.GetMonth(dt).ToString().PadLeft(2, '0')}/{pc.GetDayOfMonth(dt).ToString().PadLeft(2, '0')}";

            return date.ToString();
        }

        public static string ToShamsiDateWithPersianNumber(this DateTime dt)
        {
            if (dt == DateTime.MinValue)
                return string.Empty;
            var pc = new PersianCalendar();
            return
                $"{pc.GetYear(dt)}/{pc.GetMonth(dt).ToString().PadLeft(2, '0')}/{pc.GetDayOfMonth(dt).ToString().PadLeft(2, '0')}";
        }

        public static string ToShamsiDateWithPersianNumber(this DateTime? date)
        {
            if (date == null)
                return string.Empty;
            var dt = date.Value;
            if (dt == DateTime.MinValue)
                return string.Empty;
            var pc = new PersianCalendar();
            return
                $"{pc.GetYear(dt)}/{pc.GetMonth(dt).ToString().PadLeft(2, '0')}/{pc.GetDayOfMonth(dt).ToString().PadLeft(2, '0')}";
        }

        public static DateTime ToDateTime(this string persianDateTime)
        {
            if (string.IsNullOrEmpty(persianDateTime))
                return DateTime.MinValue;
            persianDateTime = persianDateTime.Replace('-', '/');
            persianDateTime = persianDateTime.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");

            var hour = 0;
            var min = 0;
            var seconds = 0;
            var dateTimeParts = persianDateTime.Split(new string[] { " " }, StringSplitOptions.None);
            if (dateTimeParts.Length > 1)
            {
                if (dateTimeParts[1] != null)
                {
                    var hm = dateTimeParts[1].Split(new[] { ':' });
                    hour = System.Convert.ToInt32(hm[0]);
                    min = System.Convert.ToInt32(hm[1]);
                }
            }

            var dateParts = dateTimeParts[0].Split(new[] { '/' }).Select(d => int.Parse(d)).ToArray();

            return new PersianCalendar().ToDateTime(dateParts[0], dateParts[1], dateParts[2],
                hour, min, seconds, 0);
        }
    }
}

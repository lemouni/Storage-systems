using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Utility
{
    public static class MetodExtations
    {
        public static string ToRial(this double value)
        {
            return value.ToString("#,0");
            // return value.ToString("N0");
        }
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");
        }
        public static DateTime ToMiladi(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, new PersianCalendar());
        }
    }
}

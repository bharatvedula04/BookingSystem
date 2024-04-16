using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Utils
{
    public static class Extensions
    {
        public static DateTime GetFormattedTime(this DateTime dateTime,int month, int day, int hours, int minutes)
        {
            //if(hours < 9 || hours > 17 || (hours > 16 && minutes > 30))
            //    return DateTime.MinValue;
            //else if(minutes != 30 || minutes != 0)
            //    return DateTime.MinValue;
            return new DateTime(
                dateTime.Year,
                month,
                day,
                hours,
                minutes,0);
        }
    }
}

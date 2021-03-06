using System;
using System.Collections.Generic;
using System.Text;

namespace Afry.TollCalculator.Core.Extension
{
    public static class DateTimeExtension
    {
        public static bool IsBetween(this DateTime dateTime, TimeSpan start, TimeSpan end)
        {
            var time = dateTime.TimeOfDay;

            if (start <= end)
            {
                return time >= start && time <= end;
            }

            return time >= start || time <= end;
        }
    }
}

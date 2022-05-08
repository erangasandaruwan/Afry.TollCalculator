using System;
using System.Collections.Generic;
using System.Text;

namespace Afry.TollCalculator.Core.Extension
{
    public static class DateTimeExtension
    {
        public static bool IsBetween(this DateTime now, TimeSpan start, TimeSpan end)
        {
            var time = now.TimeOfDay;
            if (start <= end)
            {
                return time >= start && time <= end;
            }
            return time >= start || time <= end;
        }
    }
}

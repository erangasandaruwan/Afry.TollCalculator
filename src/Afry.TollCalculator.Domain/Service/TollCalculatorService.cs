using Afry.TollCalculator.Core.Extension;
using Afry.TollCalculator.Core.Model;
using System;
using System.Linq;

namespace Afry.TollCalculator.Domain.Service
{
    public class TollCalculatorService : ITollCalculatorService
    {
        public int GetTollFee(Vehicle vehicle, DateTime[] dates)
        {
            int totalFee = 0;
            if (dates.Length > 0)
            {
                var datesSorted = dates.OrderBy(d => d).ToList();
                DateTime intervalStart = datesSorted[0];

                foreach (DateTime date in datesSorted)
                {
                    int nextFee = GetTollFeeByDate(date, vehicle);
                    int tempFee = GetTollFeeByDate(intervalStart, vehicle);

                    TimeSpan span = date - intervalStart;
                    int diffInMinutes = (int)span.TotalMinutes;

                    if (diffInMinutes <= 60)
                    {
                        if (totalFee > 0)
                        {
                            totalFee -= tempFee;
                        };

                        if (nextFee >= tempFee)
                        {
                            tempFee = nextFee;
                        };

                        totalFee += tempFee;
                    }
                    else
                    {
                        totalFee += nextFee;
                    }

                    intervalStart = date;
                }
            }

            if (totalFee > 60) 
            { 
                totalFee = 60; 
            };
            
            return totalFee;
        }

        private int GetTollFeeByDate(DateTime dateTime, Vehicle vehicle)
        {
            if (dateTime.IsTollFree() || vehicle.IsTollFree()) 
            { 
                return 0; 
            }

            // 6.00 - 6.30 am
            if (dateTime.IsBetween(new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 59)))
                return 8;
            // 6.30 - 7.00 am
            else if (dateTime.IsBetween(new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 59)))
                return 13;
            // 7.00 - 8.00 am
            else if (dateTime.IsBetween(new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 59)))
                return 18;
            // 8.00 - 8.30 am
            else if (dateTime.IsBetween(new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 59)))
                return 13;
            // 8.30 - 3.00 pm
            else if (dateTime.IsBetween(new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 59)))
                return 8;
            // 3.00 - 3.30 pm
            else if (dateTime.IsBetween(new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 59)))
                return 13;
            // 3.30 - 5.00 pm
            else if (dateTime.IsBetween(new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 59)))
                return 18;
            // 5.00 - 6.00 pm
            else if (dateTime.IsBetween(new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 59)))
                return 13;
            // 6.00 - 6.30 pm
            else if(dateTime.IsBetween(new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 59)))
                return 8;
            else
                return 0;
        }
    }
}

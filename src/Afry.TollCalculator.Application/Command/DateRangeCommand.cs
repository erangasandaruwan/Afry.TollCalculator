using Afry.TollCalculator.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afry.TollCalculator.Application.Command
{
    public class DateRangeCommand : ITollCalculationCommand
    {
        public Vehicle Vehicle { get; set; }
        public DateTime[] TollDates { get; set; }
    }
}

using Afry.TollCalculator.Core.Model;
using System;

namespace Afry.TollCalculator.Application.Command
{
    public class DateCommand : ITollCalculationCommand
    {
        public Vehicle Vehicle { get; set; }
        public DateTime TollDate { get; set; }
    }
}

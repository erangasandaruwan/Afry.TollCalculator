using Afry.TollCalculator.Core.Model;
using System;

namespace Afry.TollCalculator.Domain.Service
{
    public interface ITollCalculatorService
    {
        int GetTollFee(Vehicle vehicle, DateTime[] dates);
    }
}

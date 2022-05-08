using Afry.TollCalculator.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afry.TollCalculator.Core.Extension
{
    public static class VehicleExtension
    {
        public static bool IsTollFree(this Vehicle vehicle)
        {
            return vehicle.IsTollable();
        }
    } 
}

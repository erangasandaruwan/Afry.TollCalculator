using System;
using System.Collections.Generic;
using System.Text;

namespace Afry.TollCalculator.Core.Model
{
    public class MotorBike : Vehicle
    {
        public override bool IsTollable()
        {
            return false;
        }
    }
}

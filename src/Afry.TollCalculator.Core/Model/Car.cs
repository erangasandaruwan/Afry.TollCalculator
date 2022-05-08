using System;
using System.Collections.Generic;
using System.Text;

namespace Afry.TollCalculator.Core.Model
{
    public class Car : Vehicle
    {
        public override bool IsTollable()
        {
            return true;
        }
    }
}

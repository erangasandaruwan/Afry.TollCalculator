using System;
using System.Collections.Generic;
using System.Text;

namespace Afry.TollCalculator.Core.Model
{
    public class Military : Vehicle
    {
        public override bool IsTollable()
        {
            return false;
        }
    }
}

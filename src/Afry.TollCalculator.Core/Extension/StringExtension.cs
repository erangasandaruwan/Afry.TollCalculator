using Afry.TollCalculator.Core.Model;
using System;

namespace Afry.TollCalculator.Core.Extension
{
    public static class StringExtension
    {
        public static Vehicle GetDefaultVehicle(this string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new Exception("Vehicle cannot be null or empty");

            if (text.ToUpper().Equals("CAR"))
                return new Car();
            else if (text.ToUpper().Equals("DIPLOMAT"))
                return new Diplomat();
            else if (text.ToUpper().Equals("EMERGENCY"))
                return new Emergency();
            else if (text.ToUpper().Equals("FOREIGN"))
                return new Foreign();
            else if (text.ToUpper().Equals("MILITARY"))
                return new Military();
            else if (text.ToUpper().Equals("MOTORBIKE"))
                return new MotorBike();
            else if (text.ToUpper().Equals("TRACTOR"))
                return new Tractor();
            else
                throw new Exception("Un-idetified vehicle type.");
        }
    }
}

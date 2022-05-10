namespace Afry.TollCalculator.Core.Model
{
    public abstract class Vehicle: ITollable
    {
        public abstract bool IsTollable();

        public string GetVehicleType()
        {
            return this.GetType().ToString();
        }
    }
}

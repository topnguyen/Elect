namespace Elect.Location.Models
{
    public class UnitOfLengthModel
    {
        public static UnitOfLengthModel Meter = new UnitOfLengthModel(ElectLocationConstants.MileToMeter);
        public static UnitOfLengthModel Kilometer = new UnitOfLengthModel(ElectLocationConstants.MileToKilometer);
        public static UnitOfLengthModel NauticalMile = new UnitOfLengthModel(ElectLocationConstants.NauticalMileToMile);
        public static UnitOfLengthModel Mile = new UnitOfLengthModel(1);
        private readonly double _fromMilesFactor;
        private UnitOfLengthModel(double fromMilesFactor)
        {
            _fromMilesFactor = fromMilesFactor;
        }
        public double ConvertFromMiles(double input)
        {
            return input * _fromMilesFactor;
        }
    }
}

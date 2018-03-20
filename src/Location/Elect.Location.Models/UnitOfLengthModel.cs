#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> UnitOfLengthModel.cs </Name>
//         <Created> 20/03/2018 2:14:32 PM </Created>
//         <Key> 81386382-8fbb-483d-aede-bd6d111c5a0f </Key>
//     </File>
//     <Summary>
//         UnitOfLengthModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

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
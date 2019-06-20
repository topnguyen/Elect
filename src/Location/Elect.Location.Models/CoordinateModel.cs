#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> CoordinateModel.cs </Name>
//         <Created> 20/03/2018 11:55:15 AM </Created>
//         <Key> b79cb5c8-1526-4b62-b5cb-b112bb2d9f7f </Key>
//     </File>
//     <Summary>
//         CoordinateModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Location.Models
{
    public class CoordinateModel
    {
        public CoordinateModel()
        {
        }

        public CoordinateModel(double longitude, double latitude)
        {
            Longitude = longitude;

            Latitude = latitude;
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        /// <summary>
        ///     Cluster Purpose 
        /// </summary>
        public int GroupNo { get; set; } = -1;

        /// <summary>
        ///     Route Sequence Purpose 
        /// </summary>
        public int SequenceNo { get; set; } = -1;
    }
}
#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ClusterModel.cs </Name>
//         <Created> 20/03/2018 11:55:53 AM </Created>
//         <Key> 80950e61-8bfe-48e9-81e6-175d5c01d2cd </Key>
//     </File>
//     <Summary>
//         ClusterModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Collections.Generic;

namespace Elect.Location.Models
{
    public class ClusterModel
    {
        public CoordinateModel CenterCoordinate { get; set; }

        public List<CoordinateModel> Coordinates { get; set; } = new List<CoordinateModel>();
    }
}
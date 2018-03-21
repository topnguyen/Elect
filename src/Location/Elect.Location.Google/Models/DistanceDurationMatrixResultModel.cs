#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DistanceDurationMatrixResultModel.cs </Name>
//         <Created> 20/03/2018 2:24:45 PM </Created>
//         <Key> f12203bd-a0e4-43b8-bf7a-479494a01a07 </Key>
//     </File>
//     <Summary>
//         DistanceDurationMatrixResultModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Location.Google.Models
{
    public class DistanceDurationMatrixResultModel
    {
        [JsonProperty(PropertyName = "origin_addresses")]
        public string[] OriginAddresses { get; set; }

        [JsonProperty(PropertyName = "destination_addresses")]
        public string[] DestinationAddresses { get; set; }

        [JsonProperty(PropertyName = "rows")]
        public DistanceMatrixRowModel[] Rows { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        #region Distance

        private int[,] _distanceMatrix;

        private int[,] GetDistanceMatrix()
        {
            var matrix = new int[OriginAddresses.Length, DestinationAddresses.Length];

            for (var i = 0; i < Rows.Length; i++)
            {
                for (var j = 0; j < Rows[i].Elements.Length; j++)
                {
                    matrix[i, j] = Rows[i].Elements[j].Distance.Value;
                }
            }

            return matrix;
        }

        /// <summary>
        ///     Get distance in meters from <see cref="OriginAddresses" />[i] to <see cref="DestinationAddresses" />[j] 
        /// </summary>
        public int[,] DistanceMatrix => _distanceMatrix ?? (_distanceMatrix = GetDistanceMatrix());

        #endregion

        #region Duration

        private int[,] _durationMatrix;

        private int[,] GetDurationMatrix()
        {
            var matrix = new int[OriginAddresses.Length, DestinationAddresses.Length];

            for (var i = 0; i < Rows.Length; i++)
            {
                for (var j = 0; j < Rows[i].Elements.Length; j++)
                {
                    matrix[i, j] = Rows[i].Elements[j].Duration.Value;
                }
            }

            return matrix;
        }

        /// <summary>
        ///     Get duration int second from <see cref="OriginAddresses" />[i] to <see cref="DestinationAddresses" />[j] 
        /// </summary>
        public int[,] DurationMatrix => _durationMatrix ?? (_durationMatrix = GetDurationMatrix());

        #endregion
    }
}
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

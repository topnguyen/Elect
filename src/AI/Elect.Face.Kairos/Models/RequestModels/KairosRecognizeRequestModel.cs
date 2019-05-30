using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.RequestModels
{
    public class KairosRecognizeRequestModel
    {
        /// <summary>
        ///     Publicly accessible URL or Base64 encoded photo.
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        ///     Gallery name, if not set will use the Default Gallery
        /// </summary>
        [JsonProperty("gallery_name")]
        public string GalleryName { get; set; }

        /// <summary>
        ///     Used to set the ratio of the smallest face we should look for in the photo.
        ///     Accepts a value between .015 (1:64 scale) and .5 (1:2 scale).
        ///     By default it is set at .015 (1:64 scale) if not specified.
        /// </summary>
        [JsonProperty("minHeadScale")]
        public double MinHeadScale { get; set; } = 0.015;

        /// <summary>
        ///     Used to determine a valid facial match / the score (1 mean 100% match).
        ///     By default it is set to 0.63.
        /// </summary>
        [JsonProperty("threshold")]
        public double Threshold { get; set; } = 0.63;

        /// <summary>
        ///     The maximum number of potential matches that are returned.
        ///     By default it is set to 10 if not supplied.
        /// </summary>
        [JsonProperty("max_num_results")]
        public int MaxNumResults { get; set; } = 10;
    }
}
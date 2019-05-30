using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.RequestModels
{
    public class KairosDetectRequestModel
    {
        /// <summary>
        ///     Publicly accessible URL or Base64 encoded photo.
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        ///     Used to set the ratio of the smallest face we should look for in the photo.
        ///     Accepts a value between .015 (1:64 scale) and .5 (1:2 scale).
        ///     By default it is set at .015 (1:64 scale) if not specified.
        /// </summary>
        [JsonProperty("minHeadScale")]
        public double MinHeadScale { get; set; } = 0.015;

        /// <summary>
        ///     Used to adjust the face detector.
        ///     If not specified the default of FRONTAL is used.
        ///     Note that these optional parameters are not reliable for face recognition, but may be useful for face detection uses.
        /// </summary>
        [JsonProperty("selector")]
        public string Selector { get; set; } = "FRONTAL";
    }
}
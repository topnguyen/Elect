using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.InfoModels
{
    public class KairosFaceIdModel
    {
        [JsonProperty("face_id")]
        public string FaceId { get; set; }
        
        [JsonProperty("enrollment_timestamp")]
        public string EnrollmentTimestamp { get; set; }
    }
}
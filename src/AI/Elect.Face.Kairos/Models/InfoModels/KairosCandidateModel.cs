using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.InfoModels
{
    public class KairosCandidateModel
    {
        [JsonProperty("confidence")]
        public double confidence { get; set; }

        [JsonProperty("enrollment_timestamp")]
        public string enrollment_timestamp { get; set; }
        
        [JsonProperty("face_id")]
        public string face_id { get; set; }

        [JsonProperty("subject_id")]
        public string subject_id { get; set; }
    }
}
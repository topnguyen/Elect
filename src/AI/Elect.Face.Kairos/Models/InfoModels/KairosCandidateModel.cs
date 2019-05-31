using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.InfoModels
{
    public class KairosCandidateModel
    {
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("enrollment_timestamp")]
        public string EnrollmentTimestamp { get; set; }
        
        [JsonProperty("face_id")]
        public string FaceId { get; set; }

        [JsonProperty("subject_id")]
        public string SubjectId { get; set; }
    }
}
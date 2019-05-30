using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.InfoModels
{
    public class KairosTransactionModel
    {
        [JsonProperty("confidence")]
        public string Confidence { get; set; }

        [JsonProperty("eyeDistance")]
        public double EyeDistance { get; set; }

        [JsonProperty("face_id")]
        public string FaceId { get; set; }

        [JsonProperty("gallery_name")]
        public string GalleryName { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
        
        [JsonProperty("image_id")]
        public string ImageId { get; set; }

        [JsonProperty("pitch")]
        public double Pitch { get; set; }

        [JsonProperty("quality")]
        public double Quality { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        
        [JsonProperty("enrollment_timestamp")]
        public string EnrollmentTimestamp { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("subject_id")]
        public string SubjectId { get; set; }

        [JsonProperty("topLeftX")]
        public int TopLeftX { get; set; }

        [JsonProperty("topLeftY")]
        public int TopLeftY { get; set; }
        
        [JsonProperty("version")]
        public double Version { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("yaw")]
        public double Yaw { get; set; }
    }
}
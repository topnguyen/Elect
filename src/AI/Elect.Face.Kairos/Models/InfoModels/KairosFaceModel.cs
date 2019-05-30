using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.InfoModels
{
    public class KairosFaceModel
    {
        [JsonProperty("topLeftX")]
        public double TopLeftX { get; set; }
        
        [JsonProperty("topLeftY")]
        public double TopLeftY { get; set; }
        
        [JsonProperty("chinTipX")]
        public double ChinTipX { get; set; }
        
        [JsonProperty("rightEyeCenterX")]
        public double RightEyeCenterX { get; set; }
        
        [JsonProperty("yaw")]
        public double Yaw { get; set; }
        
        [JsonProperty("chinTipY")]
        public double ChinTipY { get; set; }
        
        [JsonProperty("confidence")]
        public double Confidence { get; set; }
        
        [JsonProperty("height")]
        public double Height { get; set; }
        
        [JsonProperty("rightEyeCenterY")]
        public double RightEyeCenterY { get; set; }
        
        [JsonProperty("width")]
        public double Width { get; set; }
        
        [JsonProperty("leftEyeCenterY")]
        public double LeftEyeCenterY { get; set; }
        
        [JsonProperty("leftEyeCenterX")]
        public double LeftEyeCenterX { get; set; }
        
        [JsonProperty("pitch")]
        public double Pitch { get; set; }
        
        [JsonProperty("attributes")]
        public KairosAttributesModel Attributes { get; set; }
        
        [JsonProperty("face_id")]
        public int FaceId { get; set; }
        
        [JsonProperty("quality")]
        public double Quality { get; set; }
        
        [JsonProperty("roll")]
        public double Roll { get; set; }
    }
}
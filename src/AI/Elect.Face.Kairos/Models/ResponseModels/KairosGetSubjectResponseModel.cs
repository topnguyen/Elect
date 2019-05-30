using System.Collections.Generic;
using Elect.Face.Kairos.Models.InfoModels;
using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.ResponseModels
{
    public class KairosGetSubjectResponseModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("face_id")]
        public List<KairosFaceIdModel> FaceIds { get; set; }
    }
}
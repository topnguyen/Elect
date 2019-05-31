using System.Collections.Generic;
using Elect.Face.Kairos.Models.InfoModels;
using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.ResponseModels
{
    public class KairosEnrollResponseModel : KairosBaseResponseModel
    {
        [JsonProperty("face_id")]
        public string FaceId { get; set; }

        [JsonProperty("images")]
        public List<KairosImageModel> Images { get; set; }
    }
}
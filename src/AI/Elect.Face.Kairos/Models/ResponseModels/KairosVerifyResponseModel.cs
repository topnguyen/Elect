using System.Collections.Generic;
using Elect.Face.Kairos.Models.InfoModels;
using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.ResponseModels
{
    public class KairosVerifyResponseModel
    {
        [JsonProperty("images")]
        public List<KairosImageModel> Images { get; set; }
    }
}
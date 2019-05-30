using System.Collections.Generic;
using Elect.Face.Kairos.Models.InfoModels;
using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.ResponseModels
{
    public class KairosGetAllGalleryResponseModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("gallery_id")]
        public List<string> GalleryIds { get; set; }
    }
}
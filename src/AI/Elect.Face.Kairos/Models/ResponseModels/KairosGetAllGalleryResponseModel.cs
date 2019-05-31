using System.Collections.Generic;
using Elect.Face.Kairos.Models.InfoModels;
using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.ResponseModels
{
    public class KairosGetAllGalleryResponseModel : KairosBaseResponseModel
    {   
        [JsonProperty("gallery_id")]
        public List<string> GalleryIds { get; set; }
    }
}
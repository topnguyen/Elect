using System.Collections.Generic;
using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.ResponseModels
{
    public class KairosGetAllSubjectResponseModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("subject_ids")]
        public List<string> SubjectIds { get; set; }
    }
}
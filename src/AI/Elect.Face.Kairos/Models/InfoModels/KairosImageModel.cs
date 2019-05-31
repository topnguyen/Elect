using System.Collections.Generic;
using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.InfoModels
{
    public class KairosImageModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("width")]
        public double Width { get; set; }
        
        [JsonProperty("height")]
        public double Height { get; set; }
        
        [JsonProperty("file")]
        public string File { get; set; }
        
        [JsonProperty("faces")]
        public List<KairosFaceModel> Faces { get; set; }
        
        [JsonProperty("attributes")]
        public KairosAttributesModel Attributes { get; set; }
        
        [JsonProperty("candidates")]
        public List<KairosCandidateModel> Candidates { get; set; }

        [JsonProperty("transaction")]
        public KairosTransactionModel Transaction { get; set; }
    }
}
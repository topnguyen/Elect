using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.InfoModels
{
    public class KairosAttributesModel
    {
        [JsonProperty("lips")]
        public string Lips { get; set; }

        [JsonProperty("asian")]
        public double Asian { get; set; }

        [JsonProperty("gender")]
        public KairosGenderModel Gender { get; set; }
        
        [JsonProperty("age")]
        public int Age { get; set; }
        
        [JsonProperty("hispanic")]
        public double Hispanic { get; set; }

        [JsonProperty("other")]
        public double Other { get; set; }
        
        [JsonProperty("black")]
        public double Black { get; set; }

        [JsonProperty("white")]
        public double White { get; set; }
        
        [JsonProperty("glasses")]
        public string Glasses { get; set; }

    }
}
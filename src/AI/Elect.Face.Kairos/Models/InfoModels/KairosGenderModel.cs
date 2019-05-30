using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.InfoModels
{
    public class KairosGenderModel
    {
        [JsonProperty("femaleConfidence")]
        public double FemaleConfidence { get; set; }

        [JsonProperty("maleConfidence")]
        public double MaleConfidence { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
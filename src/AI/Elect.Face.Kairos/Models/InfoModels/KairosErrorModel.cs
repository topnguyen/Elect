using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.InfoModels
{
    public class KairosErrorModel
    {
        [JsonProperty("ErrorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
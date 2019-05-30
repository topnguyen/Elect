using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.RequestModels
{
    public class KairosGetSubjectRequestModel
    {
        [JsonProperty("subject_id")]
        public string SubjectId { get; set; }

        /// <summary>
        ///     Gallery name, if not set will use the Default Gallery
        /// </summary>
        [JsonProperty("gallery_name")]
        public string GalleryName { get; set; }
    }
}
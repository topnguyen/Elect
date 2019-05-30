using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.RequestModels
{
    public class KairosRemoveFaceRequestModel
    {
        /// <summary>
        ///     This is optional.
        ///     Set specific Face Id to remove a face in subject only.
        /// </summary>
        /// <remarks>If not set FaceId, then remove Subject</remarks>
        [JsonProperty("face_id")]
        public string FaceId { get; set; }

        [JsonProperty("subject_id")]
        public string SubjectId { get; set; }

        /// <summary>
        ///     Gallery name, if not set will use the Default Gallery
        /// </summary>
        [JsonProperty("gallery_name")]
        public string GalleryName { get; set; }
    }
}
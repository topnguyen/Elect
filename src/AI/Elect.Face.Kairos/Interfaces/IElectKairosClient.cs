using System.Threading.Tasks;
using Elect.Face.Kairos.Models.RequestModels;
using Elect.Face.Kairos.Models.ResponseModels;

namespace Elect.Face.Kairos.Interfaces
{
    public interface IElectKairosClient
    {
        #region Face

        /// <summary>
        ///     The image must contain only 1 face.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosEnrollResponseModel> Enroll(KairosEnrollRequestModel model);
        
        /// <summary>
        ///     The image must contain only 1 face.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosVerifyResponseModel> Verify(KairosVerifyRequestModel model);

        /// <summary>
        ///     An image can have multiple faces
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosRecognizeResponseModel> Recognize(KairosRecognizeRequestModel model);
        
        /// <summary>
        ///     An image can have multiple faces
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosDetectResponseModel> Detect(KairosDetectRequestModel model);

        #endregion

        #region Gallery

        /// <summary>
        ///     All Gallery Ids
        /// </summary>
        /// <returns>All gallery ids</returns>
        Task<KairosGetAllGalleryResponseModel> GetAllGalleryIds();

        /// <summary>
        ///     Gallery's Subject Ids.
        ///     Get all Subject ids inside a gallery
        /// </summary>
        /// <param name="galleryName">If not set, will use Default Gallery</param>
        /// <returns>List all subject ids inside the gallery</returns>
        Task<KairosGetAllSubjectResponseModel> GetAllSubjectIds(string galleryName = null);
        
        
        /// <summary>
        ///     Subject's Face Ids.
        ///     Displays all face_id's and enrollment timestamps for each template you have enrolled
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<KairosGetSubjectResponseModel> GetAllFaceIds(KairosGetSubjectRequestModel model);

        /// <summary>
        ///     Remove gallery
        /// </summary>
        /// <param name="galleryName">If not set, will use Default Gallery</param>
        /// <returns></returns>
        Task RemoveGallery(string galleryName = null);
        
        /// <summary>
        ///     Remove Face or Subject
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>Setup FaceId to remove face, if empty FaceId then remove Subject</remarks>
        Task RemoveFaceOrSubject(KairosRemoveFaceRequestModel model);

        #endregion
    }
}
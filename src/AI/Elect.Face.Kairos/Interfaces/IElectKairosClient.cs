using System;
using System.Threading.Tasks;
using Elect.Face.Kairos.Models.RequestModels;
using Elect.Face.Kairos.Models.ResponseModels;

namespace Elect.Face.Kairos.Interfaces
{
    public interface IElectKairosClient
    {
        #region Face

        // ---------------------- EnrollAsync ----------------------

        /// <summary>
        ///     The image must contain only 1 face.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosEnrollResponseModel> EnrollAsync(Action<KairosEnrollRequestModel> model);
        
        /// <summary>
        ///     The image must contain only 1 face.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosEnrollResponseModel> EnrollAsync(KairosEnrollRequestModel model);
        
        // ---------------------- VerifyAsync ----------------------

        /// <summary>
        ///     The image must contain only 1 face.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosVerifyResponseModel> VerifyAsync(Action<KairosVerifyRequestModel> model);
        
        /// <summary>
        ///     The image must contain only 1 face.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosVerifyResponseModel> VerifyAsync(KairosVerifyRequestModel model);

        // ---------------------- RecognizeAsync ----------------------

        /// <summary>
        ///     An image can have multiple faces
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosRecognizeResponseModel> RecognizeAsync(Action<KairosRecognizeRequestModel> model);
        
        /// <summary>
        ///     An image can have multiple faces
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosRecognizeResponseModel> RecognizeAsync(KairosRecognizeRequestModel model);
        
        // ---------------------- DetectAsync ----------------------

        /// <summary>
        ///     An image can have multiple faces
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosDetectResponseModel> DetectAsync(Action<KairosDetectRequestModel> model);
        
        /// <summary>
        ///     An image can have multiple faces
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>View best practise image via https://www.kairos.com/docs/api/best-practices</remarks>
        Task<KairosDetectResponseModel> DetectAsync(KairosDetectRequestModel model);

        #endregion

        #region Gallery

        // ---------------------- GetAllGalleryIdsAsync ----------------------

        /// <summary>
        ///     All Gallery Ids
        /// </summary>
        /// <returns>All gallery ids</returns>
        Task<KairosGetAllGalleryResponseModel> GetAllGalleryIdsAsync();

        // ---------------------- GetAllSubjectIdsAsync ----------------------

        /// <summary>
        ///     Gallery's Subject Ids.
        ///     Get all Subject ids inside a gallery
        /// </summary>
        /// <param name="galleryName">If not set, will use Default Gallery</param>
        /// <returns>List all subject ids inside the gallery</returns>
        Task<KairosGetAllSubjectResponseModel> GetAllSubjectIdsAsync(string galleryName = null);
        
        // ---------------------- GetAllFaceIdsAsync ----------------------

        /// <summary>
        ///     Subject's Face Ids.
        ///     Displays all face_id's and enrollment timestamps for each template you have enrolled
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<KairosGetSubjectResponseModel> GetAllFaceIdsAsync(Action<KairosGetSubjectRequestModel> model);

        /// <summary>
        ///     Subject's Face Ids.
        ///     Displays all face_id's and enrollment timestamps for each template you have enrolled
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<KairosGetSubjectResponseModel> GetAllFaceIdsAsync(KairosGetSubjectRequestModel model);

        // ---------------------- RemoveGallery ----------------------

        /// <summary>
        ///     Remove gallery
        /// </summary>
        /// <param name="galleryName">If not set, will use Default Gallery</param>
        /// <returns></returns>
        Task RemoveGallery(string galleryName = null);
        
        // ---------------------- RemoveFaceOrSubjectAsync ----------------------
        
        /// <summary>
        ///     Remove Face or Subject
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>Setup FaceId to remove face, if empty FaceId then remove Subject</remarks>
        Task RemoveFaceOrSubjectAsync(Action<KairosRemoveFaceRequestModel> model);

        /// <summary>
        ///     Remove Face or Subject
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>Setup FaceId to remove face, if empty FaceId then remove Subject</remarks>
        Task RemoveFaceOrSubjectAsync(KairosRemoveFaceRequestModel model);

        #endregion
    }
}
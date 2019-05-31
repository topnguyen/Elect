using System;
using System.Threading.Tasks;
using Elect.Core.ActionUtils;
using Elect.Face.Kairos.Interfaces;
using Elect.Face.Kairos.Models;
using Elect.Face.Kairos.Models.RequestModels;
using Elect.Face.Kairos.Models.ResponseModels;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace Elect.Face.Kairos.Services
{
    public class ElectKairosClient : IElectKairosClient
    {
        private readonly ElectKairosOptions _config;

        public ElectKairosClient(IOptions<ElectKairosOptions> config)
        {
            _config = config.Value;
        }

        public IFlurlRequest CreateHttpRequest(string urlPath)
        {
            var endpoint = $"https://api.kairos.com/{urlPath}";

            var request = endpoint
                .WithHeader("Content-Type", "application/json")
                .WithHeader("app_id", _config.AppId)
                .WithHeader("app_key", _config.AppKey);

            return request;
        }

        #region Face

        // ---------------------- EnrollAsync ----------------------

        public Task<KairosEnrollResponseModel> EnrollAsync(Action<KairosEnrollRequestModel> model)
        {
            return EnrollAsync(model.GetValue());
        }

        public async Task<KairosEnrollResponseModel> EnrollAsync(KairosEnrollRequestModel model)
        {
            model.GalleryName = !string.IsNullOrWhiteSpace(model.GalleryName)
                ? model.GalleryName
                : _config.DefaultGallery;

            var result =
                await CreateHttpRequest("enroll")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosEnrollResponseModel>();

            return result;
        }

        // ---------------------- VerifyAsync ----------------------

        public Task<KairosVerifyResponseModel> VerifyAsync(Action<KairosVerifyRequestModel> model)
        {
            return VerifyAsync(model.GetValue());
        }

        public async Task<KairosVerifyResponseModel> VerifyAsync(KairosVerifyRequestModel model)
        {
            model.GalleryName = !string.IsNullOrWhiteSpace(model.GalleryName)
                ? model.GalleryName
                : _config.DefaultGallery;
            
            var result =
                await CreateHttpRequest("verify")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosVerifyResponseModel>();

            return result;
        }

        // ---------------------- RecognizeAsync ----------------------

        public Task<KairosRecognizeResponseModel> RecognizeAsync(Action<KairosRecognizeRequestModel> model)
        {
            return RecognizeAsync(model.GetValue());
        }

        public async Task<KairosRecognizeResponseModel> RecognizeAsync(KairosRecognizeRequestModel model)
        {
            model.GalleryName = !string.IsNullOrWhiteSpace(model.GalleryName)
                ? model.GalleryName
                : _config.DefaultGallery;

            try
            {
                var result =
                    await CreateHttpRequest("recognize")
                        .PostJsonAsync(model)
                        .ReceiveJson<KairosRecognizeResponseModel>();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // ---------------------- DetectAsync ----------------------

        public Task<KairosDetectResponseModel> DetectAsync(Action<KairosDetectRequestModel> model)
        {
            return DetectAsync(model.GetValue());
        }

        public async Task<KairosDetectResponseModel> DetectAsync(KairosDetectRequestModel model)
        {
            var result =
                await CreateHttpRequest("detect")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosDetectResponseModel>();

            return result;
        }

        #endregion

        #region Gallery

        // ---------------------- GetAllGalleryIdsAsync ----------------------

        public async Task<KairosGetAllGalleryResponseModel> GetAllGalleryIdsAsync()
        {
            var result =
                await CreateHttpRequest("gallery/list_all")
                    .PostJsonAsync(new { })
                    .ReceiveJson<KairosGetAllGalleryResponseModel>();

            return result;
        }

        // ---------------------- GetAllSubjectIdsAsync ----------------------

        public async Task<KairosGetAllSubjectResponseModel> GetAllSubjectIdsAsync(string galleryName = null)
        {
            var result =
                await CreateHttpRequest("gallery/view")
                    .PostJsonAsync(new
                    {
                        gallery_name = galleryName
                    })
                    .ReceiveJson<KairosGetAllSubjectResponseModel>();

            return result;
        }

        // ---------------------- GetAllFaceIdsAsync ----------------------

        public Task<KairosGetSubjectResponseModel> GetAllFaceIdsAsync(Action<KairosGetSubjectRequestModel> model)
        {
            return GetAllFaceIdsAsync(model.GetValue());
        }

        public async Task<KairosGetSubjectResponseModel> GetAllFaceIdsAsync(KairosGetSubjectRequestModel model)
        {
            model.GalleryName = !string.IsNullOrWhiteSpace(model.GalleryName)
                ? model.GalleryName
                : _config.DefaultGallery;

            var result =
                await CreateHttpRequest("gallery/view_subject")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosGetSubjectResponseModel>();

            return result;
        }

        // ---------------------- RemoveGallery ----------------------

        public async Task<KairosRemoveGalleryResponse> RemoveGallery(string galleryName = null)
        {
            var result =
                await CreateHttpRequest("gallery/view_subject")
                    .PostJsonAsync(new
                    {
                        gallery_name = galleryName
                    })
                    .ReceiveJson<KairosRemoveGalleryResponse>();

            return result;
        }

        // ---------------------- RemoveFaceOrSubjectAsync ----------------------

        public Task<KairosRemoveFaceOrSubjectResponseModel> RemoveFaceOrSubjectAsync(
            Action<KairosRemoveFaceOrSubjectRequestModel> model)
        {
            return RemoveFaceOrSubjectAsync(model.GetValue());
        }

        public async Task<KairosRemoveFaceOrSubjectResponseModel> RemoveFaceOrSubjectAsync(
            KairosRemoveFaceOrSubjectRequestModel model)
        {
            model.GalleryName = !string.IsNullOrWhiteSpace(model.GalleryName)
                ? model.GalleryName
                : _config.DefaultGallery;

            var result = await CreateHttpRequest("gallery/remove_subject")
                .PostJsonAsync(model)
                .ReceiveJson<KairosRemoveFaceOrSubjectResponseModel>();

            return result;
        }

        #endregion
    }
}
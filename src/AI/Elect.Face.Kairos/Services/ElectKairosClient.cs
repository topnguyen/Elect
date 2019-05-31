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

            var enrollResponse =
                await CreateHttpRequest("enroll")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosEnrollResponseModel>();

            return enrollResponse;
        }

        // ---------------------- VerifyAsync ----------------------

        public Task<KairosVerifyResponseModel> VerifyAsync(Action<KairosVerifyRequestModel> model)
        {
            return VerifyAsync(model.GetValue());
        }

        public async Task<KairosVerifyResponseModel> VerifyAsync(KairosVerifyRequestModel model)
        {
            var verifyResponse =
                await CreateHttpRequest("verify")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosVerifyResponseModel>();

            return verifyResponse;
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


            var recognizeResponse =
                await CreateHttpRequest("recognize")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosRecognizeResponseModel>();

            return recognizeResponse;
        }

        // ---------------------- DetectAsync ----------------------

        public Task<KairosDetectResponseModel> DetectAsync(Action<KairosDetectRequestModel> model)
        {
            return DetectAsync(model.GetValue());
        }

        public async Task<KairosDetectResponseModel> DetectAsync(KairosDetectRequestModel model)
        {
            var detectResponse =
                await CreateHttpRequest("detect")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosDetectResponseModel>();

            return detectResponse;
        }
        
        #endregion

        #region Gallery
        
        // ---------------------- GetAllGalleryIdsAsync ----------------------

        public async Task<KairosGetAllGalleryResponseModel> GetAllGalleryIdsAsync()
        {
            var allGalleryResponse =
                await CreateHttpRequest("gallery/list_all")
                    .PostJsonAsync(new { })
                    .ReceiveJson<KairosGetAllGalleryResponseModel>();

            return allGalleryResponse;
        }
        
        // ---------------------- GetAllSubjectIdsAsync ----------------------

        public async Task<KairosGetAllSubjectResponseModel> GetAllSubjectIdsAsync(string galleryName = null)
        {
            var allSubjectResponse =
                await CreateHttpRequest("gallery/view")
                    .PostJsonAsync(new
                    {
                        gallery_name = galleryName
                    })
                    .ReceiveJson<KairosGetAllSubjectResponseModel>();

            return allSubjectResponse;
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

            var allFaceIdResponse =
                await CreateHttpRequest("gallery/view_subject")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosGetSubjectResponseModel>();

            return allFaceIdResponse;
        }

        // ---------------------- RemoveGallery ----------------------

        public async Task RemoveGallery(string galleryName = null)
        {
            await CreateHttpRequest("gallery/view_subject")
                .PostJsonAsync(new
                {
                    gallery_name = galleryName
                });
        }

        // ---------------------- RemoveFaceOrSubjectAsync ----------------------

        public Task RemoveFaceOrSubjectAsync(Action<KairosRemoveFaceRequestModel> model)
        {
            return RemoveFaceOrSubjectAsync(model.GetValue());
        }

        public async Task RemoveFaceOrSubjectAsync(KairosRemoveFaceRequestModel model)
        {
            model.GalleryName = !string.IsNullOrWhiteSpace(model.GalleryName)
                ? model.GalleryName
                : _config.DefaultGallery;

            await CreateHttpRequest("remove_subject")
                .PostJsonAsync(model);
        }
        
        #endregion
    }
}
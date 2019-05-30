using System.Threading.Tasks;
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


        public async Task<KairosEnrollResponseModel> Enroll(KairosEnrollRequestModel model)
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

        public async Task<KairosVerifyResponseModel> Verify(KairosVerifyRequestModel model)
        {
            var verifyResponse =
                await CreateHttpRequest("verify")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosVerifyResponseModel>();

            return verifyResponse;
        }

        public async Task<KairosRecognizeResponseModel> Recognize(KairosRecognizeRequestModel model)
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

        public async Task<KairosDetectResponseModel> Detect(KairosDetectRequestModel model)
        {
            var detectResponse =
                await CreateHttpRequest("detect")
                    .PostJsonAsync(model)
                    .ReceiveJson<KairosDetectResponseModel>();

            return detectResponse;
        }

        public async Task<KairosGetAllGalleryResponseModel> GetAllGalleryIds()
        {
            var allGalleryResponse =
                await CreateHttpRequest("gallery/list_all")
                    .PostJsonAsync(new { })
                    .ReceiveJson<KairosGetAllGalleryResponseModel>();

            return allGalleryResponse;
        }

        public async Task<KairosGetAllSubjectResponseModel> GetAllSubjectIds(string galleryName = null)
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

        public async Task<KairosGetSubjectResponseModel> GetAllFaceIds(KairosGetSubjectRequestModel model)
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

        public async Task RemoveGallery(string galleryName = null)
        {
            await CreateHttpRequest("gallery/view_subject")
                .PostJsonAsync(new
                {
                    gallery_name = galleryName
                });
        }

        public async Task RemoveFaceOrSubject(KairosRemoveFaceRequestModel model)
        {
            model.GalleryName = !string.IsNullOrWhiteSpace(model.GalleryName)
                ? model.GalleryName
                : _config.DefaultGallery;

            await CreateHttpRequest("remove_subject")
                .PostJsonAsync(model);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using Elect.Face.Kairos;
using Elect.Face.Kairos.Interfaces;
using Elect.Face.Kairos.Models.RequestModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.AI.Face.Kairos
{
    [TestClass]
    public class KairosFaceUnitTest
    {
        public IElectKairosClient GetElectKairosClient()
        {
            var services = new ServiceCollection();

            services.AddElectKairos(_ =>
            {
                _.AppId = "";
                _.AppKey = "";
                _.DefaultGallery = "Top Gallery";
            });

            var provider = services.BuildServiceProvider();

            var electKairosClient = provider.GetService<IElectKairosClient>();

            return electKairosClient;
        }

        [TestMethod]
        public async Task Faces()
        {
            var electKairosClient = GetElectKairosClient();

            // Hue
            
            var enrollHue1Result = await electKairosClient.EnrollAsync(_ =>
            {
                _.Image = "https://imagecy.com/download/550703cafdea4ec7a2f51ed274c0b286?c=true";
                _.SubjectId = "Hue";
            });
            
            var enrollHue2Result = await electKairosClient.EnrollAsync(_ =>
            {
                _.Image = "https://imagecy.com/download/6398988799274fc1975cf623c7a47630?c=true";
                _.SubjectId = "Hue";
            });

            // Get back Hue Face
            
            var allHueFaces =  await electKairosClient.GetAllFaceIdsAsync(_ =>
            {
                _.SubjectId = "Hue";
            });
            
            // Remove a face of Hue
             await electKairosClient.RemoveFaceOrSubjectAsync(_ =>
                {
                    _.FaceId = allHueFaces.FaceIds.FirstOrDefault().FaceId;
                    _.SubjectId = "Hue";
                });
            
            // Verify Hue
            
            var verifyHueResult = await electKairosClient.VerifyAsync(_ =>
            {
                _.Image = "https://imagecy.com/download/60259c7e3403431ea542fbfafafd0db4?c=true";
                _.SubjectId = "Hue";
            });
            
            // Dong
            
            var enrollDong1Result = await electKairosClient.EnrollAsync(_ =>
            {
                _.Image = "https://imagecy.com/download/dfbf2258628c49588244d194bf4f83f9?c=true";
                _.SubjectId = "Dong";
            });
            
            var enrollDong2Result = await electKairosClient.EnrollAsync(_ =>
            {
                _.Image = "https://imagecy.com/download/fd4c2b8d29d1488b9187caef82b93a88?c=true";
                _.SubjectId = "Dong";
            });
            
            // Recognize test, result must Dong
              
            var recognizeResult = await electKairosClient.VerifyAsync(_ =>
            {
                _.Image = "https://imagecy.com/download/18e5adc10c324b1baa99b6491f8201b3?c=true";
            });
            
            // Get Gallery Have
            var galleries = await electKairosClient.GetAllGalleryIdsAsync();
            
            // Remove a gallery
            await electKairosClient.RemoveGallery(galleries.GalleryIds.FirstOrDefault());
        }
    }
}
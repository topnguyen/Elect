using System.Threading.Tasks;
using Elect.Face.Kairos;
using Elect.Face.Kairos.Interfaces;
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
                _.DefaultGallery = "Elect Kairos";
            });

            var provider = services.BuildServiceProvider();

            var electKairosClient = provider.GetService<IElectKairosClient>();

            return electKairosClient;
        }

        [TestMethod]
        public async Task Faces()
        {
            var electKairosClient = GetElectKairosClient();

            await electKairosClient.RemoveGallery("SAS");
        }
    }
}
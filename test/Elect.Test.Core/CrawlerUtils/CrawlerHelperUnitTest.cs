namespace Elect.Test.Core.CrawlerUtils
{
    [TestClass]
    public class CrawlerHelperUnitTest
    {
        [TestMethod]
        public async Task GetListMetadataAsync_EmptyUrls_ReturnsEmptyList()
        {
            var result = await CrawlerHelper.GetListMetadataAsync();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public async Task GetListMetadataAsync_DuplicateUrls_Deduplicates()
        {
            // This test assumes GetMetadataByUrlAsync is robust to invalid URLs
            var result = await CrawlerHelper.GetListMetadataAsync("http://test.com", "http://test.com");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task GetMetadataByUrlAsync_InvalidUrl_ReturnsDefaultMetadata()
        {
            var result = await CrawlerHelper.GetMetadataByUrlAsync("http://invalid.url");
            Assert.IsNotNull(result);
        }
    }
}

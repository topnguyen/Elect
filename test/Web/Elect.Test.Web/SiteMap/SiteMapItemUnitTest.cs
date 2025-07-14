using Elect.Web.SiteMap.Models;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapItemUnitTest
    {
        [TestMethod]
        public void Constructor_WithUrl_CreatesValidItem()
        {
            var url = "https://example.com/page";
            var item = new SiteMapItem(url);

            Assert.AreEqual(url, item.Url);
            Assert.IsNull(item.LastModified);
            Assert.IsNull(item.Frequency);
            Assert.IsNull(item.Priority);
        }

        [TestMethod]
        public void Constructor_WithAllParameters_CreatesValidItem()
        {
            var url = "https://example.com/page";
            var lastModified = DateTime.Now;
            var frequency = SiteMapItemFrequency.Daily;
            var priority = 0.8;

            var item = new SiteMapItem(url, lastModified, frequency, priority);

            Assert.AreEqual(url, item.Url);
            Assert.AreEqual(lastModified, item.LastModified);
            Assert.AreEqual(frequency, item.Frequency);
            Assert.AreEqual(priority, item.Priority);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullUrl_ThrowsArgumentNullException()
        {
            new SiteMapItem(null);
        }

        [TestMethod]
        public void Priority_SetValidValue_SetsCorrectly()
        {
            var item = new SiteMapItem("https://example.com");
            
            item.Priority = 0.5;
            Assert.AreEqual(0.5, item.Priority);

            item.Priority = 0.0;
            Assert.AreEqual(0.0, item.Priority);

            item.Priority = 1.0;
            Assert.AreEqual(1.0, item.Priority);

            item.Priority = null;
            Assert.IsNull(item.Priority);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Priority_SetValueLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var item = new SiteMapItem("https://example.com");
            item.Priority = -0.1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Priority_SetValueGreaterThanOne_ThrowsArgumentOutOfRangeException()
        {
            var item = new SiteMapItem("https://example.com");
            item.Priority = 1.1;
        }

        [TestMethod]
        public void Frequency_SetAndGet_WorksCorrectly()
        {
            var item = new SiteMapItem("https://example.com");
            
            item.Frequency = SiteMapItemFrequency.Weekly;
            Assert.AreEqual(SiteMapItemFrequency.Weekly, item.Frequency);

            item.Frequency = null;
            Assert.IsNull(item.Frequency);
        }

        [TestMethod]
        public void LastModified_SetAndGet_WorksCorrectly()
        {
            var item = new SiteMapItem("https://example.com");
            var date = new DateTime(2023, 1, 1);
            
            item.LastModified = date;
            Assert.AreEqual(date, item.LastModified);

            item.LastModified = null;
            Assert.IsNull(item.LastModified);
        }

        [TestMethod]
        public void Dispose_DoesNotThrow()
        {
            var item = new SiteMapItem("https://example.com");
            item.Dispose(); // Should not throw
        }
    }
}
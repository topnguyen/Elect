using Elect.Web.SiteMap.Services;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapValidatorUnitTest
    {
        [TestMethod]
        public void CheckDocumentSize_WithValidSize_DoesNotThrow()
        {
            var validXml = new string('a', 1000); // 1KB
            
            // Should not throw
            SiteMapValidator.CheckDocumentSize(validXml);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CheckDocumentSize_WithOversizedDocument_ThrowsNotSupportedException()
        {
            var oversizedXml = new string('a', SiteMapValidator.MaximumSiteMapSizeInBytes);
            
            SiteMapValidator.CheckDocumentSize(oversizedXml);
        }

        [TestMethod]
        public void CheckDocumentSize_WithMaximumAllowedSize_DoesNotThrow()
        {
            var maxSizeXml = new string('a', SiteMapValidator.MaximumSiteMapSizeInBytes - 1);
            
            // Should not throw
            SiteMapValidator.CheckDocumentSize(maxSizeXml);
        }

        [TestMethod]
        public void CheckSiteMapCount_WithValidCount_DoesNotThrow()
        {
            var validCount = 1000;
            
            // Should not throw
            SiteMapValidator.CheckSiteMapCount(validCount);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CheckSiteMapCount_WithExcessiveCount_ThrowsNotSupportedException()
        {
            var excessiveCount = SiteMapValidator.MaximumSiteMapCount + 1;
            
            SiteMapValidator.CheckSiteMapCount(excessiveCount);
        }

        [TestMethod]
        public void CheckSiteMapCount_WithMaximumAllowedCount_DoesNotThrow()
        {
            var maxCount = SiteMapValidator.MaximumSiteMapCount;
            
            // Should not throw
            SiteMapValidator.CheckSiteMapCount(maxCount);
        }

        [TestMethod]
        public void CheckSiteMapCount_WithZeroCount_DoesNotThrow()
        {
            // Should not throw
            SiteMapValidator.CheckSiteMapCount(0);
        }

        [TestMethod]
        public void Constants_HaveExpectedValues()
        {
            Assert.AreEqual(50000, SiteMapValidator.MaximumSiteMapCount);
            Assert.AreEqual(25000, SiteMapValidator.MaximumSiteMapIndexCount);
            Assert.AreEqual(10485760, SiteMapValidator.MaximumSiteMapSizeInBytes); // 10MB
        }

        [TestMethod]
        public void CheckDocumentSize_WithEmptyString_DoesNotThrow()
        {
            // Should not throw
            SiteMapValidator.CheckDocumentSize("");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckDocumentSize_WithNullString_ThrowsNullReferenceException()
        {
            // CheckDocumentSize doesn't handle null input
            SiteMapValidator.CheckDocumentSize(null);
        }

        [TestMethod]
        public void CheckSiteMapCount_WithNegativeCount_DoesNotThrow()
        {
            // Should not throw (negative numbers are less than maximum)
            SiteMapValidator.CheckSiteMapCount(-1);
        }
    }
}
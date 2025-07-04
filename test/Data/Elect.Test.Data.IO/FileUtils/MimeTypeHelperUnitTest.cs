namespace Elect.Test.Data.IO.FileUtils
{
    [TestClass]
    public class MimeTypeHelperUnitTests
    {
        [DataTestMethod]
        [DataRow(".jpg", "image/jpeg")]
        [DataRow("jpg", "image/jpeg")]
        [DataRow(".unknown", "application/octet-stream")]
        [DataRow("unknown", "application/octet-stream")]
        public void GetMimeType_ReturnsExpected(string ext, string expected)
        {
            Assert.AreEqual(expected, MimeTypeHelper.GetMimeType(ext));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMimeType_ThrowsOnNull()
        {
            MimeTypeHelper.GetMimeType(null);
        }

        [DataTestMethod]
        [DataRow("image/jpeg", ".jpg")]
        [DataRow("application/xml", ".xml")]
        public void GetExtension_ReturnsExpected(string mime, string expected)
        {
            Assert.AreEqual(expected, MimeTypeHelper.GetExtension(mime));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetExtension_ThrowsOnNull()
        {
            MimeTypeHelper.GetExtension(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetExtension_ThrowsOnDotInput()
        {
            MimeTypeHelper.GetExtension(".jpg");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetExtension_ThrowsOnNotFound()
        {
            MimeTypeHelper.GetExtension("not/registered");
        }

        [TestMethod]
        public void GetExtension_ReturnsEmptyOnNotFoundAndNoThrow()
        {
            Assert.AreEqual(string.Empty, MimeTypeHelper.GetExtension("not/registered", false));
        }
    }
}
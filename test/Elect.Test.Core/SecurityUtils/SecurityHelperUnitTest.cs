namespace Elect.Test.Core.SecurityUtils
{
    [TestClass]
    public class SecurityHelperUnitTest
    {
        [TestMethod]
        public void GenerateSalt_ReturnsBase64String()
        {
            var salt = SecurityHelper.GenerateSalt(16);
            var bytes = Convert.FromBase64String(salt);
            Assert.AreEqual(16, bytes.Length);
        }
        [TestMethod]
        public void EncryptSha256_ReturnsHash()
        {
            var hash = SecurityHelper.EncryptSha256("test");
            Assert.AreEqual(64, hash.Length);
        }
        [TestMethod]
        public void EncryptSha512_ReturnsHash()
        {
            var hash = SecurityHelper.EncryptSha512("test");
            Assert.AreEqual(128, hash.Length);
        }
        [TestMethod]
        public void EncryptHmacSha256_ReturnsHash()
        {
            var key = SecurityHelper.GenerateSalt(32);
            var hash = SecurityHelper.EncryptHmacSha256("test", key);
            Assert.AreEqual(64, hash.Length);
        }
        [TestMethod]
        public void EncryptHmacSha512_ReturnsHash()
        {
            var key = SecurityHelper.GenerateSalt(64);
            var hash = SecurityHelper.EncryptHmacSha512("test", key);
            Assert.AreEqual(128, hash.Length);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateSalt_ThrowsOnZeroLength()
        {
            SecurityHelper.GenerateSalt(0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptSha256_ThrowsOnNull()
        {
            SecurityHelper.EncryptSha256(null);
        }
        [TestMethod]
        public void EncryptSha256_EmptyString_ReturnsHash()
        {
            var hash = SecurityHelper.EncryptSha256("");
            Assert.AreEqual(64, hash.Length);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptSha512_ThrowsOnNull()
        {
            SecurityHelper.EncryptSha512(null);
        }
        [TestMethod]
        public void EncryptSha512_EmptyString_ReturnsHash()
        {
            var hash = SecurityHelper.EncryptSha512("");
            Assert.AreEqual(128, hash.Length);
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void EncryptHmacSha256_ThrowsOnInvalidKey()
        {
            SecurityHelper.EncryptHmacSha256("test", "notbase64");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptHmacSha256_ThrowsOnNullValue()
        {
            var key = SecurityHelper.GenerateSalt(32);
            SecurityHelper.EncryptHmacSha256(null, key);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptHmacSha256_ThrowsOnNullKey()
        {
            SecurityHelper.EncryptHmacSha256("test", null);
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void EncryptHmacSha512_ThrowsOnInvalidKey()
        {
            SecurityHelper.EncryptHmacSha512("test", "notbase64");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptHmacSha512_ThrowsOnNullValue()
        {
            var key = SecurityHelper.GenerateSalt(64);
            SecurityHelper.EncryptHmacSha512(null, key);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptHmacSha512_ThrowsOnNullKey()
        {
            SecurityHelper.EncryptHmacSha512("test", null);
        }
    }
}

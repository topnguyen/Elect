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
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateSalt_ThrowsOnNegativeLength()
        {
            SecurityHelper.GenerateSalt(-1);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptSha256_ThrowsOnNull()
        {
            SecurityHelper.EncryptSha256(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptSha512_ThrowsOnNull()
        {
            SecurityHelper.EncryptSha512(null);
        }

        [TestMethod]
        public void EncryptSha256_EmptyString_ReturnsHash()
        {
            var hash = SecurityHelper.EncryptSha256("");
            Assert.AreEqual(64, hash.Length);
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
            SecurityHelper.EncryptHmacSha256(null, SecurityHelper.GenerateSalt(32));
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
            SecurityHelper.EncryptHmacSha512(null, SecurityHelper.GenerateSalt(64));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EncryptHmacSha512_ThrowsOnNullKey()
        {
            SecurityHelper.EncryptHmacSha512("test", null);
        }

        [TestMethod]
        public void HashPassword_WithSalt_ProducesConsistentHash()
        {
            var hash1 = SecurityHelper.HashPassword("password123", "somesalt");
            var hash2 = SecurityHelper.HashPassword("password123", "somesalt");
            Assert.AreEqual(hash1, hash2);
            Assert.AreEqual(44, hash1.Length); // 32 bytes base64
        }

        [TestMethod]
        public void HashPassword_WithOutSalt_ProducesHashAndSalt()
        {
            string salt;
            var hash = SecurityHelper.HashPassword("password123", out salt);
            Assert.IsFalse(string.IsNullOrEmpty(hash));
            Assert.IsFalse(string.IsNullOrEmpty(salt));
        }

        [TestMethod]
        public void Encrypt_And_Decrypt_RoundTrip()
        {
            var key = "encryptionkey";
            var original = "SensitiveData123!";
            var encrypted = SecurityHelper.Encrypt(original, key);
            Assert.AreNotEqual(original, encrypted);
            var decrypted = SecurityHelper.Decrypt(encrypted, key);
            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void TryDecrypt_SuccessAndFailure()
        {
            var key = "encryptionkey";
            var original = "SensitiveData123!";
            var encrypted = SecurityHelper.Encrypt(original, key);
            string decrypted;
            var success = SecurityHelper.TryDecrypt(encrypted, key, out decrypted);
            Assert.IsTrue(success);
            Assert.AreEqual(original, decrypted);
            // Failure case: wrong key
            success = SecurityHelper.TryDecrypt(encrypted, "wrongkey", out decrypted);
            Assert.IsFalse(success);
            Assert.IsNull(decrypted);
        }

        [TestMethod]
        public void HashCrc32_ProducesExpectedFormat()
        {
            var hash = SecurityHelper.HashCrc32("test");
            Assert.AreEqual(8, hash.Length); // CRC32 is 4 bytes, 8 hex chars
            Assert.IsTrue(System.Text.RegularExpressions.Regex.IsMatch(hash, "^[A-F0-9]{8}$"));
        }
    }
}

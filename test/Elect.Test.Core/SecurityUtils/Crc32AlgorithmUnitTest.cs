namespace Elect.Test.Core.SecurityUtils
{
    [TestClass]
    public class Crc32AlgorithmUnitTest
    {
        [TestMethod]
        public void Crc32_DefaultConstructor_HashMatchesKnownValue()
        {
            var crc32 = new Crc32();
            var data = Encoding.UTF8.GetBytes("123456789");
            var hash = crc32.ComputeHash(data);
            // Known CRC32 for "123456789" is 0xCBF43926
            var expected = new byte[] { 0xCB, 0xF4, 0x39, 0x26 };
            var actual = hash.Skip(hash.Length - 4).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Crc32_CustomPolynomialAndSeed_ProducesDifferentHash()
        {
            var crc32 = new Crc32(0x04C11DB7, 0x12345678);
            var data = Encoding.UTF8.GetBytes("test");
            var hash1 = crc32.ComputeHash(data);
            var crc32Default = new Crc32();
            var hash2 = crc32Default.ComputeHash(data);
            CollectionAssert.AreNotEqual(hash1, hash2);
        }
        [TestMethod]
        public void Crc32_EmptyArray_ReturnsExpectedHash()
        {
            var crc32 = new Crc32();
            var hash = crc32.ComputeHash(new byte[0]);
            Assert.AreEqual(4, hash.Length);
        }
        [TestMethod]
        public void Crc32_RepeatedInitialize_ResetsHash()
        {
            var crc32 = new Crc32();
            var data = Encoding.UTF8.GetBytes("abc");
            var hash1 = crc32.ComputeHash(data);
            crc32.Initialize();
            var hash2 = crc32.ComputeHash(data);
            CollectionAssert.AreEqual(hash1, hash2);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Crc32_ComputeHash_ThrowsOnNullArray()
        {
            var crc32 = new Crc32();
            byte[] data = null;
            // Explicitly cast to byte[] to resolve ambiguity
            crc32.ComputeHash((byte[])data);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Crc32_ComputeHash_ThrowsOnNegativeStart()
        {
            var crc32 = new Crc32();
            var data = new byte[10];
            crc32.ComputeHash(data, -1, 5);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Crc32_ComputeHash_ThrowsOnNegativeSize()
        {
            var crc32 = new Crc32();
            var data = new byte[10];
            crc32.ComputeHash(data, 0, -1);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Crc32_ComputeHash_ThrowsOnOutOfBounds()
        {
            var crc32 = new Crc32();
            var data = new byte[10];
            crc32.ComputeHash(data, 5, 10);
        }
    }
}

namespace Elect.Test.Core.StreamUtils
{
    [TestClass]
    public class StreamHelperUnitTest
    {
        [TestMethod]
        public void ConvertToBytes_ReturnsCorrectBytes()
        {
            var expected = new byte[] { 10, 20, 30, 40 };
            using (var ms = new MemoryStream(expected))
            {
                var result = StreamHelper.ConvertToBytes(ms);
                CollectionAssert.AreEqual(expected, result);
            }
        }
        [TestMethod]
        public void ConvertToBytes_EmptyStream_ReturnsEmptyArray()
        {
            using (var ms = new MemoryStream())
            {
                var result = StreamHelper.ConvertToBytes(ms);
                Assert.AreEqual(0, result.Length);
            }
        }
        [TestMethod]
        public void ConvertToBytes_NonSeekableStream_WorksCorrectly()
        {
            var data = new byte[] { 1, 2, 3 };
            using (var stream = new NonSeekableMemoryStream(data))
            {
                var result = StreamHelper.ConvertToBytes(stream);
                CollectionAssert.AreEqual(data, result);
            }
        }

        [TestMethod]
        public void ConvertToBytes_BufferResize_WorksCorrectly()
        {
            var data = new byte[5000];
            for (int i = 0; i < data.Length; i++) data[i] = (byte)(i % 256);
            using (var ms = new MemoryStream(data))
            {
                var result = StreamHelper.ConvertToBytes(ms);
                CollectionAssert.AreEqual(data, result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertToBytes_StreamThrowsOnRead_ThrowsException()
        {
            using (var stream = new ThrowingStream())
            {
                StreamHelper.ConvertToBytes(stream);
            }
        }

        [TestMethod]
        public void ConvertToBytes_StreamWithNonZeroPosition_ResetsPosition()
        {
            var data = new byte[] { 5, 6, 7, 8 };
            using (var ms = new MemoryStream(data))
            {
                ms.Position = 2;
                var result = StreamHelper.ConvertToBytes(ms);
                CollectionAssert.AreEqual(data, result);
                Assert.AreEqual(2, ms.Position); // Should restore original position
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ConvertToBytes_StreamLengthOverflow_ThrowsException()
        {
            using (var stream = new LargeLengthStream())
            {
                StreamHelper.ConvertToBytes(stream);
            }
        }

        [TestMethod]
        public void ConvertToBytes_TriggersBufferResizeAndReadsExtraByte()
        {
            // Arrange: stream length = N, but actually N+1 bytes available
            int initialLength = 100;
            var data = new byte[initialLength + 1];
            for (int i = 0; i < data.Length; i++) data[i] = (byte)(i % 256);
            // Create a stream that reports Length = initialLength, but allows reading one more byte
            using (var stream = new FakeLengthStream(data, initialLength))
            {
                // Act
                var result = StreamHelper.ConvertToBytes(stream);
                // Assert
                CollectionAssert.AreEqual(data, result);
            }
        }

        // Helper classes for testing
        private class NonSeekableMemoryStream : MemoryStream
        {
            public NonSeekableMemoryStream(byte[] buffer) : base(buffer) { }
            public override bool CanSeek => false;
        }

        private class ThrowingStream : MemoryStream
        {
            public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        }

        private class LargeLengthStream : MemoryStream
        {
            public override long Length => (long)int.MaxValue + 10;
        }

        private class FakeLengthStream : MemoryStream
        {
            private readonly long _fakeLength;
            public FakeLengthStream(byte[] buffer, long fakeLength) : base(buffer) => _fakeLength = fakeLength;
            public override long Length => _fakeLength;
        }
    }
}

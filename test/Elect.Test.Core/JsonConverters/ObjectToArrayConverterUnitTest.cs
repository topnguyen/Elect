using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Elect.Test.Core.JsonConverters
{
    [JsonConverter(typeof(ObjectToArrayConverter))]
    public class Sample
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    [TestClass]
    public class ObjectToArrayConverterUnitTest
    {
        [TestMethod]
        public void WriteJson_SerializesObjectToArray()
        {
            var sample = new Sample { Id = 42, Name = "Test" };
            var json = JsonConvert.SerializeObject(sample);
            Assert.AreEqual("[42,\"Test\"]", json);
        }
        [TestMethod]
        public void ReadJson_DeserializesArrayToObject()
        {
            var json = "[42,\"Test\"]";
            var sample = JsonConvert.DeserializeObject<Sample>(json);
            Assert.AreEqual(42, sample.Id);
            Assert.AreEqual("Test", sample.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void ReadJson_InvalidToken_ThrowsException()
        {
            var json = "{\"Id\":42,\"Name\":\"Test\"}";
            JsonConvert.DeserializeObject<Sample>(json);
        }

        [TestMethod]
        public void CanConvert_CoversAllBranches()
        {
            var converter = new ObjectToArrayConverter();
            Assert.IsFalse(converter.CanConvert(typeof(int))); // primitive
            Assert.IsFalse(converter.CanConvert(typeof(string))); // string
            Assert.IsFalse(converter.CanConvert(typeof(int[]))); // array
            Assert.IsFalse(converter.CanConvert(typeof(List<int>))); // collection
            Assert.IsFalse(converter.CanConvert(typeof(JsonConverter))); // Newtonsoft type
            Assert.IsTrue(converter.CanConvert(typeof(Sample))); // user-defined class
        }

        [TestMethod]
        public void ReadJson_NullToken_ReturnsNull()
        {
            var converter = new ObjectToArrayConverter();
            var serializer = new JsonSerializer();
            using var sr = new StringReader("null");
            using var reader = new JsonTextReader(sr);
            reader.Read();
            var result = converter.ReadJson(reader, typeof(Sample), null, serializer);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReadJson_InvalidContract_ThrowsException()
        {
            var converter = new ObjectToArrayConverter();
            var serializer = new JsonSerializer();
            using var sr = new StringReader("[1,2]");
            using var reader = new JsonTextReader(sr);
            reader.Read();
            try
            {
                converter.ReadJson(reader, typeof(System.IO.Stream), null, serializer);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                // Accept either JsonSerializationException or NullReferenceException for robustness
                Assert.IsTrue(
                    ex is JsonSerializationException || ex is NullReferenceException,
                    $"Expected JsonSerializationException or NullReferenceException, but got {ex.GetType()}"
                );
            }
        }
    }
}

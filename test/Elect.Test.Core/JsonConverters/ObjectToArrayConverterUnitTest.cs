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
    }
}

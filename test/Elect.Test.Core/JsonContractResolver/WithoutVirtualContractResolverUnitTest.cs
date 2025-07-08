namespace Elect.Test.Core.JsonContractResolver
{
    public class TestClass
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
    }
    [TestClass]
    public class WithoutVirtualContractResolverUnitTest
    {
        [TestMethod]
        public void ShouldSerialize_SkipsVirtualProperties()
        {
            var obj = new TestClass { Id = 1, Name = "Test" };
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new WithoutVirtualContractResolver(),
                Formatting = Newtonsoft.Json.Formatting.None
            };
            var json = JsonConvert.SerializeObject(obj, settings);
            Assert.IsTrue(json.Contains("Id"));
            Assert.IsFalse(json.Contains("Name"));
        }
    }
}

using Elect.Web.ITempDataDictionaryUtils;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Elect.Test.Web
{
    [TestClass]
    public class ITempDataDictionaryUnitTest
    {
        private Mock<ITempDataDictionary> _tempDataMock;

        [TestInitialize]
        public void Setup()
        {
            _tempDataMock = new Mock<ITempDataDictionary>();
            _tempDataMock.Setup(x => x[It.IsAny<string>()])
                        .Returns((string key) => _tempDataMock.Object.ContainsKey(key) ? 
                            _tempDataMock.Object[key] : null);
        }

        [TestMethod]
        public void Set_StoresSerializedObject()
        {
            var testObject = new TestModel { Id = 1, Name = "Test" };
            var key = "test_key";
            var serializedValue = JsonConvert.SerializeObject(testObject);

            _tempDataMock.SetupSet(x => x[key] = It.IsAny<string>()).Verifiable();

            ITempDataDictionaryHelper.Set(_tempDataMock.Object, key, testObject);

            _tempDataMock.VerifySet(x => x[key] = serializedValue, Times.Once);
        }

        [TestMethod]
        public void Get_ReturnsDeserializedObject_WhenKeyExists()
        {
            var testObject = new TestModel { Id = 1, Name = "Test" };
            var key = "test_key";
            var serializedValue = JsonConvert.SerializeObject(testObject);

            object outValue;
            _tempDataMock.Setup(x => x.TryGetValue(key, out outValue))
                        .Returns((string k, out object v) => 
                        {
                            v = serializedValue;
                            return true;
                        });

            var result = ITempDataDictionaryHelper.Get<TestModel>(_tempDataMock.Object, key);

            Assert.IsNotNull(result);
            Assert.AreEqual(testObject.Id, result.Id);
            Assert.AreEqual(testObject.Name, result.Name);
        }

        [TestMethod]
        public void Get_ReturnsNull_WhenKeyDoesNotExist()
        {
            var key = "nonexistent_key";

            object outValue;
            _tempDataMock.Setup(x => x.TryGetValue(key, out outValue))
                        .Returns((string k, out object v) => 
                        {
                            v = null;
                            return false;
                        });

            var result = ITempDataDictionaryHelper.Get<TestModel>(_tempDataMock.Object, key);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Get_ReturnsNull_WhenValueIsNull()
        {
            var key = "null_key";

            object outValue;
            _tempDataMock.Setup(x => x.TryGetValue(key, out outValue))
                        .Returns((string k, out object v) => 
                        {
                            v = null;
                            return true;
                        });

            var result = ITempDataDictionaryHelper.Get<TestModel>(_tempDataMock.Object, key);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Extensions_Set_CallsHelperSet()
        {
            var testObject = new TestModel { Id = 1, Name = "Test" };
            var key = "test_key";
            var serializedValue = JsonConvert.SerializeObject(testObject);

            _tempDataMock.SetupSet(x => x[key] = It.IsAny<string>()).Verifiable();

            _tempDataMock.Object.Set(key, testObject);

            _tempDataMock.VerifySet(x => x[key] = serializedValue, Times.Once);
        }

        [TestMethod]
        public void Extensions_Get_CallsHelperGet()
        {
            var testObject = new TestModel { Id = 1, Name = "Test" };
            var key = "test_key";
            var serializedValue = JsonConvert.SerializeObject(testObject);

            object outValue;
            _tempDataMock.Setup(x => x.TryGetValue(key, out outValue))
                        .Returns((string k, out object v) => 
                        {
                            v = serializedValue;
                            return true;
                        });

            var result = _tempDataMock.Object.Get<TestModel>(key);

            Assert.IsNotNull(result);
            Assert.AreEqual(testObject.Id, result.Id);
            Assert.AreEqual(testObject.Name, result.Name);
        }

        public class TestModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
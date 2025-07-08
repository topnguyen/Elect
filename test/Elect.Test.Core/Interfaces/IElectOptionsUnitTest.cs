namespace Elect.Test.Core.Interfaces
{
    [TestClass]
    public class IElectOptionsUnitTest
    {
        private class TestOptions : IElectOptions { }
        [TestMethod]
        public void CanImplementIElectOptions()
        {
            var options = new TestOptions();
            Assert.IsInstanceOfType(options, typeof(IElectOptions));
        }
    }
}
namespace Elect.Test.Core.GuidUtils
{
    [TestClass]
    public class GuidHelperUnitTest
    {
        [TestMethod]
        public void Generate_ShouldReturnUniqueGuids()
        {
            var guid1 = GuidHelper.Generate();
            var guid2 = GuidHelper.Generate();
            Assert.AreNotEqual(guid1, guid2);
            Assert.AreNotEqual(Guid.Empty, guid1);
            Assert.AreNotEqual(Guid.Empty, guid2);
        }
        [TestMethod]
        public void IsGuidString_ValidGuid_ReturnsTrue()
        {
            var guid = Guid.NewGuid().ToString();
            Assert.IsTrue(GuidHelper.IsGuidString(guid));
        }
        [TestMethod]
        public void IsGuidString_InvalidGuid_ReturnsFalse()
        {
            Assert.IsFalse(GuidHelper.IsGuidString("not-a-guid"));
            Assert.IsFalse(GuidHelper.IsGuidString(""));
            Assert.IsFalse(GuidHelper.IsGuidString(null));
            Assert.IsFalse(GuidHelper.IsGuidString("   "));
        }
    }
}

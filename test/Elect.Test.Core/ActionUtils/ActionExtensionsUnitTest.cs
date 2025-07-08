namespace Elect.Test.Core.ActionUtils
{
    [TestClass]
    public class ActionExtensionsUnitTest
    {
        private class TestClass { public int Value { get; set; } }
        [TestMethod]
        public void GetValue_ShouldApplyActionAndReturnObject()
        {
            var result = new Action<TestClass>(x => x.Value = 42).GetValue();
            Assert.IsNotNull(result);
            Assert.AreEqual(42, result.Value);
        }
        [TestMethod]
        public void GetValue_ShouldReturnDefaultObjectIfNoAction()
        {
            var result = new Action<TestClass>(x => { }).GetValue();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Value);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_ShouldThrowIfActionIsNull()
        {
            Action<TestClass> action = null;
            action.GetValue();
        }
    }
}

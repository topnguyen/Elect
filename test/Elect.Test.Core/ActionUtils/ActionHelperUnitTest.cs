namespace Elect.Test.Core.ActionUtils
{
    [TestClass]
    public class ActionHelperUnitTest
    {
        private class TestClass { public int Value { get; set; } }
        [TestMethod]
        public void GetValue_ShouldApplyActionAndReturnObject()
        {
            var result = ActionHelper.GetValue<TestClass>(x => x.Value = 99);
            Assert.IsNotNull(result);
            Assert.AreEqual(99, result.Value);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValue_ShouldThrowIfActionIsNull()
        {
            ActionHelper.GetValue<TestClass>(null);
        }
        [TestMethod]
        [ExpectedException(typeof(TargetInvocationException))]
        public void GetValue_ShouldThrowIfActionThrows()
        {
            ActionHelper.GetValue<TestClass>(x => throw new InvalidOperationException());
        }
        [TestMethod]
        public void IgnoreError_Action_Success_ReturnsTrue()
        {
            var called = false;
            var result = ActionHelper.IgnoreError(() => called = true);
            Assert.IsTrue(result);
            Assert.IsTrue(called);
        }
        [TestMethod]
        public void IgnoreError_Action_Exception_ReturnsFalseAndCallsOnError()
        {
            Exception captured = null;
            var result = ActionHelper.IgnoreError(() => throw new Exception("fail"), ex => captured = ex);
            Assert.IsFalse(result);
            Assert.IsNotNull(captured);
            Assert.AreEqual("fail", captured.Message);
        }
        [TestMethod]
        public void IgnoreError_Action_Exception_ReturnsFalseWithoutOnError()
        {
            var result = ActionHelper.IgnoreError(() => throw new Exception("fail"));
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IgnoreError_Func_Success_ReturnsValue()
        {
            var result = ActionHelper.IgnoreError(() => 123);
            Assert.AreEqual(123, result);
        }
        [TestMethod]
        public void IgnoreError_Func_Exception_ReturnsDefaultAndCallsOnError()
        {
            Exception captured = null;
            var result = ActionHelper.IgnoreError<int>(() => throw new Exception("fail"), ex => captured = ex, 42);
            Assert.AreEqual(42, result);
            Assert.IsNotNull(captured);
            Assert.AreEqual("fail", captured.Message);
        }
        [TestMethod]
        public void IgnoreError_Func_Exception_ReturnsDefaultWithoutOnError()
        {
            var result = ActionHelper.IgnoreError<int>(() => throw new Exception("fail"), null, 77);
            Assert.AreEqual(77, result);
        }
        [TestMethod]
        public void IgnoreError_Func_NullOperation_ReturnsDefault()
        {
            var result = ActionHelper.IgnoreError<int>(null, null, 55);
            Assert.AreEqual(55, result);
        }
    }
}

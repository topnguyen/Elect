namespace Elect.Test.Core.ObjUtils
{
    [TestClass]
    public class ElectDisposableModelUnitTest
    {
        private class TestDisposable : ElectDisposableModel
        {
            public bool Disposed { get; private set; }
            protected override void DisposeUnmanagedResources()
            {
                Disposed = true;
            }
        }
        [TestMethod]
        public void Dispose_CallsDisposeUnmanagedResources()
        {
            var obj = new TestDisposable();
            obj.Dispose();
            Assert.IsTrue(obj.Disposed);
        }
        [TestMethod]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            var obj = new TestDisposable();
            obj.Dispose();
            obj.Dispose();
            Assert.IsTrue(obj.Disposed);
        }
    }
}

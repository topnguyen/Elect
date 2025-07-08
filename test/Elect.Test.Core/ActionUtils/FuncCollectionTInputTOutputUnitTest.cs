namespace Elect.Test.Core.ActionUtils
{
    [TestClass]
    public class FuncCollectionTInputTOutputUnitTest
    {
        [TestMethod]
        public void Add_ShouldAddFunc_AndReturnId()
        {
            var collection = new FuncCollection<int, string>();
            var id = collection.Add(x => x.ToString());
            Assert.IsFalse(string.IsNullOrEmpty(id));
            Assert.AreEqual(1, collection.Get().Count);
            Assert.AreEqual(id, collection.Get().First().Id);
        }
        [TestMethod]
        public void Get_ShouldReturnAllFuncs()
        {
            var collection = new FuncCollection<int, int>();
            collection.Add(x => x + 1);
            collection.Add(x => x * 2);
            var funcs = collection.Get();
            Assert.AreEqual(2, funcs.Count);
        }
        [TestMethod]
        public void Remove_ShouldRemoveFuncById()
        {
            var collection = new FuncCollection<string, int>();
            var id1 = collection.Add(x => x.Length);
            var id2 = collection.Add(x => x.GetHashCode());
            collection.Remove(id1);
            var funcs = collection.Get();
            Assert.AreEqual(1, funcs.Count);
            Assert.AreEqual(id2, funcs[0].Id);
        }
        [TestMethod]
        public void Empty_ShouldRemoveAllFuncs()
        {
            var collection = new FuncCollection<double, double>();
            collection.Add(x => x + 1);
            collection.Add(x => x * 2);
            collection.Empty();
            Assert.AreEqual(0, collection.Get().Count);
        }
        [TestMethod]
        public void Empty_ShouldDoNothingIfAlreadyEmpty()
        {
            var collection = new FuncCollection<int, int>();
            collection.Empty();
            Assert.AreEqual(0, collection.Get().Count);
        }
        private class TestFuncModel<TInput, TOutput> : FuncModel<TInput, TOutput>
        {
            public bool Disposed { get; private set; }
            public TestFuncModel(Func<TInput, TOutput> func) : base(func) { }
            protected override void Dispose(bool disposing)
            {
                Disposed = true;
                base.Dispose(disposing);
            }
        }
        private class TestFuncCollection<TInput, TOutput> : FuncCollection<TInput, TOutput>
        {
            public TestFuncCollection(IEnumerable<TestFuncModel<TInput, TOutput>> models)
            {
                Funcs = models.Cast<FuncModel<TInput, TOutput>>().ToList();
            }
            public List<FuncModel<TInput, TOutput>> GetFuncs() => Funcs;
            public void CallDisposeUnmanagedResources() => DisposeUnmanagedResources();
        }
        [TestMethod]
        public void DisposeUnmanagedResources_ShouldDisposeAllFuncs()
        {
            var models = new List<TestFuncModel<int, int>>
            {
                new TestFuncModel<int, int>(x => x + 1),
                new TestFuncModel<int, int>(x => x * 2)
            };
            var collection = new TestFuncCollection<int, int>(models);
            collection.CallDisposeUnmanagedResources();
            Assert.IsTrue(models.All(m => m.Disposed));
        }
    }
}

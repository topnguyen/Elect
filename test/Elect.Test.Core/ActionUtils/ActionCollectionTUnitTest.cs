namespace Elect.Test.Core.ActionUtils
{
    [TestClass]
    public class ActionCollectionTUnitTest
    {
        [TestMethod]
        public void Add_ShouldAddAction_AndReturnId()
        {
            var collection = new ActionCollection<int>();
            var id = collection.Add(x => { });
            Assert.IsFalse(string.IsNullOrEmpty(id));
            Assert.AreEqual(1, collection.Get().Count);
            Assert.AreEqual(id, collection.Get().First().Id);
        }
        [TestMethod]
        public void Get_ShouldReturnAllActions()
        {
            var collection = new ActionCollection<string>();
            collection.Add(x => { });
            collection.Add(x => { });
            var actions = collection.Get();
            Assert.AreEqual(2, actions.Count);
        }
        [TestMethod]
        public void Remove_ShouldRemoveActionById()
        {
            var collection = new ActionCollection<double>();
            var id1 = collection.Add(x => { });
            var id2 = collection.Add(x => { });
            collection.Remove(id1);
            var actions = collection.Get();
            Assert.AreEqual(1, actions.Count);
            Assert.AreEqual(id2, actions[0].Id);
        }
        [TestMethod]
        public void Empty_ShouldRemoveAllActions()
        {
            var collection = new ActionCollection<object>();
            collection.Add(x => { });
            collection.Add(x => { });
            collection.Empty();
            Assert.AreEqual(0, collection.Get().Count);
        }
        [TestMethod]
        public void Empty_ShouldDoNothingIfAlreadyEmpty()
        {
            var collection = new ActionCollection<int>();
            collection.Empty();
            Assert.AreEqual(0, collection.Get().Count);
        }
        private class TestActionModel<T> : ActionModel<T>
        {
            public bool Disposed { get; private set; }
            public TestActionModel(Action<T> action) : base(action) { }
            protected override void Dispose(bool disposing)
            {
                Disposed = true;
                base.Dispose(disposing);
            }
        }
        private class TestActionCollection<T> : ActionCollection<T>
        {
            public TestActionCollection(IEnumerable<TestActionModel<T>> models)
            {
                Actions = models.Cast<ActionModel<T>>().ToList();
            }
            public List<ActionModel<T>> GetActions() => Actions;
            public void CallDisposeUnmanagedResources() => DisposeUnmanagedResources();
        }
        [TestMethod]
        public void DisposeUnmanagedResources_ShouldDisposeAllActions()
        {
            var models = new List<TestActionModel<string>>
            {
                new TestActionModel<string>(x => { }),
                new TestActionModel<string>(x => { })
            };
            var collection = new TestActionCollection<string>(models);
            collection.CallDisposeUnmanagedResources();
            Assert.IsTrue(models.All(m => m.Disposed));
        }
    }
}

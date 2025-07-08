namespace Elect.Test.Core.ActionUtils
{
    [TestClass]
    public class ActionCollectionUnitTest
    {
        [TestMethod]
        public void Add_ShouldAddAction_AndReturnId()
        {
            var collection = new ActionCollection();
            var id = collection.Add(() => { });
            Assert.IsFalse(string.IsNullOrEmpty(id));
            Assert.AreEqual(1, collection.Get().Count);
            Assert.AreEqual(id, collection.Get().First().Id);
        }
        [TestMethod]
        public void Get_ShouldReturnAllActions()
        {
            var collection = new ActionCollection();
            collection.Add(() => { });
            collection.Add(() => { });
            var actions = collection.Get();
            Assert.AreEqual(2, actions.Count);
        }
        [TestMethod]
        public void Remove_ShouldRemoveActionById()
        {
            var collection = new ActionCollection();
            var id1 = collection.Add(() => { });
            var id2 = collection.Add(() => { });
            collection.Remove(id1);
            var actions = collection.Get();
            Assert.AreEqual(1, actions.Count);
            Assert.AreEqual(id2, actions[0].Id);
        }
        [TestMethod]
        public void Remove_ShouldDoNothingIfIdNotFound()
        {
            var collection = new ActionCollection();
            collection.Add(() => { });
            collection.Remove("notfound");
            Assert.AreEqual(1, collection.Get().Count);
        }
        [TestMethod]
        public void Empty_ShouldRemoveAllActions()
        {
            var collection = new ActionCollection();
            collection.Add(() => { });
            collection.Add(() => { });
            collection.Empty();
            Assert.AreEqual(0, collection.Get().Count);
        }
        [TestMethod]
        public void Empty_ShouldDoNothingIfAlreadyEmpty()
        {
            var collection = new ActionCollection();
            collection.Empty();
            Assert.AreEqual(0, collection.Get().Count);
        }
        private class TestActionModel : ActionModel
        {
            public bool Disposed { get; private set; }
            public TestActionModel(Action action) : base(action) { }
            protected override void Dispose(bool disposing)
            {
                Disposed = true;
                base.Dispose(disposing);
            }
        }
        private class TestActionCollection : ActionCollection
        {
            public TestActionCollection(IEnumerable<TestActionModel> models)
            {
                Actions = models.Cast<ActionModel>().ToList();
            }
            public List<ActionModel> GetActions() => Actions;
            public void CallDisposeUnmanagedResources() => DisposeUnmanagedResources();
        }
        [TestMethod]
        public void DisposeUnmanagedResources_ShouldDisposeAllActions()
        {
            var models = new List<TestActionModel>
            {
                new TestActionModel(() => { }),
                new TestActionModel(() => { })
            };
            var collection = new TestActionCollection(models);
            collection.CallDisposeUnmanagedResources();
            Assert.IsTrue(models.All(m => m.Disposed));
        }
    }
}

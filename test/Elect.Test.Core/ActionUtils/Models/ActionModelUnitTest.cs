namespace Elect.Test.Core.ActionUtils.Models
{
    [TestClass]
    public class ActionModelUnitTest
    {
        [TestMethod]
        public void Constructor_ShouldSetActionProperty()
        {
            Action action = () => { };
            var model = new ActionModel(action);
            Assert.AreEqual(action, model.Action);
        }
        [TestMethod]
        public void Id_ShouldBeUniqueAndNotNull()
        {
            var model1 = new ActionModel(() => { });
            var model2 = new ActionModel(() => { });
            Assert.IsFalse(string.IsNullOrEmpty(model1.Id));
            Assert.IsFalse(string.IsNullOrEmpty(model2.Id));
            Assert.AreNotEqual(model1.Id, model2.Id);
        }
    }
}

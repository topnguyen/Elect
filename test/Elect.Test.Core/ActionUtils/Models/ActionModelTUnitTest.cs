namespace Elect.Test.Core.ActionUtils.Models
{
    [TestClass]
    public class ActionModelTUnitTest
    {
        [TestMethod]
        public void Constructor_ShouldSetActionProperty()
        {
            Action<int> action = x => { };
            var model = new ActionModel<int>(action);
            Assert.AreEqual(action, model.Action);
        }
        [TestMethod]
        public void Id_ShouldBeUniqueAndNotNull()
        {
            var model1 = new ActionModel<string>(x => { });
            var model2 = new ActionModel<string>(x => { });
            Assert.IsFalse(string.IsNullOrEmpty(model1.Id));
            Assert.IsFalse(string.IsNullOrEmpty(model2.Id));
            Assert.AreNotEqual(model1.Id, model2.Id);
        }
    }
}

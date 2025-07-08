namespace Elect.Test.Core.ActionUtils.Models
{
    [TestClass]
    public class FuncModelTInputToutputUnitTest
    {
        [TestMethod]
        public void Constructor_ShouldSetFuncProperty()
        {
            Func<int, string> func = x => x.ToString();
            var model = new FuncModel<int, string>(func);
            Assert.AreEqual(func, model.Func);
        }
        [TestMethod]
        public void Id_ShouldBeUniqueAndNotNull()
        {
            var model1 = new FuncModel<int, int>(x => x + 1);
            var model2 = new FuncModel<int, int>(x => x * 2);
            Assert.IsFalse(string.IsNullOrEmpty(model1.Id));
            Assert.IsFalse(string.IsNullOrEmpty(model2.Id));
            Assert.AreNotEqual(model1.Id, model2.Id);
        }
    }
}

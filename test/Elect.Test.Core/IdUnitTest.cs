namespace Elect.Test.Core
{
    [TestClass]
    public class IdUnitTest
    {
        [TestMethod]
        public void GenerateShortIdCase()
        {
            const uint originalId = 2028044070;
            var idShortString = IdHelper.ToShortString(originalId);
            var idShort = IdHelper.FromShortString(idShortString);
            Assert.AreEqual(idShort, originalId);
        }
        [TestMethod]
        public void GenerateLongIdCase()
        {
            // https://www.youtube.com/watch?v=HCUPVi7XDqo
            const string originalId = "HCUPVi7XDqo";
            var id = IdHelper.FromString(originalId);
            var idString = IdHelper.ToString(id);
            Assert.AreEqual(idString, originalId);
        }
    }
}

namespace Elect.Test.Core.SimilarityUtils
{
    [TestClass]
    public class JaroWinklerUnitTest
    {
        [TestMethod]
        public void Similarity_IdenticalStrings_ReturnsOne()
        {
            var jw = new JaroWinkler();
            Assert.AreEqual(1.0, jw.Similarity("abc", "abc"), 1e-10);
        }
        [TestMethod]
        public void Similarity_CompletelyDifferentStrings_ReturnsLow()
        {
            var jw = new JaroWinkler();
            Assert.IsTrue(jw.Similarity("abc", "xyz") < 0.5);
        }
        [TestMethod]
        public void Similarity_EmptyStrings_ReturnsOne()
        {
            var jw = new JaroWinkler();
            Assert.AreEqual(1.0, jw.Similarity("", ""), 1e-10);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Similarity_NullFirst_Throws()
        {
            var jw = new JaroWinkler();
            jw.Similarity(null, "abc");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Similarity_NullSecond_Throws()
        {
            var jw = new JaroWinkler();
            jw.Similarity("abc", null);
        }
    }
}

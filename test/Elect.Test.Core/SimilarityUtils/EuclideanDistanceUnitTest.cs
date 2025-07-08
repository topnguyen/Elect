namespace Elect.Test.Core.SimilarityUtils
{
    [TestClass]
    public class EuclideanDistanceUnitTest
    {
        [TestMethod]
        public void Distance_ReturnsCorrectResult()
        {
            var a = new double[] { 1, 2, 3 };
            var b = new double[] { 4, 6, 8 };
            var result = Euclidean.Distance(a, b);
            Assert.AreEqual(Math.Sqrt(50), result, 1e-10);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Distance_DifferentLength_Throws()
        {
            var a = new double[] { 1, 2 };
            var b = new double[] { 1, 2, 3 };
            Euclidean.Distance(a, b);
        }
    }
}

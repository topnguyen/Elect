namespace Elect.Test.Core
{
    [TestClass]
    public class SimilarityUnitTest
    {
        [TestMethod]
        public void JaroWinklerCase()
        {
            var jw = new JaroWinkler();
            var twitter = jw.Similarity("Twitter", "twitter");
            var chien = jw.Similarity(("chien"), ("niche"));
            var twitterv1v2 = jw.Similarity("twitter v1", "Twitter v2");
            var Shazam = jw.Similarity("ShazamIphone", "ShazamAndroid");
            var FamosInstagramSW = jw.Similarity("Famos Instagram SW", "Famous Instagram");
            var IntFacebook1 = jw.Similarity("Int Facebook", "CI Facebook");
            var IntFacebook2 = jw.Similarity("Int Facebook", "Instagram Int");
        }
        [TestMethod]
        public void EuclideanCase()
        {
            var result1 = Euclidean.Distance(new double[] {0, 0}, new double[] {1, 0});
            var result2 = Euclidean.Distance(new double[] {0, 0}, new double[] {3, 2});
            var result3 = Euclidean.Distance(new double[] {-7, -4, 3}, new double[] {17, 6, 2.5});
            var result4 = Euclidean.Distance(new double[] {5, 13, 17, 3, 25, 21, 7, 1}, new double[] {20, 26, 7, 5, 28, 3, 23, 10});
        }
    }
}

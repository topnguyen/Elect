using Elect.Core.SimilarityUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
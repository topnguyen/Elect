using Elect.Core.SimilarityUtils;
using Elect.Core.StringUtils;
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

            var twitter = jw.Similarity(StringHelper.Normalize("Twitter"), StringHelper.Normalize("twitter"));
            var chien = jw.Similarity(StringHelper.Normalize("chien"), StringHelper.Normalize("niche"));
            var twitterv1v2 = jw.Similarity(StringHelper.Normalize("twitter v1"), StringHelper.Normalize("Twitter v2"));
            var Shazam = jw.Similarity(StringHelper.Normalize("ShazamIphone"), StringHelper.Normalize("ShazamAndroid"));
            var FamosInstagramSW = jw.Similarity(StringHelper.Normalize("Famos Instagram SW"), StringHelper.Normalize("Famous Instagram"));
            var IntFacebook1 = jw.Similarity(StringHelper.Normalize("Int Facebook"), StringHelper.Normalize("CI Facebook"));
            var IntFacebook2 = jw.Similarity(StringHelper.Normalize("Int Facebook"), StringHelper.Normalize("Instagram Int"));
        }
    }
}
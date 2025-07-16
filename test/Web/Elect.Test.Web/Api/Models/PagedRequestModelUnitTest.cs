namespace Elect.Test.Web.Api.Models
{
    [TestClass]
    public class PagedRequestModelUnitTest
    {
        [TestMethod]
        public void Skip_GetSet_WorksCorrectly()
        {
            var request = new PagedRequestModel();
            var skip = 50;
            
            request.Skip = skip;
            
            Assert.AreEqual(skip, request.Skip);
        }

        [TestMethod]
        public void Take_GetSet_WorksCorrectly()
        {
            var request = new PagedRequestModel();
            var take = 25;
            
            request.Take = take;
            
            Assert.AreEqual(take, request.Take);
        }

        [TestMethod]
        public void ExcludeIds_GetSet_WorksCorrectly()
        {
            var request = new PagedRequestModel();
            var excludeIds = "1,2,3,4,5";
            
            request.ExcludeIds = excludeIds;
            
            Assert.AreEqual(excludeIds, request.ExcludeIds);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var request = new PagedRequestModel();
            
            Assert.AreEqual(0, request.Skip);
            Assert.AreEqual(10, request.Take);
            Assert.IsNull(request.ExcludeIds);
        }

        [TestMethod]
        public void Skip_CanBeNegative()
        {
            var request = new PagedRequestModel();
            
            request.Skip = -10;
            
            Assert.AreEqual(-10, request.Skip);
        }

        [TestMethod]
        public void Take_CanBeZero()
        {
            var request = new PagedRequestModel();
            
            request.Take = 0;
            
            Assert.AreEqual(0, request.Take);
        }

        [TestMethod]
        public void Take_CanBeNegative()
        {
            var request = new PagedRequestModel();
            
            request.Take = -5;
            
            Assert.AreEqual(-5, request.Take);
        }

        [TestMethod]
        public void ExcludeIds_CanBeEmpty()
        {
            var request = new PagedRequestModel();
            
            request.ExcludeIds = string.Empty;
            
            Assert.AreEqual(string.Empty, request.ExcludeIds);
        }

        [TestMethod]
        public void ExcludeIds_CanBeWhitespace()
        {
            var request = new PagedRequestModel();
            var whitespace = "   ";
            
            request.ExcludeIds = whitespace;
            
            Assert.AreEqual(whitespace, request.ExcludeIds);
        }

        [TestMethod]
        public void InheritsFromElectDisposableModel()
        {
            var request = new PagedRequestModel();
            
            Assert.IsInstanceOfType(request, typeof(ElectDisposableModel));
        }

        [TestMethod]
        public void Properties_AreVirtual()
        {
            var request = new PagedRequestModel();
            var type = typeof(PagedRequestModel);
            
            var skipProperty = type.GetProperty("Skip");
            var takeProperty = type.GetProperty("Take");
            var excludeIdsProperty = type.GetProperty("ExcludeIds");
            
            Assert.IsTrue(skipProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(skipProperty.GetSetMethod().IsVirtual);
            Assert.IsTrue(takeProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(takeProperty.GetSetMethod().IsVirtual);
            Assert.IsTrue(excludeIdsProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(excludeIdsProperty.GetSetMethod().IsVirtual);
        }

        [TestMethod]
        public void Skip_LargeValues_WorkCorrectly()
        {
            var request = new PagedRequestModel();
            var largeSkip = int.MaxValue;
            
            request.Skip = largeSkip;
            
            Assert.AreEqual(largeSkip, request.Skip);
        }

        [TestMethod]
        public void Take_LargeValues_WorkCorrectly()
        {
            var request = new PagedRequestModel();
            var largeTake = int.MaxValue;
            
            request.Take = largeTake;
            
            Assert.AreEqual(largeTake, request.Take);
        }

        [TestMethod]
        public void ExcludeIds_ComplexString_WorksCorrectly()
        {
            var request = new PagedRequestModel();
            var complexIds = "abc-123,def-456,ghi-789";
            
            request.ExcludeIds = complexIds;
            
            Assert.AreEqual(complexIds, request.ExcludeIds);
        }
    }
}
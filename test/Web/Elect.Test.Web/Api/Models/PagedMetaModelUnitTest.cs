using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace Elect.Test.Web.Api.Models
{
    [TestClass]
    public class PagedMetaModelUnitTest
    {
        private Mock<IUrlHelper> _mockUrlHelper;
        private ActionContext _actionContext;

        [TestInitialize]
        public void Setup()
        {
            // Create concrete HttpContext instead of mocking
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/api/test";
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("example.com");
            
            _actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ControllerActionDescriptor()
            };
            
            _mockUrlHelper = new Mock<IUrlHelper>();
            _mockUrlHelper.Setup(x => x.ActionContext).Returns(_actionContext);
            _mockUrlHelper.Setup(x => x.Content(It.IsAny<string>())).Returns((string s) => s);
        }

        [TestMethod]
        public void Constructor_Default_CreatesInstance()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            
            Assert.IsNotNull(meta);
            Assert.IsNull(meta.Meta);
            Assert.IsNull(meta.First);
            Assert.IsNull(meta.Previous);
            Assert.IsNull(meta.Next);
            Assert.IsNull(meta.Last);
        }

        [TestMethod]
        public void Constructor_WithPagedResponse_CopiesProperties()
        {
            var pagedResponse = new PagedResponseModel<TestModel>
            {
                Total = 100,
                Items = new List<TestModel> { new TestModel(1, "item1"), new TestModel(2, "item2") },
                AdditionalData = new Dictionary<string, object> { { "key", "value" } }
            };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(pagedResponse);
            
            Assert.AreEqual(100, meta.Total);
            Assert.AreEqual(pagedResponse.Items, meta.Items);
            Assert.AreEqual(pagedResponse.AdditionalData, meta.AdditionalData);
        }

        [TestMethod]
        public void Constructor_WithUrlHelper_GeneratesLinks()
        {
            var pagedRequest = new PagedRequestModel { Skip = 10, Take = 10 };
            var pagedResponse = new PagedResponseModel<TestModel> { Total = 100 };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(_mockUrlHelper.Object, pagedRequest, pagedResponse);
            
            Assert.IsNotNull(meta.Meta);
            Assert.IsNotNull(meta.First);
            Assert.IsNotNull(meta.Previous);
            Assert.IsNotNull(meta.Next);
            Assert.IsNotNull(meta.Last);
        }

        [TestMethod]
        public void Meta_GetSet_WorksCorrectly()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            var linkModel = new LinkModel { Url = "https://example.com/meta" };
            
            meta.Meta = linkModel;
            
            Assert.AreEqual(linkModel, meta.Meta);
        }

        [TestMethod]
        public void First_GetSet_WorksCorrectly()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            var linkModel = new LinkModel { Url = "https://example.com/first" };
            
            meta.First = linkModel;
            
            Assert.AreEqual(linkModel, meta.First);
        }

        [TestMethod]
        public void Previous_GetSet_WorksCorrectly()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            var linkModel = new LinkModel { Url = "https://example.com/previous" };
            
            meta.Previous = linkModel;
            
            Assert.AreEqual(linkModel, meta.Previous);
        }

        [TestMethod]
        public void Next_GetSet_WorksCorrectly()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            var linkModel = new LinkModel { Url = "https://example.com/next" };
            
            meta.Next = linkModel;
            
            Assert.AreEqual(linkModel, meta.Next);
        }

        [TestMethod]
        public void Last_GetSet_WorksCorrectly()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            var linkModel = new LinkModel { Url = "https://example.com/last" };
            
            meta.Last = linkModel;
            
            Assert.AreEqual(linkModel, meta.Last);
        }

        [TestMethod]
        public void Properties_AreVirtual()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            var type = typeof(PagedMetaModel<PagedRequestModel, TestModel>);
            
            var metaProperty = type.GetProperty("Meta");
            var firstProperty = type.GetProperty("First");
            var previousProperty = type.GetProperty("Previous");
            var nextProperty = type.GetProperty("Next");
            var lastProperty = type.GetProperty("Last");
            
            Assert.IsTrue(metaProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(metaProperty.GetSetMethod().IsVirtual);
            Assert.IsTrue(firstProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(firstProperty.GetSetMethod().IsVirtual);
            Assert.IsTrue(previousProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(previousProperty.GetSetMethod().IsVirtual);
            Assert.IsTrue(nextProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(nextProperty.GetSetMethod().IsVirtual);
            Assert.IsTrue(lastProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(lastProperty.GetSetMethod().IsVirtual);
        }

        [TestMethod]
        public void GetPreviousLink_FirstPage_ReturnsNull()
        {
            var pagedRequest = new PagedRequestModel { Skip = 0, Take = 10 };
            var pagedResponse = new PagedResponseModel<TestModel> { Total = 100 };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(_mockUrlHelper.Object, pagedRequest, pagedResponse);
            
            Assert.IsNull(meta.Previous);
        }

        [TestMethod]
        public void GetNextLink_LastPage_ReturnsNull()
        {
            var pagedRequest = new PagedRequestModel { Skip = 90, Take = 10 };
            var pagedResponse = new PagedResponseModel<TestModel> { Total = 100 };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(_mockUrlHelper.Object, pagedRequest, pagedResponse);
            
            Assert.IsNull(meta.Next);
        }

        [TestMethod]
        public void GetLastLink_SmallDataSet_ReturnsNull()
        {
            var pagedRequest = new PagedRequestModel { Skip = 0, Take = 20 };
            var pagedResponse = new PagedResponseModel<TestModel> { Total = 15 };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(_mockUrlHelper.Object, pagedRequest, pagedResponse);
            
            Assert.IsNull(meta.Last);
        }

        [TestMethod]
        public void GetLastLink_OnLastPage_ReturnsNull()
        {
            var pagedRequest = new PagedRequestModel { Skip = 90, Take = 10 };
            var pagedResponse = new PagedResponseModel<TestModel> { Total = 100 };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(_mockUrlHelper.Object, pagedRequest, pagedResponse);
            
            Assert.IsNull(meta.Last);
        }

        [TestMethod]
        public void GetPreviousLink_ExcessiveSkip_ReturnsNull()
        {
            var pagedRequest = new PagedRequestModel { Skip = 200, Take = 10 };
            var pagedResponse = new PagedResponseModel<TestModel> { Total = 100 };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(_mockUrlHelper.Object, pagedRequest, pagedResponse);
            
            Assert.IsNull(meta.Previous);
        }

        [TestMethod]
        public void Constructor_WithHttpMethodPost_SetsCorrectMethod()
        {
            var pagedRequest = new PagedRequestModel { Skip = 10, Take = 10 };
            var pagedResponse = new PagedResponseModel<TestModel> { Total = 100 };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(_mockUrlHelper.Object, pagedRequest, pagedResponse, HttpMethod.POST);
            
            Assert.AreEqual(HttpMethod.POST, meta.Meta.Method);
            Assert.AreEqual(HttpMethod.POST, meta.First.Method);
            Assert.AreEqual(HttpMethod.POST, meta.Previous.Method);
            Assert.AreEqual(HttpMethod.POST, meta.Next.Method);
            Assert.AreEqual(HttpMethod.POST, meta.Last.Method);
        }

        [TestMethod]
        public void InheritsFromPagedResponseModel()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            
            Assert.IsInstanceOfType(meta, typeof(PagedResponseModel<TestModel>));
        }

        [TestMethod]
        public void LinksContainCorrectData()
        {
            var pagedRequest = new PagedRequestModel { Skip = 20, Take = 10 };
            var pagedResponse = new PagedResponseModel<TestModel> { Total = 100 };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(_mockUrlHelper.Object, pagedRequest, pagedResponse);
            
            Assert.IsNotNull(meta.Meta.Data);
            Assert.IsNotNull(meta.First.Data);
            Assert.IsNotNull(meta.Previous.Data);
            Assert.IsNotNull(meta.Next.Data);
            Assert.IsNotNull(meta.Last.Data);
        }

        [TestMethod]
        public void AllLinksHaveUrls()
        {
            var pagedRequest = new PagedRequestModel { Skip = 20, Take = 10 };
            var pagedResponse = new PagedResponseModel<TestModel> { Total = 100 };
            
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>(_mockUrlHelper.Object, pagedRequest, pagedResponse);
            
            Assert.IsNotNull(meta.Meta.Url);
            Assert.IsNotNull(meta.First.Url);
            Assert.IsNotNull(meta.Previous.Url);
            Assert.IsNotNull(meta.Next.Url);
            Assert.IsNotNull(meta.Last.Url);
        }

        [TestMethod]
        public void Meta_CanBeSetToNull()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            
            meta.Meta = null;
            
            Assert.IsNull(meta.Meta);
        }

        [TestMethod]
        public void First_CanBeSetToNull()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            
            meta.First = null;
            
            Assert.IsNull(meta.First);
        }

        [TestMethod]
        public void Previous_CanBeSetToNull()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            
            meta.Previous = null;
            
            Assert.IsNull(meta.Previous);
        }

        [TestMethod]
        public void Next_CanBeSetToNull()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            
            meta.Next = null;
            
            Assert.IsNull(meta.Next);
        }

        [TestMethod]
        public void Last_CanBeSetToNull()
        {
            var meta = new PagedMetaModel<PagedRequestModel, TestModel>();
            
            meta.Last = null;
            
            Assert.IsNull(meta.Last);
        }
    }
}
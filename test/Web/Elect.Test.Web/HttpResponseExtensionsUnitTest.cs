namespace Elect.Test.Web
{
    [TestClass]
    public class HttpResponseExtensionsUnitTest
    {
        [TestMethod]
        public async Task WriteAsync_DelegatesToHttpContext()
        {
            var context = new DefaultHttpContext();
            var actionResult = new OkResult();
            var executorMock = new Mock<IActionResultExecutor<OkResult>>();
            executorMock.Setup(e => e.ExecuteAsync(It.IsAny<ActionContext>(), actionResult))
                .Returns(Task.CompletedTask)
                .Verifiable();
            context.RequestServices = new ServiceProviderStub(typeof(IActionResultExecutor<OkResult>), executorMock.Object);
            await context.Response.WriteAsync(actionResult);
            executorMock.Verify();
        }

        private class ServiceProviderStub : IServiceProvider
        {
            private readonly System.Type _type;
            private readonly object _instance;
            public ServiceProviderStub(System.Type type, object instance) { _type = type; _instance = instance; }
            public object GetService(System.Type serviceType) => serviceType == _type ? _instance : null;
        }
    }
}

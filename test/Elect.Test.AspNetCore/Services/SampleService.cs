using Elect.Core.StringUtils;
using Elect.DI.Attributes;

namespace Elect.Test.AspNetCore.Services
{
    [ScopedDependency(ServiceType = typeof(ISampleService))]
    public class SampleService : ISampleService
    {
        public string GetSampleText()
        {
            return StringHelper.Generate(5);
        }
    }
}
using Elect.Web.SiteMap.Attributes;
using Elect.Web.SiteMap.Models;

namespace Elect.Test.Web.SiteMap
{
    [TestClass]
    public class SiteMapItemAttributeUnitTest
    {
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            var frequency = SiteMapItemFrequency.Daily;
            var priority = 0.8;

            var attribute = new SiteMapItemAttribute(frequency, priority);

            Assert.AreEqual(frequency, attribute.Frequency);
            Assert.AreEqual(priority, attribute.Priority);
        }

        [TestMethod]
        public void Properties_CanBeModified()
        {
            var attribute = new SiteMapItemAttribute(SiteMapItemFrequency.Weekly, 0.5);
            
            attribute.Frequency = SiteMapItemFrequency.Monthly;
            attribute.Priority = 0.9;

            Assert.AreEqual(SiteMapItemFrequency.Monthly, attribute.Frequency);
            Assert.AreEqual(0.9, attribute.Priority);
        }

        [TestMethod]
        public void CanBeAppliedToClass()
        {
            var type = typeof(TestClassWithAttribute);
            var attributes = type.GetCustomAttributes(typeof(SiteMapItemAttribute), false);
            
            Assert.AreEqual(1, attributes.Length);
            var attr = (SiteMapItemAttribute)attributes[0];
            Assert.AreEqual(SiteMapItemFrequency.Daily, attr.Frequency);
            Assert.AreEqual(0.8, attr.Priority);
        }

        [TestMethod]
        public void CanBeAppliedToMethod()
        {
            var methodInfo = typeof(TestClassWithAttribute).GetMethod(nameof(TestClassWithAttribute.TestMethod));
            var attributes = methodInfo.GetCustomAttributes(typeof(SiteMapItemAttribute), false);
            
            Assert.AreEqual(1, attributes.Length);
            var attr = (SiteMapItemAttribute)attributes[0];
            Assert.AreEqual(SiteMapItemFrequency.Weekly, attr.Frequency);
            Assert.AreEqual(0.6, attr.Priority);
        }

        [SiteMapItem(SiteMapItemFrequency.Daily, 0.8)]
        private class TestClassWithAttribute
        {
            [SiteMapItem(SiteMapItemFrequency.Weekly, 0.6)]
            public void TestMethod() { }
        }
    }
}
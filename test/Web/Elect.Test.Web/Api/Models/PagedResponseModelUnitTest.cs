namespace Elect.Test.Web.Api.Models
{
    [TestClass]
    public class PagedResponseModelUnitTest
    {
        [TestMethod]
        public void Total_GetSet_WorksCorrectly()
        {
            var response = new PagedResponseModel<TestModel>();
            var total = 150;
            
            response.Total = total;
            
            Assert.AreEqual(total, response.Total);
        }

        [TestMethod]
        public void Items_GetSet_WorksCorrectly()
        {
            var response = new PagedResponseModel<TestModel>();
            var items = new List<TestModel> { new TestModel(1, "item1"), new TestModel(2, "item2") };
            
            response.Items = items;
            
            Assert.AreEqual(items, response.Items);
            Assert.AreEqual(2, response.Items.Count());
        }

        [TestMethod]
        public void AdditionalData_GetSet_WorksCorrectly()
        {
            var response = new PagedResponseModel<TestModel>();
            var additionalData = new Dictionary<string, object>
            {
                { "key1", "value1" },
                { "key2", 42 }
            };
            
            response.AdditionalData = additionalData;
            
            Assert.AreEqual(additionalData, response.AdditionalData);
            Assert.AreEqual("value1", response.AdditionalData["key1"]);
            Assert.AreEqual(42, response.AdditionalData["key2"]);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var response = new PagedResponseModel<TestModel>();
            
            Assert.AreEqual(0, response.Total);
            Assert.IsNull(response.Items);
            Assert.IsNotNull(response.AdditionalData);
            Assert.AreEqual(0, response.AdditionalData.Count);
        }

        [TestMethod]
        public void Total_CanBeNegative()
        {
            var response = new PagedResponseModel<TestModel>();
            
            response.Total = -10;
            
            Assert.AreEqual(-10, response.Total);
        }

        [TestMethod]
        public void Items_CanBeEmpty()
        {
            var response = new PagedResponseModel<TestModel>();
            var emptyItems = new List<TestModel>();
            
            response.Items = emptyItems;
            
            Assert.AreEqual(emptyItems, response.Items);
            Assert.AreEqual(0, response.Items.Count());
        }

        [TestMethod]
        public void Items_CanBeNull()
        {
            var response = new PagedResponseModel<TestModel>();
            
            response.Items = null;
            
            Assert.IsNull(response.Items);
        }

        [TestMethod]
        public void AdditionalData_CanBeModified()
        {
            var response = new PagedResponseModel<TestModel>();
            
            response.AdditionalData["newKey"] = "newValue";
            response.AdditionalData["numberKey"] = 123;
            
            Assert.AreEqual("newValue", response.AdditionalData["newKey"]);
            Assert.AreEqual(123, response.AdditionalData["numberKey"]);
            Assert.AreEqual(2, response.AdditionalData.Count);
        }

        [TestMethod]
        public void AdditionalData_CanBeSetToNull()
        {
            var response = new PagedResponseModel<TestModel>();
            
            response.AdditionalData = null;
            
            Assert.IsNull(response.AdditionalData);
        }

        [TestMethod]
        public void InheritsFromElectDisposableModel()
        {
            var response = new PagedResponseModel<TestModel>();
            
            Assert.IsInstanceOfType(response, typeof(ElectDisposableModel));
        }

        [TestMethod]
        public void Properties_AreVirtual()
        {
            var type = typeof(PagedResponseModel<TestModel>);
            
            var totalProperty = type.GetProperty("Total");
            var itemsProperty = type.GetProperty("Items");
            var additionalDataProperty = type.GetProperty("AdditionalData");
            
            Assert.IsTrue(totalProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(totalProperty.GetSetMethod().IsVirtual);
            Assert.IsTrue(itemsProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(itemsProperty.GetSetMethod().IsVirtual);
            Assert.IsTrue(additionalDataProperty.GetGetMethod().IsVirtual);
            Assert.IsTrue(additionalDataProperty.GetSetMethod().IsVirtual);
        }

        [TestMethod]
        public void Total_LargeValues_WorkCorrectly()
        {
            var response = new PagedResponseModel<TestModel>();
            var largeTotal = int.MaxValue;
            
            response.Total = largeTotal;
            
            Assert.AreEqual(largeTotal, response.Total);
        }

        [TestMethod]
        public void AdditionalData_ComplexObjects_WorkCorrectly()
        {
            var response = new PagedResponseModel<TestModel>();
            var complexObject = new { Name = "Test", Value = 123 };
            
            response.AdditionalData["complex"] = complexObject;
            
            Assert.AreEqual(complexObject, response.AdditionalData["complex"]);
        }
    }
}
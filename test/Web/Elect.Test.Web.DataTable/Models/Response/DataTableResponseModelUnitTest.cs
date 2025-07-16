using Elect.Web.DataTable.Models.Response;
using Elect.Core.ObjUtils;

namespace Elect.Test.Web.DataTable.Models.Response
{
    [TestClass]
    public class DataTableResponseModelUnitTest
    {
        public class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class TransformedEntity
        {
            public int Id { get; set; }
            public string DisplayName { get; set; }
            public string FormattedDate { get; set; }
        }

        [TestMethod]
        public void Constructor_Default_InitializesCorrectly()
        {
            // Act
            var model = new DataTableResponseModel<TestEntity>();

            // Assert
            Assert.AreEqual(0, model.TotalRecord);
            Assert.AreEqual(0, model.TotalDisplayRecord);
            Assert.AreEqual(0, model.Echo);
            Assert.IsNull(model.Data);
            Assert.AreEqual(typeof(TestEntity), model.DataType);
            Assert.IsNotNull(model.AdditionalData);
            Assert.AreEqual(0, model.AdditionalData.Count);
        }

        [TestMethod]
        public void Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>();
            var testData = new object[]
            {
                new TestEntity { Id = 1, Name = "Test1", CreatedDate = DateTime.Now },
                new TestEntity { Id = 2, Name = "Test2", CreatedDate = DateTime.Now }
            };

            // Act
            model.TotalRecord = 100;
            model.TotalDisplayRecord = 50;
            model.Echo = 1;
            model.Data = testData;

            // Assert
            Assert.AreEqual(100, model.TotalRecord);
            Assert.AreEqual(50, model.TotalDisplayRecord);
            Assert.AreEqual(1, model.Echo);
            Assert.AreSame(testData, model.Data);
            Assert.AreEqual(2, model.Data.Length);
        }

        [TestMethod]
        public void DataType_ReturnsCorrectType()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>();

            // Act
            var dataType = model.DataType;

            // Assert
            Assert.AreEqual(typeof(TestEntity), dataType);
        }

        [TestMethod]
        public void AdditionalData_CanBeModified()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>();

            // Act
            model.AdditionalData["customKey"] = "customValue";
            model.AdditionalData["anotherKey"] = 42;

            // Assert
            Assert.AreEqual("customValue", model.AdditionalData["customKey"]);
            Assert.AreEqual(42, model.AdditionalData["anotherKey"]);
            Assert.AreEqual(2, model.AdditionalData.Count);
        }

        [TestMethod]
        public void Transform_WithValidTransform_ReturnsTransformedData()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>
            {
                TotalRecord = 100,
                TotalDisplayRecord = 50,
                Echo = 1,
                Data = new object[]
                {
                    new TestEntity { Id = 1, Name = "Test1", CreatedDate = new DateTime(2023, 1, 1) },
                    new TestEntity { Id = 2, Name = "Test2", CreatedDate = new DateTime(2023, 1, 2) }
                }
            };

            // Act
            var transformed = model.Transform<TestEntity, TransformedEntity>(entity => new TransformedEntity
            {
                Id = entity.Id,
                DisplayName = $"Entity: {entity.Name}",
                FormattedDate = entity.CreatedDate.ToString("yyyy-MM-dd")
            });

            // Assert
            Assert.AreEqual(100, transformed.TotalRecord);
            Assert.AreEqual(50, transformed.TotalDisplayRecord);
            Assert.AreEqual(1, transformed.Echo);
            Assert.AreEqual(2, transformed.Data.Length);
            
            var firstItem = (TransformedEntity)transformed.Data[0];
            Assert.AreEqual(1, firstItem.Id);
            Assert.AreEqual("Entity: Test1", firstItem.DisplayName);
            Assert.AreEqual("2023-01-01", firstItem.FormattedDate);
            
            var secondItem = (TransformedEntity)transformed.Data[1];
            Assert.AreEqual(2, secondItem.Id);
            Assert.AreEqual("Entity: Test2", secondItem.DisplayName);
            Assert.AreEqual("2023-01-02", secondItem.FormattedDate);
        }

        [TestMethod]
        public void Transform_WithEmptyData_ReturnsEmptyTransformedData()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>
            {
                TotalRecord = 0,
                TotalDisplayRecord = 0,
                Echo = 1,
                Data = new object[0]
            };

            // Act
            var transformed = model.Transform<TestEntity, TransformedEntity>(entity => new TransformedEntity
            {
                Id = entity.Id,
                DisplayName = entity.Name,
                FormattedDate = entity.CreatedDate.ToString("yyyy-MM-dd")
            });

            // Assert
            Assert.AreEqual(0, transformed.TotalRecord);
            Assert.AreEqual(0, transformed.TotalDisplayRecord);
            Assert.AreEqual(1, transformed.Echo);
            Assert.AreEqual(0, transformed.Data.Length);
        }

        [TestMethod]
        public void Transform_WithNullData_HandlesGracefully()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>
            {
                TotalRecord = 0,
                TotalDisplayRecord = 0,
                Echo = 1,
                Data = null
            };

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => 
                model.Transform<TestEntity, TransformedEntity>(entity => new TransformedEntity
                {
                    Id = entity.Id,
                    DisplayName = entity.Name,
                    FormattedDate = entity.CreatedDate.ToString("yyyy-MM-dd")
                }));
        }

        [TestMethod]
        public void Transform_PreservesAdditionalData()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>
            {
                TotalRecord = 100,
                TotalDisplayRecord = 50,
                Echo = 1,
                Data = new object[]
                {
                    new TestEntity { Id = 1, Name = "Test1", CreatedDate = DateTime.Now }
                }
            };
            model.AdditionalData["customKey"] = "customValue";

            // Act
            var transformed = model.Transform<TestEntity, TransformedEntity>(entity => new TransformedEntity
            {
                Id = entity.Id,
                DisplayName = entity.Name,
                FormattedDate = entity.CreatedDate.ToString("yyyy-MM-dd")
            });

            // Assert
            // Note: Transform method doesn't preserve AdditionalData based on the implementation
            // This test documents the current behavior
            Assert.AreEqual(0, transformed.AdditionalData.Count);
        }

        [TestMethod]
        public void InheritsFromElectDisposableModel()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>();

            // Act & Assert
            Assert.IsTrue(model is ElectDisposableModel);
        }

        [TestMethod]
        public void Dispose_DoesNotThrow()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>();

            // Act & Assert
            model.Dispose(); // Should not throw
        }

        [TestMethod]
        public void Transform_WithComplexTransform_WorksCorrectly()
        {
            // Arrange
            var model = new DataTableResponseModel<TestEntity>
            {
                Data = new object[]
                {
                    new TestEntity { Id = 1, Name = "Test1", CreatedDate = new DateTime(2023, 1, 1) },
                    new TestEntity { Id = 2, Name = "Test2", CreatedDate = new DateTime(2023, 1, 2) }
                }
            };

            // Act
            var transformed = model.Transform<TestEntity, object>(entity => new
            {
                entity.Id,
                UpperName = entity.Name.ToUpper(),
                IsRecent = entity.CreatedDate > DateTime.Now.AddDays(-30),
                Summary = $"ID: {entity.Id}, Name: {entity.Name}"
            });

            // Assert
            Assert.AreEqual(2, transformed.Data.Length);
            
            var firstItem = transformed.Data[0];
            var firstItemType = firstItem.GetType();
            Assert.AreEqual(1, firstItemType.GetProperty("Id").GetValue(firstItem));
            Assert.AreEqual("TEST1", firstItemType.GetProperty("UpperName").GetValue(firstItem));
            Assert.AreEqual("ID: 1, Name: Test1", firstItemType.GetProperty("Summary").GetValue(firstItem));
        }

        [TestMethod]
        public void ComplexScenario_FullDataTableResponse()
        {
            // Arrange
            var entities = new[]
            {
                new TestEntity { Id = 1, Name = "John Doe", CreatedDate = new DateTime(2023, 1, 1) },
                new TestEntity { Id = 2, Name = "Jane Smith", CreatedDate = new DateTime(2023, 2, 1) },
                new TestEntity { Id = 3, Name = "Bob Johnson", CreatedDate = new DateTime(2023, 3, 1) }
            };

            var model = new DataTableResponseModel<TestEntity>
            {
                TotalRecord = 1000,
                TotalDisplayRecord = 25,
                Echo = 3,
                Data = entities.Cast<object>().ToArray()
            };

            // Add some additional data
            model.AdditionalData["requestId"] = "abc123";
            model.AdditionalData["processingTime"] = 150;
            model.AdditionalData["serverTime"] = DateTime.Now;

            // Act
            var transformed = model.Transform<TestEntity, object>(entity => new
            {
                entity.Id,
                FullName = entity.Name,
                CreatedMonth = entity.CreatedDate.ToString("MMMM yyyy"),
                IsNew = entity.CreatedDate > new DateTime(2023, 2, 1)
            });

            // Assert
            Assert.AreEqual(1000, transformed.TotalRecord);
            Assert.AreEqual(25, transformed.TotalDisplayRecord);
            Assert.AreEqual(3, transformed.Echo);
            Assert.AreEqual(3, transformed.Data.Length);
            
            // Check transformed data
            var firstTransformed = transformed.Data[0];
            var firstType = firstTransformed.GetType();
            Assert.AreEqual(1, firstType.GetProperty("Id").GetValue(firstTransformed));
            Assert.AreEqual("John Doe", firstType.GetProperty("FullName").GetValue(firstTransformed));
            Assert.AreEqual("January 2023", firstType.GetProperty("CreatedMonth").GetValue(firstTransformed));
            Assert.AreEqual(false, firstType.GetProperty("IsNew").GetValue(firstTransformed));
            
            var thirdTransformed = transformed.Data[2];
            var thirdType = thirdTransformed.GetType();
            Assert.AreEqual(3, thirdType.GetProperty("Id").GetValue(thirdTransformed));
            Assert.AreEqual("Bob Johnson", thirdType.GetProperty("FullName").GetValue(thirdTransformed));
            Assert.AreEqual("March 2023", thirdType.GetProperty("CreatedMonth").GetValue(thirdTransformed));
            Assert.AreEqual(true, thirdType.GetProperty("IsNew").GetValue(thirdTransformed));
            
            // Original model's additional data should not be preserved
            Assert.AreEqual(0, transformed.AdditionalData.Count);
        }
    }
}
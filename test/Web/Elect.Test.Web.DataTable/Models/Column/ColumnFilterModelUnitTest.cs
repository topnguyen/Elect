using Elect.Web.DataTable.Models.Column;
using Elect.Web.DataTable.Models.Constants;
using Elect.Web.DataTable.Models.Options;
using System.Collections;

namespace Elect.Test.Web.DataTable.Models.Column
{
    [TestClass]
    public class ColumnFilterModelUnitTest
    {
        [TestInitialize]
        public void Setup()
        {
            // Initialize the ElectDataTableOptions instance for tests
            if (ElectDataTableOptions.Instance == null)
            {
                ElectDataTableOptions.Instance = new ElectDataTableOptions();
            }
        }

        [TestMethod]
        public void Constructor_WithStringType_SetsTextFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(string));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Text, filter[FilterConstants.Type]);
        }

        [TestMethod]
        public void Constructor_WithIntType_SetsTextFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(int));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Text, filter[FilterConstants.Type]);
        }

        [TestMethod]
        public void Constructor_WithBoolType_SetsSelectFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(bool));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Select, filter[FilterConstants.Type]);
            Assert.IsNotNull(filter[FilterConstants.Values]);
            
            var values = (object[])filter[FilterConstants.Values];
            Assert.AreEqual(2, values.Length);
        }

        [TestMethod]
        public void Constructor_WithNullableBoolType_SetsSelectFilterWithNullOption()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(bool?));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Select, filter[FilterConstants.Type]);
            Assert.IsNotNull(filter[FilterConstants.Values]);
            
            var values = (object[])filter[FilterConstants.Values];
            Assert.AreEqual(3, values.Length);
            Assert.AreEqual(DataConstants.Null, values[0]);
            Assert.AreEqual(DataConstants.True, values[1]);
            Assert.AreEqual(DataConstants.False, values[2]);
        }

        [TestMethod]
        public void Constructor_WithDateTimeType_SetsTextFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(DateTime));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Text, filter[FilterConstants.Type]);
        }

        [TestMethod]
        public void Constructor_WithNullableDateTimeType_SetsTextFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(DateTime?));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Text, filter[FilterConstants.Type]);
        }

        [TestMethod]
        public void Constructor_WithDateTimeOffsetType_SetsTextFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(DateTimeOffset));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Text, filter[FilterConstants.Type]);
        }

        [TestMethod]
        public void Constructor_WithNullableDateTimeOffsetType_SetsTextFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(DateTimeOffset?));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Text, filter[FilterConstants.Type]);
        }

        [TestMethod]
        public void Constructor_WithEnumType_SetsSelectFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(SortDirection));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Select, filter[FilterConstants.Type]);
            Assert.IsNotNull(filter[FilterConstants.Values]);
            
            var values = (object[])filter[FilterConstants.Values];
            Assert.IsTrue(values.Length > 0);
        }

        [TestMethod]
        public void Constructor_WithNullableEnumType_SetsSelectFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(SortDirection?));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Select, filter[FilterConstants.Type]);
            Assert.IsNotNull(filter[FilterConstants.Values]);
            
            var values = (object[])filter[FilterConstants.Values];
            Assert.IsTrue(values.Length > 0);
        }

        [TestMethod]
        public void Constructor_WithNullType_RemovesTypeProperty()
        {
            // Act
            var filter = new ColumnFilterModel(null);

            // Assert
            Assert.IsNotNull(filter);
            Assert.IsFalse(filter.ContainsKey(FilterConstants.Type));
        }

        [TestMethod]
        public void InheritsFromHashtable()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(string));

            // Assert
            Assert.IsTrue(filter is Hashtable);
        }

        [TestMethod]
        public void CanBeUsedAsHashtable()
        {
            // Arrange
            var filter = new ColumnFilterModel(typeof(string));

            // Act
            filter["customProperty"] = "customValue";
            filter["anotherProperty"] = 42;

            // Assert
            Assert.AreEqual("customValue", filter["customProperty"]);
            Assert.AreEqual(42, filter["anotherProperty"]);
            Assert.IsTrue(filter.ContainsKey("customProperty"));
            Assert.IsTrue(filter.ContainsKey("anotherProperty"));
        }

        [TestMethod]
        public void FilterValues_CanBeSetInternally()
        {
            // Arrange
            var filter = new ColumnFilterModel(typeof(string));
            var values = new object[] { "value1", "value2", "value3" };

            // Act - Using reflection to access internal property
            var property = typeof(ColumnFilterModel).GetProperty("FilterValues", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            property.SetValue(filter, values);

            // Assert
            Assert.AreEqual(values, filter[FilterConstants.Values]);
        }

        [TestMethod]
        public void FilterType_CanBeSetInternally()
        {
            // Arrange
            var filter = new ColumnFilterModel(typeof(string));
            var newType = FilterConstants.Select;

            // Act - Using reflection to access internal property
            var property = typeof(ColumnFilterModel).GetProperty("FilterType", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            property.SetValue(filter, newType);

            // Assert
            Assert.AreEqual(newType, filter[FilterConstants.Type]);
        }

        [TestMethod]
        public void Constructor_WithComplexType_SetsTextFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(List<string>));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Text, filter[FilterConstants.Type]);
        }

        [TestMethod]
        public void Constructor_WithCustomClass_SetsTextFilter()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(ColumnFilterModel));

            // Assert
            Assert.IsNotNull(filter);
            Assert.AreEqual(FilterConstants.Text, filter[FilterConstants.Type]);
        }

        [TestMethod]
        public void BooleanFilter_HasCorrectStructure()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(bool));
            var values = (object[])filter[FilterConstants.Values];

            // Assert
            Assert.AreEqual(2, values.Length);
            
            // Check the structure of the first value (True)
            var trueValue = values[0];
            var trueType = trueValue.GetType();
            Assert.IsNotNull(trueType.GetProperty("value"));
            Assert.IsNotNull(trueType.GetProperty("label"));
            
            // Check the structure of the second value (False)
            var falseValue = values[1];
            var falseType = falseValue.GetType();
            Assert.IsNotNull(falseType.GetProperty("value"));
            Assert.IsNotNull(falseType.GetProperty("label"));
        }

        [TestMethod]
        public void EnumFilter_HasCorrectStructure()
        {
            // Act
            var filter = new ColumnFilterModel(typeof(SortDirection));
            var values = (object[])filter[FilterConstants.Values];

            // Assert
            Assert.IsTrue(values.Length > 0);
            
            // Check the structure of the first value
            var firstValue = values[0];
            var firstType = firstValue.GetType();
            Assert.IsNotNull(firstType.GetProperty("value"));
            Assert.IsNotNull(firstType.GetProperty("label"));
        }

        [TestMethod]
        public void CanStoreMultipleCustomProperties()
        {
            // Arrange
            var filter = new ColumnFilterModel(typeof(string));

            // Act
            filter["prop1"] = "value1";
            filter["prop2"] = 42;
            filter["prop3"] = true;
            filter["prop4"] = new[] { "a", "b", "c" };

            // Assert
            Assert.AreEqual("value1", filter["prop1"]);
            Assert.AreEqual(42, filter["prop2"]);
            Assert.AreEqual(true, filter["prop3"]);
            Assert.IsTrue(((string[])filter["prop4"]).SequenceEqual(new[] { "a", "b", "c" }));
            Assert.IsTrue(filter.Count >= 5); // At least type + 4 custom properties
        }
    }
}
using Elect.Web.DataTable.Models.Column;
using Elect.Web.DataTable.Models.Constants;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Elect.Test.Web.DataTable.Models.Column
{
    [TestClass]
    public class ColumnModelUnitTest
    {
        [TestMethod]
        public void Constructor_WithValidParameters_InitializesCorrectly()
        {
            // Arrange
            var name = "TestColumn";
            var type = typeof(string);

            // Act
            var column = new ColumnModel(name, type);

            // Assert
            Assert.AreEqual(name, column.Name);
            Assert.AreEqual(name, column.DisplayName);
            Assert.AreEqual(type, column.Type);
            Assert.IsTrue(column.IsVisible);
            Assert.IsTrue(column.IsSortable);
            Assert.IsTrue(column.IsSearchable);
            Assert.AreEqual(string.Empty, column.CssClass);
            Assert.AreEqual(string.Empty, column.CssClassHeader);
            Assert.AreEqual(SortDirection.None, column.SortDirection);
            Assert.IsNotNull(column.ColumnFilter);
        }

        [TestMethod]
        public void Constructor_WithNullName_SetsNullName()
        {
            // Arrange
            string nullName = null;
            var type = typeof(string);

            // Act
            var column = new ColumnModel(nullName, type);

            // Assert
            Assert.IsNull(column.Name);
            Assert.IsNull(column.DisplayName);
            Assert.AreEqual(type, column.Type);
        }

        [TestMethod]
        public void Constructor_WithNullType_InitializesWithNullType()
        {
            // Arrange
            var name = "TestColumn";
            Type nullType = null;

            // Act
            var column = new ColumnModel(name, nullType);

            // Assert
            Assert.AreEqual(name, column.Name);
            Assert.AreEqual(name, column.DisplayName);
            Assert.IsNull(column.Type);
            Assert.IsNotNull(column.ColumnFilter);
        }

        [TestMethod]
        public void Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var column = new ColumnModel("Test", typeof(string));

            // Act
            column.Name = "NewName";
            column.DisplayName = "New Display Name";
            column.IsVisible = false;
            column.IsSortable = false;
            column.IsSearchable = false;
            column.CssClass = "custom-class";
            column.CssClassHeader = "header-class";
            column.SortDirection = SortDirection.Ascending;
            column.MRenderFunction = "customRender";
            column.Width = "100px";
            column.FilterColHint = "Enter value...";
            column.FilterColAdditionalAttribute = "data-toggle='tooltip'";
            column.AdditionalAttributeHeader = "data-sort='true'";

            // Assert
            Assert.AreEqual("NewName", column.Name);
            Assert.AreEqual("New Display Name", column.DisplayName);
            Assert.AreEqual(false, column.IsVisible);
            Assert.AreEqual(false, column.IsSortable);
            Assert.AreEqual(false, column.IsSearchable);
            Assert.AreEqual("custom-class", column.CssClass);
            Assert.AreEqual("header-class", column.CssClassHeader);
            Assert.AreEqual(SortDirection.Ascending, column.SortDirection);
            Assert.AreEqual("customRender", column.MRenderFunction);
            Assert.AreEqual("100px", column.Width);
            Assert.AreEqual("Enter value...", column.FilterColHint);
            Assert.AreEqual("data-toggle='tooltip'", column.FilterColAdditionalAttribute);
            Assert.AreEqual("data-sort='true'", column.AdditionalAttributeHeader);
        }

        [TestMethod]
        public void SortDirection_CanBeSetToAllValues()
        {
            // Arrange
            var column = new ColumnModel("Test", typeof(string));

            // Act & Assert
            column.SortDirection = SortDirection.None;
            Assert.AreEqual(SortDirection.None, column.SortDirection);

            column.SortDirection = SortDirection.Ascending;
            Assert.AreEqual(SortDirection.Ascending, column.SortDirection);

            column.SortDirection = SortDirection.Descending;
            Assert.AreEqual(SortDirection.Descending, column.SortDirection);
        }

        [TestMethod]
        public void ColumnFilter_IsInitializedInConstructor()
        {
            // Arrange
            var type = typeof(string);

            // Act
            var column = new ColumnModel("Test", type);

            // Assert
            Assert.IsNotNull(column.ColumnFilter);
            Assert.IsTrue(column.ColumnFilter is ColumnFilterModel);
        }

        [TestMethod]
        public void ColumnFilter_CanBeReplaced()
        {
            // Arrange
            var column = new ColumnModel("Test", typeof(string));
            var newFilter = new ColumnFilterModel(typeof(int));

            // Act
            column.ColumnFilter = newFilter;

            // Assert
            Assert.AreSame(newFilter, column.ColumnFilter);
        }

        [TestMethod]
        public void SearchColumns_CanBeSetAndRetrieved()
        {
            // Arrange
            var column = new ColumnModel("Test", typeof(string));
            var searchColumns = new JObject
            {
                { "searchTerm", "test" },
                { "searchType", "exact" }
            };

            // Act
            column.SearchColumns = searchColumns;

            // Assert
            Assert.AreSame(searchColumns, column.SearchColumns);
            Assert.AreEqual("test", column.SearchColumns["searchTerm"]);
            Assert.AreEqual("exact", column.SearchColumns["searchType"]);
        }

        [TestMethod]
        public void CustomAttributes_CanBeSetAndRetrieved()
        {
            // Arrange
            var column = new ColumnModel("Test", typeof(string));
            var attributes = new Attribute[]
            {
                new DisplayAttribute { Name = "Custom Display" },
                new RequiredAttribute()
            };

            // Act
            column.CustomAttributes = attributes;

            // Assert
            Assert.AreSame(attributes, column.CustomAttributes);
            Assert.AreEqual(2, column.CustomAttributes.Length);
            Assert.IsTrue(column.CustomAttributes[0] is DisplayAttribute);
            Assert.IsTrue(column.CustomAttributes[1] is RequiredAttribute);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            // Arrange & Act
            var column = new ColumnModel("Test", typeof(string));

            // Assert
            Assert.AreEqual(true, column.IsVisible);
            Assert.AreEqual(true, column.IsSortable);
            Assert.AreEqual(true, column.IsSearchable);
            Assert.AreEqual(string.Empty, column.CssClass);
            Assert.AreEqual(string.Empty, column.CssClassHeader);
            Assert.AreEqual(SortDirection.None, column.SortDirection);
            Assert.IsNull(column.MRenderFunction);
            Assert.IsNull(column.Width);
            Assert.IsNull(column.FilterColHint);
            Assert.IsNull(column.FilterColAdditionalAttribute);
            Assert.IsNull(column.AdditionalAttributeHeader);
            Assert.IsNull(column.SearchColumns);
            Assert.IsNull(column.CustomAttributes);
        }

        [TestMethod]
        public void WithDifferentTypes_InitializesColumnFilterCorrectly()
        {
            // Act & Assert - Test different types
            var stringColumn = new ColumnModel("StringCol", typeof(string));
            Assert.IsNotNull(stringColumn.ColumnFilter);

            var intColumn = new ColumnModel("IntCol", typeof(int));
            Assert.IsNotNull(intColumn.ColumnFilter);

            var boolColumn = new ColumnModel("BoolCol", typeof(bool));
            Assert.IsNotNull(boolColumn.ColumnFilter);

            var dateColumn = new ColumnModel("DateCol", typeof(DateTime));
            Assert.IsNotNull(dateColumn.ColumnFilter);

            var enumColumn = new ColumnModel("EnumCol", typeof(SortDirection));
            Assert.IsNotNull(enumColumn.ColumnFilter);
        }

        [TestMethod]
        public void ComplexConfiguration_WorksCorrectly()
        {
            // Arrange
            var column = new ColumnModel("ComplexColumn", typeof(DateTime));
            var customAttributes = new Attribute[]
            {
                new DisplayAttribute { Name = "Creation Date" },
                new RequiredAttribute()
            };
            var searchColumns = new JObject
            {
                { "format", "yyyy-MM-dd" },
                { "locale", "en-US" }
            };

            // Act
            column.DisplayName = "Creation Date";
            column.IsVisible = true;
            column.IsSortable = true;
            column.IsSearchable = false;
            column.CssClass = "date-column";
            column.CssClassHeader = "date-header";
            column.SortDirection = SortDirection.Descending;
            column.MRenderFunction = "formatDate";
            column.Width = "120px";
            column.FilterColHint = "Select date...";
            column.FilterColAdditionalAttribute = "data-datepicker='true'";
            column.AdditionalAttributeHeader = "data-sort-type='date'";
            column.CustomAttributes = customAttributes;
            column.SearchColumns = searchColumns;

            // Assert
            Assert.AreEqual("ComplexColumn", column.Name);
            Assert.AreEqual("Creation Date", column.DisplayName);
            Assert.AreEqual(typeof(DateTime), column.Type);
            Assert.AreEqual(true, column.IsVisible);
            Assert.AreEqual(true, column.IsSortable);
            Assert.AreEqual(false, column.IsSearchable);
            Assert.AreEqual("date-column", column.CssClass);
            Assert.AreEqual("date-header", column.CssClassHeader);
            Assert.AreEqual(SortDirection.Descending, column.SortDirection);
            Assert.AreEqual("formatDate", column.MRenderFunction);
            Assert.AreEqual("120px", column.Width);
            Assert.AreEqual("Select date...", column.FilterColHint);
            Assert.AreEqual("data-datepicker='true'", column.FilterColAdditionalAttribute);
            Assert.AreEqual("data-sort-type='date'", column.AdditionalAttributeHeader);
            Assert.AreSame(customAttributes, column.CustomAttributes);
            Assert.AreSame(searchColumns, column.SearchColumns);
            Assert.IsNotNull(column.ColumnFilter);
        }
    }
}
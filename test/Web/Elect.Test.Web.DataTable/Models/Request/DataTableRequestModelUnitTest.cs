using Elect.Web.DataTable.Models.Request;
using Elect.Core.ObjUtils;

namespace Elect.Test.Web.DataTable.Models.Request
{
    [TestClass]
    public class DataTableRequestModelUnitTest
    {
        [TestMethod]
        public void Constructor_Default_InitializesLists()
        {
            // Act
            var model = new DataTableRequestModel();

            // Assert
            Assert.IsNotNull(model.ColumnNames);
            Assert.IsNotNull(model.ColReorderIndexs);
            Assert.IsNotNull(model.ListIsSortable);
            Assert.IsNotNull(model.ListIsSearchable);
            Assert.IsNotNull(model.SearchValues);
            Assert.IsNotNull(model.SortCol);
            Assert.IsNotNull(model.SortDir);
            Assert.IsNotNull(model.ListIsEscapeRegexColumn);
            Assert.AreEqual(0, model.ColumnNames.Count);
            Assert.AreEqual(0, model.ColReorderIndexs.Count);
            Assert.AreEqual(0, model.ListIsSortable.Count);
            Assert.AreEqual(0, model.ListIsSearchable.Count);
            Assert.AreEqual(0, model.SearchValues.Count);
            Assert.AreEqual(0, model.SortCol.Count);
            Assert.AreEqual(0, model.SortDir.Count);
            Assert.AreEqual(0, model.ListIsEscapeRegexColumn.Count);
        }

        [TestMethod]
        public void Constructor_WithColumns_InitializesListsWithCapacity()
        {
            // Arrange
            var columnCount = 5;

            // Act
            var model = new DataTableRequestModel(columnCount);

            // Assert
            Assert.AreEqual(columnCount, model.Columns);
            Assert.IsNotNull(model.ColumnNames);
            Assert.IsNotNull(model.ColReorderIndexs);
            Assert.IsNotNull(model.ListIsSortable);
            Assert.IsNotNull(model.ListIsSearchable);
            Assert.IsNotNull(model.SearchValues);
            Assert.IsNotNull(model.SortCol);
            Assert.IsNotNull(model.SortDir);
            Assert.IsNotNull(model.ListIsEscapeRegexColumn);
            
            // Lists should be initialized with correct capacity
            Assert.AreEqual(columnCount, model.ColumnNames.Capacity);
            Assert.AreEqual(columnCount, model.ListIsSortable.Capacity);
            Assert.AreEqual(columnCount, model.ListIsSearchable.Capacity);
            Assert.AreEqual(columnCount, model.SearchValues.Capacity);
            Assert.AreEqual(columnCount, model.SortCol.Capacity);
            Assert.AreEqual(columnCount, model.SortDir.Capacity);
            Assert.AreEqual(columnCount, model.ListIsEscapeRegexColumn.Capacity);
        }

        [TestMethod]
        public void Properties_CanBeSetAndRetrieved()
        {
            // Arrange
            var model = new DataTableRequestModel();

            // Act
            model.DisplayStart = 10;
            model.DisplayLength = 25;
            model.Columns = 5;
            model.Search = "test search";
            model.EscapeRegex = true;
            model.SortingCols = 2;
            model.Echo = 1;

            // Assert
            Assert.AreEqual(10, model.DisplayStart);
            Assert.AreEqual(25, model.DisplayLength);
            Assert.AreEqual(5, model.Columns);
            Assert.AreEqual("test search", model.Search);
            Assert.AreEqual(true, model.EscapeRegex);
            Assert.AreEqual(2, model.SortingCols);
            Assert.AreEqual(1, model.Echo);
        }

        [TestMethod]
        public void Lists_CanBeModified()
        {
            // Arrange
            var model = new DataTableRequestModel();

            // Act
            model.ColumnNames.Add("Column1");
            model.ColumnNames.Add("Column2");
            model.ColReorderIndexs.Add(0);
            model.ColReorderIndexs.Add(1);
            model.ListIsSortable.Add(true);
            model.ListIsSortable.Add(false);
            model.ListIsSearchable.Add(true);
            model.ListIsSearchable.Add(true);
            model.SearchValues.Add("value1");
            model.SearchValues.Add("value2");
            model.SortCol.Add(0);
            model.SortCol.Add(1);
            model.SortDir.Add("asc");
            model.SortDir.Add("desc");
            model.ListIsEscapeRegexColumn.Add(true);
            model.ListIsEscapeRegexColumn.Add(false);

            // Assert
            Assert.AreEqual(2, model.ColumnNames.Count);
            Assert.AreEqual("Column1", model.ColumnNames[0]);
            Assert.AreEqual("Column2", model.ColumnNames[1]);
            Assert.AreEqual(2, model.ColReorderIndexs.Count);
            Assert.AreEqual(0, model.ColReorderIndexs[0]);
            Assert.AreEqual(1, model.ColReorderIndexs[1]);
            Assert.AreEqual(2, model.ListIsSortable.Count);
            Assert.AreEqual(true, model.ListIsSortable[0]);
            Assert.AreEqual(false, model.ListIsSortable[1]);
            Assert.AreEqual(2, model.ListIsSearchable.Count);
            Assert.AreEqual(true, model.ListIsSearchable[0]);
            Assert.AreEqual(true, model.ListIsSearchable[1]);
            Assert.AreEqual(2, model.SearchValues.Count);
            Assert.AreEqual("value1", model.SearchValues[0]);
            Assert.AreEqual("value2", model.SearchValues[1]);
            Assert.AreEqual(2, model.SortCol.Count);
            Assert.AreEqual(0, model.SortCol[0]);
            Assert.AreEqual(1, model.SortCol[1]);
            Assert.AreEqual(2, model.SortDir.Count);
            Assert.AreEqual("asc", model.SortDir[0]);
            Assert.AreEqual("desc", model.SortDir[1]);
            Assert.AreEqual(2, model.ListIsEscapeRegexColumn.Count);
            Assert.AreEqual(true, model.ListIsEscapeRegexColumn[0]);
            Assert.AreEqual(false, model.ListIsEscapeRegexColumn[1]);
        }

        [TestMethod]
        public void Data_CanBeSetAndRetrieved()
        {
            // Arrange
            var model = new DataTableRequestModel();
            var data = new Dictionary<string, object>
            {
                { "key1", "value1" },
                { "key2", 42 },
                { "key3", true }
            };

            // Act
            model.Data = data;

            // Assert
            Assert.AreSame(data, model.Data);
            Assert.AreEqual("value1", model.Data["key1"]);
            Assert.AreEqual(42, model.Data["key2"]);
            Assert.AreEqual(true, model.Data["key3"]);
        }

        [TestMethod]
        public void Data_CanBeNull()
        {
            // Arrange
            var model = new DataTableRequestModel();

            // Act
            model.Data = null;

            // Assert
            Assert.IsNull(model.Data);
        }

        [TestMethod]
        public void InheritsFromElectDisposableModel()
        {
            // Arrange
            var model = new DataTableRequestModel();

            // Act & Assert
            Assert.IsTrue(model is ElectDisposableModel);
        }

        [TestMethod]
        public void Dispose_DoesNotThrow()
        {
            // Arrange
            var model = new DataTableRequestModel();

            // Act & Assert
            model.Dispose(); // Should not throw
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            // Arrange & Act
            var model = new DataTableRequestModel();

            // Assert
            Assert.AreEqual(0, model.DisplayStart);
            Assert.AreEqual(0, model.DisplayLength);
            Assert.AreEqual(0, model.Columns);
            Assert.IsNull(model.Search);
            Assert.AreEqual(false, model.EscapeRegex);
            Assert.AreEqual(0, model.SortingCols);
            Assert.AreEqual(0, model.Echo);
            Assert.IsNull(model.Data);
        }

        [TestMethod]
        public void ComplexScenario_MultipleColumnsWithSortingAndSearching()
        {
            // Arrange
            var model = new DataTableRequestModel(3);

            // Act
            model.DisplayStart = 20;
            model.DisplayLength = 10;
            model.Search = "global search";
            model.EscapeRegex = true;
            model.SortingCols = 2;
            model.Echo = 5;
            
            // Configure columns
            model.ColumnNames.AddRange(new[] { "Name", "Email", "CreatedDate" });
            model.ListIsSortable.AddRange(new[] { true, false, true });
            model.ListIsSearchable.AddRange(new[] { true, true, false });
            model.SearchValues.AddRange(new[] { "john", "gmail", "" });
            model.SortCol.AddRange(new[] { 0, 2 });
            model.SortDir.AddRange(new[] { "asc", "desc" });
            model.ListIsEscapeRegexColumn.AddRange(new[] { true, false, true });
            
            // Add custom data
            model.Data = new Dictionary<string, object>
            {
                { "customFilter", "active" },
                { "dateRange", "2023-01-01,2023-12-31" }
            };

            // Assert
            Assert.AreEqual(20, model.DisplayStart);
            Assert.AreEqual(10, model.DisplayLength);
            Assert.AreEqual(3, model.Columns);
            Assert.AreEqual("global search", model.Search);
            Assert.AreEqual(true, model.EscapeRegex);
            Assert.AreEqual(2, model.SortingCols);
            Assert.AreEqual(5, model.Echo);
            
            Assert.AreEqual(3, model.ColumnNames.Count);
            Assert.AreEqual("Name", model.ColumnNames[0]);
            Assert.AreEqual("Email", model.ColumnNames[1]);
            Assert.AreEqual("CreatedDate", model.ColumnNames[2]);
            
            Assert.AreEqual(3, model.ListIsSortable.Count);
            Assert.AreEqual(true, model.ListIsSortable[0]);
            Assert.AreEqual(false, model.ListIsSortable[1]);
            Assert.AreEqual(true, model.ListIsSortable[2]);
            
            Assert.AreEqual(3, model.ListIsSearchable.Count);
            Assert.AreEqual(true, model.ListIsSearchable[0]);
            Assert.AreEqual(true, model.ListIsSearchable[1]);
            Assert.AreEqual(false, model.ListIsSearchable[2]);
            
            Assert.AreEqual(3, model.SearchValues.Count);
            Assert.AreEqual("john", model.SearchValues[0]);
            Assert.AreEqual("gmail", model.SearchValues[1]);
            Assert.AreEqual("", model.SearchValues[2]);
            
            Assert.AreEqual(2, model.SortCol.Count);
            Assert.AreEqual(0, model.SortCol[0]);
            Assert.AreEqual(2, model.SortCol[1]);
            
            Assert.AreEqual(2, model.SortDir.Count);
            Assert.AreEqual("asc", model.SortDir[0]);
            Assert.AreEqual("desc", model.SortDir[1]);
            
            Assert.AreEqual(3, model.ListIsEscapeRegexColumn.Count);
            Assert.AreEqual(true, model.ListIsEscapeRegexColumn[0]);
            Assert.AreEqual(false, model.ListIsEscapeRegexColumn[1]);
            Assert.AreEqual(true, model.ListIsEscapeRegexColumn[2]);
            
            Assert.AreEqual(2, model.Data.Count);
            Assert.AreEqual("active", model.Data["customFilter"]);
            Assert.AreEqual("2023-01-01,2023-12-31", model.Data["dateRange"]);
        }
    }
}
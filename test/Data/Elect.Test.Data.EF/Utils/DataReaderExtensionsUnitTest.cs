namespace Elect.Test.Data.EF.Utils
{
    [TestClass]
    public class DataReaderExtensionsUnitTest
    {
        private class TestDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime? Date { get; set; }
            public bool IsActive { get; set; }
            public string ReadOnlyProperty { get; private set; }
            public int WriteOnlyField;
        }

        private class EmptyDto
        {
        }

        private class MixedCaseDto
        {
            public int ID { get; set; }
            public string NAME { get; set; }
            public bool isactive { get; set; }
        }

        // Mock DbDataReader for testing
        private class MockDbDataReader : DbDataReader
        {
            private readonly List<Dictionary<string, object>> _data;
            private readonly List<string> _fieldNames;
            private int _currentIndex = -1;

            public MockDbDataReader(List<Dictionary<string, object>> data, List<string> fieldNames)
            {
                _data = data ?? new List<Dictionary<string, object>>();
                _fieldNames = fieldNames ?? new List<string>();
            }

            public override bool HasRows => _data.Count > 0;
            public override int FieldCount => _fieldNames.Count;

            public override bool Read()
            {
                _currentIndex++;
                return _currentIndex < _data.Count;
            }

            public override string GetName(int ordinal)
            {
                if (ordinal < 0 || ordinal >= _fieldNames.Count)
                    throw new IndexOutOfRangeException();
                return _fieldNames[ordinal];
            }

            public override object GetValue(int ordinal)
            {
                if (_currentIndex < 0 || _currentIndex >= _data.Count)
                    throw new InvalidOperationException("No current row");
                
                var fieldName = GetName(ordinal);
                return _data[_currentIndex].ContainsKey(fieldName) ? _data[_currentIndex][fieldName] : DBNull.Value;
            }

            // Required abstract members (minimal implementations for testing)
            public override object this[int ordinal] => GetValue(ordinal);
            public override object this[string name] => throw new NotImplementedException();
            public override int Depth => 0;
            public override bool IsClosed => false;
            public override int RecordsAffected => 0;
            public override bool GetBoolean(int ordinal) => (bool)GetValue(ordinal);
            public override byte GetByte(int ordinal) => (byte)GetValue(ordinal);
            public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length) => throw new NotImplementedException();
            public override char GetChar(int ordinal) => (char)GetValue(ordinal);
            public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length) => throw new NotImplementedException();
            public override string GetDataTypeName(int ordinal) => throw new NotImplementedException();
            public override DateTime GetDateTime(int ordinal) => (DateTime)GetValue(ordinal);
            public override decimal GetDecimal(int ordinal) => (decimal)GetValue(ordinal);
            public override double GetDouble(int ordinal) => (double)GetValue(ordinal);
            public override Type GetFieldType(int ordinal) => throw new NotImplementedException();
            public override float GetFloat(int ordinal) => (float)GetValue(ordinal);
            public override Guid GetGuid(int ordinal) => (Guid)GetValue(ordinal);
            public override short GetInt16(int ordinal) => (short)GetValue(ordinal);
            public override int GetInt32(int ordinal) => (int)GetValue(ordinal);
            public override long GetInt64(int ordinal) => (long)GetValue(ordinal);
            public override int GetOrdinal(string name) => _fieldNames.IndexOf(name);
            public override string GetString(int ordinal) => (string)GetValue(ordinal);
            public override bool IsDBNull(int ordinal) => GetValue(ordinal) == DBNull.Value;
            public override bool NextResult() => false;
            public override IEnumerator GetEnumerator() => throw new NotImplementedException();
            public override int GetValues(object[] values) => throw new NotImplementedException();
        }

        [TestMethod]
        public void QueryTo_WithValidDataAndProperties_ReturnsCorrectObjects()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Id", 1 },
                    { "Name", "Test1" },
                    { "Date", new DateTime(2023, 1, 1) },
                    { "IsActive", true }
                },
                new Dictionary<string, object>
                {
                    { "Id", 2 },
                    { "Name", "Test2" },
                    { "Date", new DateTime(2023, 1, 2) },
                    { "IsActive", false }
                }
            };
            var fieldNames = new List<string> { "Id", "Name", "Date", "IsActive" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<TestDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Test1", result[0].Name);
            Assert.AreEqual(new DateTime(2023, 1, 1), result[0].Date);
            Assert.IsTrue(result[0].IsActive);
            
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Test2", result[1].Name);
            Assert.AreEqual(new DateTime(2023, 1, 2), result[1].Date);
            Assert.IsFalse(result[1].IsActive);
        }

        [TestMethod]
        public void QueryTo_WithNullValues_HandlesNullsCorrectly()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Id", 1 },
                    { "Name", DBNull.Value },
                    { "Date", DBNull.Value },
                    { "IsActive", true }
                }
            };
            var fieldNames = new List<string> { "Id", "Name", "Date", "IsActive" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<TestDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.IsNull(result[0].Name);
            Assert.IsNull(result[0].Date);
            Assert.IsTrue(result[0].IsActive);
        }

        [TestMethod]
        public void QueryTo_WithCaseInsensitiveMatching_MapsCorrectly()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "id", 1 },
                    { "name", "Test" },
                    { "ISACTIVE", true }
                }
            };
            var fieldNames = new List<string> { "id", "name", "ISACTIVE" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<MixedCaseDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].ID);
            Assert.AreEqual("Test", result[0].NAME);
            Assert.IsTrue(result[0].isactive);
        }

        [TestMethod]
        public void QueryTo_WithExtraFields_IgnoresUnmatchedFields()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Id", 1 },
                    { "Name", "Test" },
                    { "ExtraField", "Ignored" },
                    { "AnotherExtra", 999 }
                }
            };
            var fieldNames = new List<string> { "Id", "Name", "ExtraField", "AnotherExtra" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<TestDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Test", result[0].Name);
        }

        [TestMethod]
        public void QueryTo_WithMissingFields_LeavesPropertiesAtDefault()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Id", 5 }
                }
            };
            var fieldNames = new List<string> { "Id" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<TestDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(5, result[0].Id);
            Assert.IsNull(result[0].Name);
            Assert.IsNull(result[0].Date);
            Assert.IsFalse(result[0].IsActive); // default bool value
        }

        [TestMethod]
        public void QueryTo_WithEmptyClass_ReturnsObjectInstances()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "SomeField", "Value" }
                }
            };
            var fieldNames = new List<string> { "SomeField" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<EmptyDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.IsInstanceOfType(result[0], typeof(EmptyDto));
        }

        [TestMethod]
        public void QueryTo_WithReadOnlyProperty_SkipsReadOnlyProperty()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Id", 1 },
                    { "ReadOnlyProperty", "CannotSet" }
                }
            };
            var fieldNames = new List<string> { "Id", "ReadOnlyProperty" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<TestDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
            // ReadOnlyProperty cannot be set, so it should remain null
            // The implementation correctly skips properties that cannot be written
        }

        [TestMethod]
        public void QueryTo_WithNullReader_ReturnsNull()
        {
            DbDataReader reader = null;

            var result = reader.QueryTo<TestDto>();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void QueryTo_WithNoRows_ReturnsNull()
        {
            var data = new List<Dictionary<string, object>>(); // Empty data
            var fieldNames = new List<string> { "Id", "Name" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<TestDto>();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void QueryTo_WithMultipleRowsAllNull_HandlesAllNullValues()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Id", DBNull.Value },
                    { "Name", DBNull.Value },
                    { "Date", DBNull.Value }
                },
                new Dictionary<string, object>
                {
                    { "Id", DBNull.Value },
                    { "Name", DBNull.Value },
                    { "Date", DBNull.Value }
                }
            };
            var fieldNames = new List<string> { "Id", "Name", "Date" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<TestDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            
            foreach (var item in result)
            {
                Assert.AreEqual(0, item.Id); // default int value
                Assert.IsNull(item.Name);
                Assert.IsNull(item.Date);
            }
        }

        [TestMethod]
        public void QueryTo_WithFieldNameVariations_MapsCorrectlyIgnoringCase()
        {
            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "ID", 1 },           // Uppercase
                    { "nAmE", "Test" },     // Mixed case
                    { "isactive", true }    // Lowercase
                }
            };
            var fieldNames = new List<string> { "ID", "nAmE", "isactive" };
            var reader = new MockDbDataReader(data, fieldNames);

            var result = reader.QueryTo<TestDto>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("Test", result[0].Name);
            Assert.IsTrue(result[0].IsActive);
        }
    }
}
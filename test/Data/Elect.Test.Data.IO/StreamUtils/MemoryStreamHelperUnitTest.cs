[TestClass]
public class MemoryStreamHelperUnitTest
{
    [TestMethod]
    public void Save_ValidStreamAndPath_FileIsCreatedWithCorrectContent()
    {
        // Arrange
        var content = new byte[] { 1, 2, 3, 4, 5 };
        using var stream = new MemoryStream(content);
        var tempPath = Path.GetTempFileName();

        try
        {
            // Act
            MemoryStreamHelper.Save(stream, tempPath);

            // Assert
            var fileContent = File.ReadAllBytes(tempPath);
            CollectionAssert.AreEqual(content, fileContent);
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Save_NullPath_ThrowsException()
    {
        using var stream = new MemoryStream();
        MemoryStreamHelper.Save(stream, null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Save_WhiteSpacePath_ThrowsException()
    {
        using var stream = new MemoryStream();
        MemoryStreamHelper.Save(stream, "   ");
    }

    [TestMethod]
    public void Save_WritesStreamToFile()
    {
        // Arrange
        var data = new byte[] { 1, 2, 3, 4, 5 };
        using var stream = new MemoryStream(data);
        var path = Path.GetTempFileName();

        try
        {
            // Act
            stream.Save(path);

            // Assert
            var fileData = File.ReadAllBytes(path);
            CollectionAssert.AreEqual(data, fileData);
        }
        finally
        {
            File.Delete(path);
        }
    }
}
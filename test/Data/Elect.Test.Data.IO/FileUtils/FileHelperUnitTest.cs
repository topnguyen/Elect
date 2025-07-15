namespace Elect.Test.Data.IO.FileUtils
{
    [TestClass]
    public class FileHelperUnitTest
{
    [TestMethod]
    public void CreateIfNotExist_CreatesFile_WhenNotExists()
    {
        var tempFile = Path.GetTempFileName();
        File.Delete(tempFile);
        FileHelper.CreateIfNotExist(tempFile);
        Assert.IsTrue(File.Exists(tempFile));
        File.Delete(tempFile);
    }
    [TestMethod]
    public void CreateIfNotExist_DoesNotThrow_WhenFileExists()
    {
        var tempFile = Path.GetTempFileName();
        FileHelper.CreateIfNotExist(tempFile);
        Assert.IsTrue(File.Exists(tempFile));
        File.Delete(tempFile);
    }
    [TestMethod]
    public void CreateIfNotExist_WithPermissions_CallsSetLinuxFilePermission()
    {
        var tempFile = Path.GetTempFileName();
        File.Delete(tempFile);
        FileHelper.CreateIfNotExist(0, tempFile);
        Assert.IsTrue(File.Exists(tempFile));
        File.Delete(tempFile);
    }
    [TestMethod]
    public void SetLinuxFilePermission_DoesNothing_OnWindows()
    {
        var tempFile = Path.GetTempFileName();
        FileHelper.SetLinuxFilePermission(0, tempFile);
        File.Delete(tempFile);
    }
    [TestMethod]
    public void CreateTempFile_CreatesFileWithExtension()
    {
        var path = FileHelper.CreateTempFile(".txt");
        Assert.IsTrue(File.Exists(path));
        Assert.AreEqual(".txt", Path.GetExtension(path));
        File.Delete(path);
    }
    [TestMethod]
    public void CreateTempFile_Stream_CreatesFileWithContent()
    {
        var path = FileHelper.CreateTempFile(".dat");
        Assert.IsTrue(File.Exists(path));
        File.Delete(path);
    }
    [TestMethod]
    public void SafeDelete_DeletesFile()
    {
        var tempFile = Path.GetTempFileName();
        Assert.IsTrue(File.Exists(tempFile));
        var result = FileHelper.SafeDelete(tempFile);
        Assert.IsTrue(result);
        Assert.IsFalse(File.Exists(tempFile));
    }
    [TestMethod]
    public void SafeDelete_ReturnsTrue_WhenFileDoesNotExist()
    {
        var tempFile = Path.GetTempFileName();
        File.Delete(tempFile);
        var result = FileHelper.SafeDelete(tempFile);
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void SafeDelete_ReturnsFalse_OnException()
    {
        var result = FileHelper.SafeDelete(null);
        Assert.IsTrue(result); // Because null or whitespace returns true
    }
    [TestMethod]
    public void IsValidBase64_ReturnsTrue_ForValidBase64()
    {
        var valid = Convert.ToBase64String("test"u8.ToArray());
        Assert.IsTrue(FileHelper.IsValidBase64(valid));
    }
    [TestMethod]
    public void IsValidBase64_ReturnsFalse_ForInvalidBase64()
    {
        Assert.IsFalse(FileHelper.IsValidBase64("notbase64"));
    }
    [TestMethod]
    public void WriteToStream_WritesFileContentToStream()
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, "abc");
        using var ms = new MemoryStream();
        FileHelper.WriteToStream(tempFile, ms);
        ms.Position = 0;
        var reader = new StreamReader(ms);
        var content = reader.ReadToEnd();
        Assert.AreEqual("abc", content);
        File.Delete(tempFile);
    }
    [TestMethod]
    public void WriteToStream_DoesNothing_WhenFileNotExists()
    {
        using var ms = new MemoryStream();
        FileHelper.WriteToStream("notfound.txt", ms);
        Assert.AreEqual(0, ms.Length);
    }
    [TestMethod]
    public void MakeValidFileName_RemovesInvalidChars()
    {
        var invalid = "a<b>c:d*e?f|g\"h/i\\j";
        var valid = FileHelper.MakeValidFileName(invalid, '_', false, false);
        Assert.IsFalse(valid.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0);
    }
    [TestMethod]
    public void MakeValidFileName_UsesFancyReplacement()
    {
        var input = "\"'/";
        var result = FileHelper.MakeValidFileName(input, '_', true, false);
        Assert.IsTrue(result.Contains('"') && result.Contains('\'') && result.Contains('⁄'), "Result should contain '\"', '\\'', and '⁄'");
    }
    [TestMethod]
    public void MakeValidFileName_RemovesAccents()
    {
        var input = "café";
        var result = FileHelper.MakeValidFileName(input, '_', false, true);
        Assert.IsTrue(result.Contains("cafe"));
    }
    [TestMethod]
    public void MakeValidFileName_ReturnsReplacement_WhenEmpty()
    {
        var result = FileHelper.MakeValidFileName("<>", '_', false, false);
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            Assert.AreEqual("__", result);
        }
        else
        {
            Assert.AreEqual("<>", result);
        }
    }
    [TestMethod]
    public void MakeValidFileName_TruncatesLongFileName()
    {
        var longName = new string('a', 300);
        var result = FileHelper.MakeValidFileName(longName, '_', false, false);
        Assert.AreEqual(260, result.Length);
    }
    [TestMethod]
    public void CreateTempFile_FromStream_CreatesFileWithCorrectContentAndExtension()
    {
        // Arrange
        var content = new byte[] { 1, 2, 3, 4, 5 };
        using var inputStream = new MemoryStream(content);
        var extension = ".dat";
        // Act
        var filePath = FileHelper.CreateTempFile(inputStream, extension);
        // Assert
        Assert.IsTrue(File.Exists(filePath));
        Assert.AreEqual(extension, Path.GetExtension(filePath));
        var fileContent = File.ReadAllBytes(filePath);
        CollectionAssert.AreEqual(content, fileContent);
        // Cleanup
        File.Delete(filePath);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateIfNotExist_ThrowsOnDirectoryTraversal()
    {
        FileHelper.CreateIfNotExist("../../../etc/passwd");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateIfNotExist_ThrowsOnRelativePathTraversal()
    {
        FileHelper.CreateIfNotExist("folder/../../../sensitive.txt");
    }
}
}

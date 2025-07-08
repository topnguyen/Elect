[TestClass]
public class DirectoryHelperUnitTest
{
    private string _tempDir;
    [TestInitialize]
    public void SetUp()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDir);
    }
    [TestCleanup]
    public void TearDown()
    {
        if (Directory.Exists(_tempDir))
            Directory.Delete(_tempDir, true);
    }
    [TestMethod]
    public void CreateIfNotExist_CreatesDirectory_WhenNotExists()
    {
        var newDir = Path.Combine(_tempDir, "newDir");
        DirectoryHelper.CreateIfNotExist(newDir);
        Assert.IsTrue(Directory.Exists(newDir));
    }
    [TestMethod]
    public void CreateIfNotExist_DoesNothing_WhenDirectoryExists()
    {
        var existingDir = Path.Combine(_tempDir, "existingDir");
        Directory.CreateDirectory(existingDir);
        DirectoryHelper.CreateIfNotExist(existingDir);
        Assert.IsTrue(Directory.Exists(existingDir));
    }
    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void CreateIfNotExist_Throws_OnNull()
    {
        DirectoryHelper.CreateIfNotExist(null);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CreateIfNotExist_Throws_OnWhiteSpace()
    {
        DirectoryHelper.CreateIfNotExist("  ");
    }
    [TestMethod]
    public void Empty_RemovesAllFilesAndDirectories()
    {
        var dir = Path.Combine(_tempDir, "toEmpty");
        Directory.CreateDirectory(dir);
        File.WriteAllText(Path.Combine(dir, "file.txt"), "test");
        var subDir = Path.Combine(dir, "sub");
        Directory.CreateDirectory(subDir);
        DirectoryHelper.Empty(dir);
        Assert.AreEqual(0, Directory.GetFiles(dir).Length);
        Assert.AreEqual(0, Directory.GetDirectories(dir).Length);
    }
    [TestMethod]
    public void IsEmpty_ReturnsTrue_WhenDirectoryIsEmpty()
    {
        var dir = Path.Combine(_tempDir, "emptyDir");
        Directory.CreateDirectory(dir);
        Assert.IsTrue(DirectoryHelper.IsEmpty(dir));
    }
    [TestMethod]
    public void IsEmpty_ReturnsFalse_WhenDirectoryHasFilesOrDirs()
    {
        var dir = Path.Combine(_tempDir, "notEmptyDir");
        Directory.CreateDirectory(dir);
        File.WriteAllText(Path.Combine(dir, "file.txt"), "test");
        Assert.IsFalse(DirectoryHelper.IsEmpty(dir));
    }
    [TestMethod]
    public void SpecialFolder_GetCurrentWindowUserFolder_ReturnsValidPath()
    {
        var path = DirectoryHelper.SpecialFolder.GetCurrentWindowUserFolder();
        Assert.IsFalse(string.IsNullOrWhiteSpace(path));
        Assert.IsTrue(Directory.Exists(path));
    }
}

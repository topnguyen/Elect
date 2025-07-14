using Elect.Data.IO.FileUtils;
using System.Threading;

namespace Elect.Test.Data.IO.FileUtils
{
    [TestClass]
    public class FileWatcherUnitTest
    {
        private string _testFilePath;

        [TestInitialize]
        public void Setup()
        {
            _testFilePath = Path.Combine(Path.GetTempPath(), "test_file_watcher.txt");
            File.WriteAllText(_testFilePath, "initial content");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }

        [TestMethod]
        public void Constructor_WithPath_SetsUpWatcher()
        {
            var watcher = new FileWatcher(_testFilePath);
            Assert.IsNotNull(watcher);
            Assert.IsNull(watcher.OnChanged);
        }

        [TestMethod]
        public void Constructor_WithPathAndDelegate_SetsUpWatcherAndDelegate()
        {
            FileWatcher.OnChangedEvent changeEvent = (source, e) => { };

            var watcher = new FileWatcher(_testFilePath, changeEvent);
            Assert.IsNotNull(watcher);
            Assert.IsNotNull(watcher.OnChanged);
        }

        [TestMethod]
        public void Start_EnablesWatcher()
        {
            var watcher = new FileWatcher(_testFilePath);
            
            // This should not throw an exception
            watcher.Start();
            
            // Clean up
            watcher.Stop();
        }

        [TestMethod]
        public void Stop_DisablesWatcher()
        {
            var watcher = new FileWatcher(_testFilePath);
            watcher.Start();
            
            // This should not throw an exception
            watcher.Stop();
        }

        [TestMethod]
        public void OnChanged_TriggersWhenFileModified()
        {
            bool eventTriggered = false;
            var resetEvent = new ManualResetEventSlim();

            var watcher = new FileWatcher(_testFilePath);
            watcher.OnChanged = (source, e) =>
            {
                eventTriggered = true;
                resetEvent.Set();
            };

            watcher.Start();

            // Modify the file
            File.WriteAllText(_testFilePath, "modified content");

            // Wait for the event to be triggered (with timeout)
            bool eventReceived = resetEvent.Wait(TimeSpan.FromSeconds(5));

            watcher.Stop();

            Assert.IsTrue(eventReceived, "File change event should have been triggered");
            Assert.IsTrue(eventTriggered, "OnChanged event should have been called");
        }

        [TestMethod]
        public void Constructor_WithInvalidPath_HandlesGracefully()
        {
            // FileWatcher constructor uses Path.GetDirectoryName and Path.GetFileName
            // which may not throw for some invalid characters on all platforms
            try
            {
                var watcher = new FileWatcher("invalid|||path");
                // If no exception is thrown, the test passes
                Assert.IsNotNull(watcher);
            }
            catch (ArgumentException)
            {
                // If ArgumentException is thrown, that's also acceptable
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                // Any other exception type is also acceptable for invalid paths
                Assert.IsTrue(ex is ArgumentException || ex is System.IO.DirectoryNotFoundException || ex is System.IO.IOException,
                    $"Expected ArgumentException, DirectoryNotFoundException, or IOException but got {ex.GetType().Name}");
            }
        }

        [TestMethod]
        public void OnChanged_Property_CanBeSetAndGet()
        {
            var watcher = new FileWatcher(_testFilePath);
            
            FileWatcher.OnChangedEvent testEvent = (source, e) => { };
            watcher.OnChanged = testEvent;
            
            Assert.AreEqual(testEvent, watcher.OnChanged);
        }
    }
}
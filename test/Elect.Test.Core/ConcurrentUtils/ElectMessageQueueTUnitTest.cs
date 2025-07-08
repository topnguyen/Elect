namespace Elect.Test.Core.ConcurrentUtils
{
    [TestClass]
    public class ElectMessageQueueTUnitTest
    {
        private class TestQueue : ElectMessageQueue<string>
        {
            public List<IEnumerable<string>> ExecutedBatches { get; } = new List<IEnumerable<string>>();
            public TestQueue(uint batchSize = 2, int thresholdSeconds = 1) : base(batchSize, TimeSpan.FromSeconds(thresholdSeconds)) { }
            protected override void Execute(IEnumerable<string> events)
            {
                ExecutedBatches.Add(events.ToList());
            }
            public void PushPublic(string evt) => Push(evt);
            public void DisposePublic() => Dispose();
        }
        [TestMethod]
        public void Push_ShouldTriggerExecute_WhenBatchSizeReached()
        {
            var queue = new TestQueue(batchSize: 2);
            queue.PushPublic("a");
            queue.PushPublic("b");
            Thread.Sleep(500); // Allow background tasks to process
            queue.DisposePublic();
            Assert.IsTrue(queue.ExecutedBatches.Any(batch => batch.Contains("a") && batch.Contains("b")));
        }
        [TestMethod]
        public void Dispose_ShouldFlushRemainingEvents()
        {
            var queue = new TestQueue(batchSize: 10);
            queue.PushPublic("x");
            queue.PushPublic("y");
            queue.DisposePublic();
            // Wait up to 1 second for the batch to be executed
            var sw = System.Diagnostics.Stopwatch.StartNew();
            bool found = false;
            while (sw.ElapsedMilliseconds < 1000)
            {
                if (queue.ExecutedBatches.Any(batch => batch.Contains("x") && batch.Contains("y")))
                {
                    found = true;
                    break;
                }
                Thread.Sleep(10);
            }
            Assert.IsTrue(found, "Dispose did not flush remaining events in time.");
        }
    }
}

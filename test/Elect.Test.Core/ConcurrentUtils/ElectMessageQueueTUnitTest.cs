namespace Elect.Test.Core.ConcurrentUtils
{
    [TestClass]
    public class ElectMessageQueueTUnitTest
    {
        private class TestQueue : ElectMessageQueue<string>
        {
            public List<IEnumerable<string>> ExecutedBatches { get; } = new List<IEnumerable<string>>();
            private readonly System.Threading.ManualResetEventSlim _batchExecutedEvent = new System.Threading.ManualResetEventSlim(false);
            public TestQueue(uint batchSize = 2, int thresholdSeconds = 1) : base(batchSize, TimeSpan.FromSeconds(thresholdSeconds)) { }
            protected override void Execute(IEnumerable<string> events)
            {
                ExecutedBatches.Add(events.ToList());
                _batchExecutedEvent.Set();
            }
            public void PushPublic(string evt) => Push(evt);
            public void DisposePublic() => Dispose();
            public void WaitForBatch(int timeoutMs = 1000) => _batchExecutedEvent.Wait(timeoutMs);
            public void ResetBatchEvent() => _batchExecutedEvent.Reset();
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
            // Wait for all batches to be executed, up to 3 seconds
            var sw = System.Diagnostics.Stopwatch.StartNew();
            bool found = false;
            while (sw.ElapsedMilliseconds < 3000)
            {
                if (queue.ExecutedBatches.Any(batch => batch.Contains("x") && batch.Contains("y")))
                {
                    found = true;
                    break;
                }
                queue.WaitForBatch(100); // Wait for up to 100ms for a batch to be executed
                queue.ResetBatchEvent();
            }
            Assert.IsTrue(found, "Dispose did not flush remaining events in time.");
        }
    }
}

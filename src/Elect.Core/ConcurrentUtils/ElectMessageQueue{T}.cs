namespace Elect.Core.ConcurrentUtils
{
    /// <inheritdoc />
    /// <summary>
    ///     Override <see cref="M:Elect.Core.ConcurrentUtils.Models.ElectMessageQueue`1.Execute(System.Collections.Generic.ICollection{`0})" /> to write your job
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ElectMessageQueue<T> : ElectDisposableModel where T : class
    {
        #region Fields
        private bool _canStop;
        protected uint BatchSize = 50;
        protected TimeSpan Threshold = TimeSpan.FromSeconds(2);
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        private readonly CancellationTokenSource _eventCancellationToken = new CancellationTokenSource();
        private readonly Task _batchTask;
        private readonly Task _timerTask;
        private readonly Task _pumpTask;
        private readonly List<Task> _workerTasks = new List<Task>();
        private readonly AutoResetEvent _timerResetEvent = new AutoResetEvent(false);
        private readonly BlockingCollection<IList<T>> _batchCollection = new BlockingCollection<IList<T>>();
        private readonly BlockingCollection<T> _eventCollection = new BlockingCollection<T>();
        private readonly ConcurrentQueue<T> _batch = new ConcurrentQueue<T>();
        #endregion
        protected ElectMessageQueue()
        {
            _batchTask = Task.Factory.StartNew(Pump, TaskCreationOptions.LongRunning);
            _timerTask = Task.Factory.StartNew(TimerPump, TaskCreationOptions.LongRunning);
            _pumpTask = Task.Factory.StartNew(EventPump, TaskCreationOptions.LongRunning);
        }
        protected ElectMessageQueue(uint batchSize, TimeSpan threshold)
        {
            BatchSize = batchSize;
            Threshold = threshold;
            _batchTask = Task.Factory.StartNew(Pump, TaskCreationOptions.LongRunning);
            _timerTask = Task.Factory.StartNew(TimerPump, TaskCreationOptions.LongRunning);
            _pumpTask = Task.Factory.StartNew(EventPump, TaskCreationOptions.LongRunning);
        }
        protected void Push(T @event)
        {
            _eventCollection.Add(@event);
        }
        protected abstract void Execute(IEnumerable<T> events);
        #region Helper
        private void Pump()
        {
            try
            {
                while (true)
                {
                    var @events = _batchCollection.Take(_cancellationToken.Token);
                    var task = Task.Factory.StartNew(x => { Execute(x as IEnumerable<T>); }, @events);
                    _workerTasks.Add(task);
                    if (_workerTasks.Count <= BatchSize)
                    {
                        continue;
                    }
                    Task.WaitAll(_workerTasks.ToArray(), _cancellationToken.Token);
                    _workerTasks.Clear();
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Shutting down batch processing");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void TimerPump()
        {
            while (!_canStop)
            {
                _timerResetEvent.WaitOne(Threshold);
                Flush();
            }
        }
        private void EventPump()
        {
            try
            {
                while (true)
                {
                    var @event = _eventCollection.Take(_eventCancellationToken.Token);
                    _batch.Enqueue(@event);
                    if (_batch.Count >= BatchSize)
                    {
                        Flush();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Shutting down event pump");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void Flush()
        {
            var collection = new List<T>();
            
            // Use a snapshot of the count to avoid race conditions
            var currentCount = _batch.Count;
            if (currentCount == 0)
            {
                return;
            }
            
            var batchSize = currentCount >= BatchSize ? (int) BatchSize : currentCount;
            
            for (var i = 0; i < batchSize; i++)
            {
                if (_batch.TryDequeue(out var @event))
                {
                    collection.Add(@event);
                }
                else
                {
                    // If we can't dequeue, break to prevent infinite loop
                    break;
                }
            }
            
            if (collection.Count > 0)
            {
                _batchCollection.Add(collection);
            }
        }
        #endregion
        #region IDisposable
        protected override void DisposeUnmanagedResources()
        {
            FlushAndCloseEventHandlers();
        }
        private void FlushAndCloseEventHandlers()
        {
            try
            {
                Console.WriteLine("Flush and Shutdown Message Queue halting...");
                _eventCancellationToken.Cancel();
                _cancellationToken.Cancel();
                Task.WaitAll(_workerTasks.ToArray());
                _canStop = true;
                _timerResetEvent.Set();
                // Flush events collection
                while (_eventCollection.TryTake(out var @event))
                {
                    _batch.Enqueue(@event);
                    if (_batch.Count >= BatchSize)
                    {
                        Flush();
                    }
                }
                // Always flush any remaining events, even if batch is not full
                Flush();
                // Flush events batch
                while (_batchCollection.TryTake(out var @events))
                {
                    Execute(@events);
                }
                // Wait for all background tasks to finish
                Task.WhenAll(_pumpTask, _batchTask, _timerTask).Wait(TimeSpan.FromSeconds(30));
                // Ensure all worker tasks are completed after final flush
                if (_workerTasks.Count > 0)
                {
                    Task.WaitAll(_workerTasks.ToArray());
                    _workerTasks.Clear();
                }
                Console.WriteLine("Flush and Shutdown Message Queue successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion IDisposable Support
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elect.Core.ConcurrentUtils.Models
{
    public abstract class ElectMessageQueue<T> : IDisposable where T : class, new()
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

        protected abstract void Write(ICollection<T> events);

        #region Helper
        
        private void Pump()
        {
            try
            {
                while (true)
                {
                    var @events = _batchCollection.Take(_cancellationToken.Token);

                    var task = Task.Factory.StartNew(x => { Write(x as IList<T>); }, @events);

                    _workerTasks.Add(task);

                    if (_workerTasks.Count <= 32)
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
            if (!_batch.Any())
            {
                return;
            }

            var batchSize = _batch.Count >= BatchSize ? (int) BatchSize : _batch.Count;

            var collection = new List<T>();

            for (var i = 0; i < batchSize; i++)
            {
                if (_batch.TryDequeue(out var @event))
                {
                    collection.Add(@event);
                }
            }

            _batchCollection.Add(collection);
        }

        #endregion

        #region IDisposable

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }

            if (disposing)
            {
                FlushAndCloseEventHandlers();

                Console.WriteLine("Sink halted successfully.");
            }

            _disposedValue = true;
        }

        private void FlushAndCloseEventHandlers()
        {
            try
            {
                Console.WriteLine("Halting sink...");

                _eventCancellationToken.Cancel();

                _cancellationToken.Cancel();

                Task.WaitAll(_workerTasks.ToArray());

                _canStop = true;

                _timerResetEvent.Set();

                // Flush events collection
                while (_eventCollection.TryTake(out T @event))
                {
                    _batch.Enqueue(@event);

                    if (_batch.Count >= BatchSize)
                    {
                        Flush();
                    }
                }

                Flush();

                // Flush events batch
                while (_batchCollection.TryTake(out IList<T> @events))
                {
                    Write(@events);
                }

                Task.WaitAll(new[] {_pumpTask, _batchTask, _timerTask}, TimeSpan.FromSeconds(30));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}
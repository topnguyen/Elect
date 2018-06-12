using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elect.Logger
{
    public abstract class ElectMessageQueue<T> : IDisposable where T : class, new()
    {
        #region Fields

        private readonly uint _batchSize;
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        private readonly CancellationTokenSource _eventCancellationToken = new CancellationTokenSource();

        private readonly Task _batchTask;
        private readonly Task _timerTask;
        private readonly Task _pumpTask;
        private readonly List<Task> _workerTasks = new List<Task>();

        private readonly TimeSpan _thresholdTimeSpan;

        private readonly AutoResetEvent _timerResetEvent = new AutoResetEvent(false);

        private readonly BlockingCollection<IList<T>> _batchCollection;
        private readonly BlockingCollection<T> _eventCollection;
        private readonly ConcurrentQueue<T> _batch;

        private bool _canStop;

        #endregion

        protected ElectMessageQueue(uint batchSize = 100, int thresholdSec = 5)
        {
            _batchSize = batchSize > 0 ? batchSize : 100;

            _thresholdTimeSpan = thresholdSec > 0
                ? TimeSpan.FromSeconds(thresholdSec)
                : TimeSpan.FromSeconds(5);

            _batch = new ConcurrentQueue<T>();

            _batchCollection = new BlockingCollection<IList<T>>();

            _eventCollection = new BlockingCollection<T>();

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
                _timerResetEvent.WaitOne(_thresholdTimeSpan);
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

                    if (_batch.Count >= _batchSize)
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

            var batchSize = _batch.Count >= _batchSize ? (int) _batchSize : _batch.Count;

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

                    if (_batch.Count >= _batchSize)
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
using System;
using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace SnipeSharp.PowerShell
{
    internal sealed class AsyncContext: SynchronizationContext, IDisposable
    {
        private BlockingCollection<(SendOrPostCallback callback, object? state)>? Queue = new();

        void IDisposable.Dispose()
        {
            if(null != Queue)
            {
                Queue.Dispose();
                Queue = null;
            }
        }

        private void BeginPump()
        {
            if(null == Queue)
                throw new ObjectDisposedException(nameof(Queue));
            while(null != Queue && Queue.TryTake(out var pair, Timeout.InfiniteTimeSpan))
                pair.callback(pair.state);
        }

        private void EndPump()
        {
            if(null == Queue)
                throw new ObjectDisposedException(nameof(Queue));
            Queue.CompleteAdding();
        }

        public override void Post(SendOrPostCallback callback, object? state)
        {
            if(null == callback)
                throw new ArgumentNullException(nameof(callback));
            if(null == Queue)
                throw new ObjectDisposedException(nameof(Queue));
            Queue.Add((callback, state));
        }

        public static void RunSync(Task task)
        {
            var savedContext = Current;
            try
            {
                using(var context = new AsyncContext())
                {
                    SetSynchronizationContext(context);
                    task.ContinueWith(a => context.EndPump(), TaskScheduler.Default);
                    try
                    {
                        task.GetAwaiter().GetResult();
                    } catch(AggregateException e)
                    {
                        var flat = e.Flatten();
                        if(e.InnerExceptions.Count == 1)
                            ExceptionDispatchInfo.Capture(flat.InnerExceptions[0]).Throw();
                        throw;
                    }
                }
            } finally
            {
                SetSynchronizationContext(savedContext);
            }
        }
    }
}

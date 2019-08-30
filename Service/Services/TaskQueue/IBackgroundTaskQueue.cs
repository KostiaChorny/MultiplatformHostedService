
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Services.TaskQueue
{
    public interface IBackgroundTaskQueue
    {
        int Size { get; }

        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}

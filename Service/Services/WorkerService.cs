using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Models;
using Service.Services.TaskQueue;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Services
{
    public class WorkerService : BackgroundService
    {
        private readonly IBackgroundTaskQueue taskQueue;
        private readonly ILogger<WorkerService> logger;
        private readonly Settings settings;

        public WorkerService(IBackgroundTaskQueue taskQueue, ILogger<WorkerService> logger, Settings settings)
        {
            this.taskQueue = taskQueue;
            this.logger = logger;
            this.settings = settings;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            var workersCount = settings.WorkersCount;
            var workers = Enumerable.Range(0, workersCount).Select(num => RunInstance(num, token));

            await Task.WhenAll(workers);

        }

        private async Task RunInstance(int num, CancellationToken token)
        {
            logger.LogInformation($"#{num} is starting.");

            while(!token.IsCancellationRequested)
            {
                var workItem = await taskQueue.DequeueAsync(token);

                try
                {
                    logger.LogInformation($"#{num}: Processing task. Queue size: {taskQueue.Size}.");
                    await workItem(token);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"#{num}: Error occurred executing task.");
                }
            }

            logger.LogInformation($"#{num} is stopping.");
        }
    }
}

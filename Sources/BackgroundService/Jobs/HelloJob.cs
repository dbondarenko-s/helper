using Quartz;

namespace BackgroundService.Jobs
{
    [DisallowConcurrentExecution]
    public class HelloJob : IJob
    {
        private readonly ILogger _logger;

        public HelloJob(ILogger<HelloJob> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("HelloJob is executing.");
        }
    }
}

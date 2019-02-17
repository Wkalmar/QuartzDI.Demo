using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Quartz;
using QuartzDi.Demo.Extrernal;

namespace QuartzDI.Demo
{
    public class DemoJob : IJob
    {
        private readonly IDemoService _demoService;
        private readonly DemoJobOptions _options;

        public DemoJob(IDemoService demoService, IOptions<DemoJobOptions> options)
        {
            _demoService = demoService;
            _options = options.Value;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _demoService.DoTask(_options.Url);
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Quartz;
using QuartzDi.Demo.Extrernal;

namespace QuartzDI.Demo
{
    public class DemoJob : IJob
    {
        public static string Url { get; set; }

        private readonly IDemoService _demoService;

        public DemoJob(IDemoService demoService)
        {
            _demoService = demoService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _demoService.DoTask(Url);
            return Task.CompletedTask;
        }
    }
}
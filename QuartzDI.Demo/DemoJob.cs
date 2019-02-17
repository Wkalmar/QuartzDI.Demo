using System.Threading.Tasks;
using Quartz;
using QuartzDi.Demo.Extrernal;

namespace QuartzDI.Demo
{
    public class DemoJob : IJob
    {
        private const string Url = "i.ua";

        public static IDemoService DemoService { get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            DemoService.DoTask(Url);
            return Task.CompletedTask;
        }
    }
}
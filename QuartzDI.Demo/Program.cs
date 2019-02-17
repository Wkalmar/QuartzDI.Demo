using Quartz;
using Quartz.Impl;
using QuartzDi.Demo.Extrernal;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace QuartzDI.Demo
{
    class Program
    {
        async static Task Main(string[] args)
        {
            DemoJob.DemoService = new DemoService();
            await ScheduleJob();
            Console.ReadLine();
        }

        private static async Task ScheduleJob()
        {
            var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            var factory = new StdSchedulerFactory(props);
            var sched = await factory.GetScheduler();
            await sched.Start();
            var job = JobBuilder.Create<DemoJob>()
                .WithIdentity("myJob", "group1")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
            .Build();
            await sched.ScheduleJob(job, trigger);
        }
    }    
}

using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using QuartzDi.Demo.Extrernal;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;

namespace QuartzDI.Demo
{
    class Program
    {
        async static Task Main(string[] args)
        {
            IConfigurationSection connectionSection = GetConnectionSection();

            DemoJob.DemoService = new DemoService();
            DemoJob.Url = connectionSection["Url"];
            await ScheduleJob();
            Console.ReadLine();
        }

        private static IConfigurationSection GetConnectionSection()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", true, true);
            var configuration = builder.Build();
            var connectionSection = configuration.GetSection("connection");
            return connectionSection;
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

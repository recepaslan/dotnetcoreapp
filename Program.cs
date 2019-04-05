using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace dotnetcoreapp
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgramRunExample().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }

        private static async Task RunProgramRunExample()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" },
                    { "quartz.scheduler.instanceName", "mytest"},
                    { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz"},
                    { "quartz.threadPool.threadCount", "3"},
                    { "quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz"},
                    { "quartz.jobStore.tablePrefix", "QRTZ_"},
                    { "quartz.jobStore.dataSource","myDS"},
                    { "quartz.dataSource.myDS.provider", "SqlServer"},
                    { "quartz.dataSource.myDS.connectionString", ""},
                    { "quartz.jobStore.useProperties","true"},
                    { "quartz.jobStore.clustered", "true"},
                    { "quartz.jobStore.lockHandler.type", "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();
                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                if (await scheduler.CheckExists(job.Key))
                {
                    await scheduler.ResumeJob(job.Key);
                }
                else
                {
                    // Trigger the job to run now, and then repeat every 10 seconds
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity("trigger1", "group1")
                        .WithCronSchedule("* * * * * ?")
                        .Build();

                    // Tell quartz to schedule the job using our trigger
                    await scheduler.ScheduleJob(job, trigger);
                }
                // some sleep to show what's happening
                await Task.Delay(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }
    }
}

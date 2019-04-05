using System;
using System.Threading.Tasks;
using Quartz;

public class HelloJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        
        Console.Out.WriteLineAsync($"{DateTime.Now.ToLongTimeString()}");
       return Task.FromResult(0);
    }
}
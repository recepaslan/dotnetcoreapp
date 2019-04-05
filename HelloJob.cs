using System;
using System.Threading.Tasks;
using Quartz;

public class HelloJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        string clientUUId = ""; //fetch from database;
        byte[] clientIdBytes = System.Text.Encoding.UTF8.GetBytes(clientUUId);
        string clientId = Convert.ToBase64String(clientIdBytes);
        Console.Out.WriteLineAsync($"{DateTime.Now.ToLongTimeString()}");
        return Task.FromResult(0);
    }
}
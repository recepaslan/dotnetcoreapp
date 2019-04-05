
using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

public class mylistener : ITriggerListener
{
    public string Name {get;}

    public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
    {
            Console.WriteLine(trigger.JobKey);
           return Task.FromResult(0); 
    }

    public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
           Console.WriteLine(trigger.JobKey);
           return Task.FromResult(0);
    }

    public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
    {
          Console.WriteLine(trigger.JobKey);
           return Task.FromResult(0);
    }

    public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
          Console.WriteLine(trigger.JobKey);
             return Task.FromResult(true);
    }
}
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Repo;
using NCrontab;


public class JobScheduler : BackgroundService {
	private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);
	private readonly ILogger<JobScheduler> _logger;
	private readonly IJobRepo _jobRepo;
	private readonly IJobFactory _jobFactory;

	public JobScheduler(ILogger<JobScheduler> logger, IJobRepo jobRepo, IJobFactory jobFactory) {
		_logger = logger;
		_jobRepo = jobRepo;
		_jobFactory = jobFactory;
	}


	protected override async Task ExecuteAsync(CancellationToken token) {
		while(!token.IsCancellationRequested) {
			await Task.Delay(_interval, token);
			var jobs = _jobRepo.GetJobs();
			
			var job = jobs.Last();

			if(job.NextRun <= DateTime.Now) {
				
				var jobInstance = _jobFactory.CreateJob(job);
				jobInstance.Execute();

				_logger.LogInformation($"{job.NextRun} {DateTime.Now}");

				var schedule = CrontabSchedule.Parse(job.CronExpression);
				
				var nextRun = schedule.GetNextOccurrence(DateTime.Now);
				
				job.NextRun = nextRun;
				
				_jobRepo.AddJob(job);
			} else {
				_logger.LogInformation("No Job Found");
			}
		}
	}
}

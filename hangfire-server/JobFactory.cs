using Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class JobFactory : IJobFactory {
	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger _logger;
 
	public JobFactory(IServiceProvider serviceProvider, ILogger logger) {
		_serviceProvider = serviceProvider;
		_logger = logger;
	}

	public IJob CreateJob(Job job) {
		return new CsvToLedger(_logger);
	}
}

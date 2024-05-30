using Microsoft.Extensions.Logging;

public class CsvToLedger : IJob {
	
	private readonly ILogger _logger;

	public CsvToLedger(ILogger logger) {
		_logger = logger;
	}

	public void Execute() {
		_logger.LogInformation("CSV to Ledger");
	}
}

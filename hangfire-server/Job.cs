namespace Models {
	public class Job {
		public int JobId { get; set; }
		public string Name { get; set; }
		public string CronExpression { get; set; }
		public DateTime NextRun { get; set; }
		public string Type { get;set; }
	}
}

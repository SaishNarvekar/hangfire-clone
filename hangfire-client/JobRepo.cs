public class JobRepo : IJobRepo {

	private readonly JobContext _context;
	public JobRepo(JobContext context) {
		_context = context;
	}

	public void AddJob(Job job) {
		_context.Jobs.Add(job);
		_context.SaveChanges();
	}

	public List<Job> GetJobs() {
		return _context.Jobs.ToList();
	}
}

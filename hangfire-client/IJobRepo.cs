public interface IJobRepo {

	void AddJob(Job job);
	List<Job> GetJobs();
}

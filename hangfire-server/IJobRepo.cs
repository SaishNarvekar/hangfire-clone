using Models;

namespace Repo {

	public interface IJobRepo {

		void AddJob(Job job);
		List<Job> GetJobs();
	}

}

using Models;
using System.Text.Json;

namespace Repo {

	public class JSONJobRepo : IJobRepo {
		
		private string dbLocation = @"D:/projects/hangfire-clone/hangfire-db";

		public void AddJob(Job job) {

			var filePath = $"{dbLocation}/{DateTime.Now.ToString("yyyy_MM_dd")}.json";

			List<Job> jobList = new List<Job>();

			if(File.Exists(filePath)) {

				string jsonJobs = File.ReadAllText(filePath);
				jobList = JsonSerializer.Deserialize<List<Job>>(jsonJobs);
			}
			else {
				var fs = File.Create(filePath);
				fs.Close();
			}

			jobList.Add(job);

			string jsonJobList = JsonSerializer.Serialize<List<Job>>(jobList);

			File.WriteAllText(filePath, jsonJobList);
		}

		public List<Job> GetJobs() {
			var filePath = $"{dbLocation}/{DateTime.Now.ToString("yyyy_MM_dd")}.json";

			string jsonJobs = File.ReadAllText(filePath);

			return JsonSerializer.Deserialize<List<Job>>(jsonJobs);
		}
	}
}

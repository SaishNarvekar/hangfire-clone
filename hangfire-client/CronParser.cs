namespace CronExpressions {

	public static class Parser {

		private static Dictionary<string, int> Days = new Dictionary<string, int>() {
			{"Sunday", 0},
			{"Monday", 1},
			{"Tuesday", 2},
			{"Wednesday", 3},
			{"Thursday", 4},
			{"Friday", 5},
			{"Saturday", 6}
		};

		public static string ToCron(this string input) {
			string[] exp = input.Split(" ");

			if(exp[0] == "Daily") {
				return $"0 {exp[1].ConvertTime()} * * *";
			} else if(exp[0] == "Weekly") {
				return $"0 {exp[2].ConvertTime()} * * {exp[1].ConvertDay()}";
			} else if(exp[0] == "Monthly") {
				return $"0 {exp[2].ConvertTime()} {exp[1]} * *";
			}

			return $"0 {exp[1].ConvertTime()} * * *";
		}

		public static string ConvertTime(this string input) {
			return input.Split(":")[0];
		}

		public static int ConvertDay(this string input) {			
			return Days[input];
		}
	}
}

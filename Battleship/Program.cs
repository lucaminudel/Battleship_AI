namespace Battleship
{
	using System;
	using System.Drawing;
	using System.Linq;

	class Program
	{
		static void DisplayUsage(string message)
		{
			Console.WriteLine("ERROR: " + message);
			Console.WriteLine();
			Console.WriteLine("Usage:");
			Console.WriteLine("{0} OPPONENT OPPONENT", Environment.GetCommandLineArgs()[0]);
			Console.WriteLine("Where OPPONENT can be:");
			Console.WriteLine("- an assembly containing an opponent implementation, i.e. RandomOpponent.dll");
			Console.WriteLine("- an executable with an out of process implementation, i.e. Random.bat");
			Console.WriteLine("- the full name of the type of a an available opponent, i.e. Battleship.Opponents.AgentSmith");
		}

		static void Main(string[] args)
		{
			IBattleshipOpponent[] opponents;
			try
			{
				var loader = new OpponentLoader();
				opponents = loader.LoadFrom(args);
			}
			catch (Exception e)
			{
				DisplayUsage(e.Message);
				return;
			}

			BattleshipCompetition bc = new BattleshipCompetition(
				opponents[0],
				opponents[1],
				new TimeSpan(0, 0, 4),  // Time per game
				501,                    // Wins per match
				true,                   // Play out?
				new Size(10, 10),       // Board Size
				2, 3, 3, 4, 5           // Ship Sizes
			);

			Console.WriteLine("Battleship AI");
			Console.WriteLine("{0} v{1} vs. {2} v{3}", 
				opponents[0].Name, opponents[0].Version,
				opponents[1].Name, opponents[1].Version
			);

			var scores = bc.RunCompetition();

			var lengthOfLongestName = scores.Keys
				.Select(k => string.Format("{0} v{1}", k.Name, k.Version))
				.Max(n => n.Length);
			var scoreFormat = string.Format("{{0,-{0}}}: {{1}}", lengthOfLongestName);

			foreach (var key in scores.Keys.OrderByDescending(k => scores[k]))
			{
				Console.WriteLine(scoreFormat, string.Format("{0} v{1}", key.Name, key.Version), scores[key]);
			}
			Console.WriteLine("Press any key to quit.");
			Console.ReadKey(true);
		}
	}
}

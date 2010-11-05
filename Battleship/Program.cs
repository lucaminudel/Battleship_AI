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
			Console.WriteLine("Where OPPONENT is an assembly containing an opponent implementation.");
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

			var scores = bc.RunCompetition();

			foreach (var key in scores.Keys.OrderByDescending(k => scores[k]))
			{
				Console.WriteLine("{0} {1}:\t{2}", key.Name, key.Version, scores[key]);
			}
			Console.WriteLine("Press any key to quit.");
			Console.ReadKey(true);
		}
	}
}

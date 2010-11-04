
namespace Battleship
{
    using System;
    using System.Drawing;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
			var op1 = new Opponents.RandomOpponent();
        	var op2 = new Opponents.RandomOpponent();

            BattleshipCompetition bc = new BattleshipCompetition(
                op1,
                op2,
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

            Console.ReadKey(true);
        }
    }
}

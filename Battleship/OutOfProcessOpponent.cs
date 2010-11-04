namespace Battleship
{
	using System;
	using System.Collections.ObjectModel;
	using System.Drawing;

	public class OutOfProcessOpponent : IBattleshipOpponent
	{
		public OutOfProcessOpponent(IHaveStandardIO io)
		{
			this.io = io;
		}

		public string Name { get { io.WriteLine("get-name"); return io.ReadLine(); } }

		public Version Version { get { io.WriteLine("get-version"); return new Version(io.ReadLine()); } }

		public void NewGame(Size size, TimeSpan timeSpan)
		{
			io.WriteLine("new-game");
		}

		public void PlaceShips(ReadOnlyCollection<Ship> ships)
		{
			foreach (var ship in ships)
			{
				io.WriteLine("place-ship {0}", ship.Length);
				var placement = io.ReadLine().Split(' ');
				var location = new Point(int.Parse(placement[0]), int.Parse(placement[1]));
				var orientation = (ShipOrientation)Enum.Parse(typeof(ShipOrientation), placement[2], true);
				ship.Place(location, orientation);
			}
		}

		public Point GetShot()
		{
			io.WriteLine("get-shot");
			var placement = io.ReadLine().Split(' ');
			return new Point(int.Parse(placement[0]), int.Parse(placement[1]));
		}

		public void ShotHit(Point shot)
		{
			io.WriteLine("shot-hit");
		}

		public void ShotHitAndSink(Point shot, Ship sunkShip)
		{
			io.WriteLine("shot-hit-and-sink {0} {1} {2} {3}", 
				sunkShip.Length, 
				sunkShip.Location.X, sunkShip.Location.Y, 
				sunkShip.Orientation.ToString().ToLower()
			);
		}

		public void ShotMiss(Point shot)
		{
			io.WriteLine("shot-hit-miss");
		}

		public void OpponentShot(Point shot)
		{
			io.WriteLine("opponent-shot {0} {1}", shot.X, shot.Y);
		}

		public void GameWon()
		{
			io.WriteLine("game-won");
		}

		public void GameLost()
		{
			io.WriteLine("game-lost");
		}

		public void NewMatch(string opponent) { }
		public void MatchOver() { io.WriteLine("match-over"); }

		private IHaveStandardIO io;
	}
}

using System;
using System.Linq;
using System.Reflection;

namespace Battleship
{
	public class OpponentLoader
	{
		public IBattleshipOpponent[] LoadFrom(params string[] args)
		{
			if (args.Length != 2)
				throw new ArgumentException("Must supply two opponents");
			return args.Select(
				file =>
					Activator.CreateInstance(
						Assembly.LoadFrom(file)
						.GetExportedTypes()
						.First(t => t.Implements<IBattleshipOpponent>())
					)
				)
				.Cast<IBattleshipOpponent>()
				.ToArray();
		}
	}
}

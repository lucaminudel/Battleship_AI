namespace Battleship
{
	using System;
	using System.Linq;
	using System.Reflection;

	public class OpponentLoader
	{
		public IBattleshipOpponent[] LoadFrom(params string[] args)
		{
			if (args.Length != 2)
				throw new ArgumentException("Must supply two opponents");

			return args.Select(fileName =>{
				if (fileName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
					return Activator.CreateInstance(
						Assembly.LoadFrom(fileName)
						.GetExportedTypes()
						.First(t => t.Implements<IBattleshipOpponent>())
					);
				else
					return new OutOfProcessOpponent(new ProcessWrapper(fileName));
			})
			.Cast<IBattleshipOpponent>()
			.ToArray();
		}
	}
}

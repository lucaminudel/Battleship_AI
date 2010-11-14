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

			return args.Select(argument => {
			    var isAnAssembly = argument.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase);
			    if (isAnAssembly)
					return Activator.CreateInstance(
						Assembly.LoadFrom(argument)
						.GetExportedTypes()
						.First(t => t.Implements<IBattleshipOpponent>())
					);

			    var isAnExecutable = argument.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase);
			    var isACommand = argument.EndsWith(".bat", StringComparison.InvariantCultureIgnoreCase);
				if (isAnExecutable || isACommand)
					return new OutOfProcessOpponent(new ProcessWrapper(argument));

			    return Assembly.GetAssembly(typeof (OpponentLoader)).CreateInstance(argument);

			})
			.Cast<IBattleshipOpponent>()
			.ToArray();
		}
	}
}

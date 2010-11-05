namespace Battleship
{
	public interface IHaveStandardIO
	{
		string ReadLine();
		void WriteLine(string format, params object[] args);
	}
}

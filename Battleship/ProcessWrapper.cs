namespace Battleship
{
	using System;
	using System.Diagnostics;

	public class ProcessWrapper : IHaveStandardIO, IDisposable
	{
		public ProcessWrapper(string fileName)
		{
			var cfg = new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardInput = true,
				RedirectStandardOutput = true
			};
			process = Process.Start(cfg);
		}

		public string ReadLine()
		{
			return process.StandardOutput.ReadLine();
		}

		public void WriteLine(string format, params object[] args)
		{
			process.StandardInput.WriteLine(format, args);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposed) return;
			if (disposing)
			{
				if (!process.HasExited)
					process.WaitForExit(100);
				process.Dispose();
			}
			disposed = true;
		}

		~ProcessWrapper()
		{
			Dispose(false);
		}

		private Process process;
		private bool disposed;
	}
}

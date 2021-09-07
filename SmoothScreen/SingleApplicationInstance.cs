using System;
using System.Threading;

namespace SmoothScreen
{
	static class SingleApplicationInstance
	{
		const string Name = "1893ecd8-5e1d-435e-b5b4-1e4e8315830a";
		public static bool CanEnter(out IDisposable disposable)
		{
			if (Mutex.TryOpenExisting(Name, out _))
			{
				disposable = null;
				return false;
			}
			else
			{
				disposable = new MutexDispose(new Mutex(true, Name));
				return true;
			}
		}

		class MutexDispose : IDisposable
		{
			private readonly Mutex mutex;

			public MutexDispose(Mutex mutex)
			{
				this.mutex = mutex;
			}

			private bool disposedValue;
			public void Dispose()
			{
				if (!disposedValue)
				{
					mutex.ReleaseMutex();
					mutex.Dispose();

					disposedValue = true;
				}
			}
		}
	}
}

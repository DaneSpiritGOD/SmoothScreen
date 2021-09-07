using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using NLog;

namespace SmoothScreen
{
	class Program
	{
		static readonly MouseState lastState = new MouseState();
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		static void Main(string[] _)
		{
			if (!SingleApplicationInstance.CanEnter(out var disposable))
			{
				logger.Info("A instance is already running. Quit...");
				return;
			}
			
			try
			{
				Hook.GlobalEvents().MouseMove += Program_MouseMove;
				Application.Run();
			}
			finally
			{
				disposable.Dispose();
			}
		}

		private static void Program_MouseMove(object sender, MouseEventArgs e)
		{
			lastState.Point = e.Location;
			logger.Debug(e.Location);
			//if (e.X > 1000 && e.X < 1500)
			//{
			//	Cursor.Position = new Point(e.X + 10, e.Y);
			//}
		}
	}
}

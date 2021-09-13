using System;
using System.Collections.Generic;
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

			HookEvents();
			try 
			{ 
				Run();
			}
			finally
			{
				UnhookEvents();
				disposable.Dispose();
			}
		}

		static void HookEvents()
		{
			Hook.GlobalEvents().MouseMove += Program_MouseMove;
		}

		static void UnhookEvents()
		{
			Hook.GlobalEvents().MouseMove -= Program_MouseMove;
		}

		static void Run()
		{
			using var context = new ApplicationContext();
			Hook.GlobalEvents().OnSequence(new Dictionary<Sequence, Action>
			{
				[Sequence.FromString("Control+Alt+B, Control+Alt+B")] = () =>
				{
					context.ExitThread();
					logger.Info("Receive termination signal, quiting...");
				}
			});
			Application.Run(context);
		}

		private static void Program_MouseMove(object sender, MouseEventArgs e)
		{
			lastState.Point = e.Location;
			logger.Debug(e.Location);
		}
	}
}

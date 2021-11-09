using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoothScreen
{
	class ScreenCutter
	{
		readonly ScreenCollection screens;

		public ScreenCutter(ScreenCollection screens)
		{
			this.screens = screens ?? throw new ArgumentNullException(nameof(screens));
		}

		public void Run()
		{
			var hasMet = new HashSet<(Screener, Screener)>();

			foreach (var screen1 in screens)
			{
				foreach (var screen2 in screens)
				{
					if (hasMet.Contains((screen2, screen1)))
					{
						continue;
					}

					Cut(screen1, screen2);

					hasMet.Add((screen1, screen2)); // (A, B)
				}
			}
		}

		static void Cut(Screener screen1, Screener screen2)
		{
			var left = screen1.LeftBorder;
			var right = screen2.RightBorder;

			Cut(screen2, screen1);
		}
	}
}

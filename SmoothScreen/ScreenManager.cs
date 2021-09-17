using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SmoothScreen
{
	class ScreenManager
	{
		readonly ScreenCollection screens;

		public ScreenManager(ScreenCollection screens)
		{
			this.screens = screens;
		}

		// TODO: add a parameter of last screen in order to improve lookup performance
		public Screener GetOwner(Point point)
		{
			foreach (var screen in screens)
			{
				if (screen.Own(point))
					return screen;
			}

			return Screener.None;
		}
	}
}

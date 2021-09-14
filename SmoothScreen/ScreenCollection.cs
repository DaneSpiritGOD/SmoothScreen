using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmoothScreen
{
	class ScreenCollection : Collection<Screener>
	{
		readonly int closeToBorderThreshold;

		public ScreenCollection(int closeToBorderThreshold) : base()
		{
			this.closeToBorderThreshold = closeToBorderThreshold;
		}

		public ScreenCollection(IEnumerable<Screen> screens, int closeToBorderThreshold) : this(closeToBorderThreshold)
		{
			foreach ( var screen in screens)
			{
				Add(screen);
			}
		}

		public void Add(Screen screen)
		{
			Add(new Screener(screen.Bounds, closeToBorderThreshold));
		}

		public Screener GetOwner(Point point)
		{
			foreach (var screen in Items)
			{
				if (screen.Own(point))
					return screen;
			}

			return Screener.None;
		}
	}
}

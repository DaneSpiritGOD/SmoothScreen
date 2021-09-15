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
		readonly int expandDistance;

		public ScreenCollection(int closeToBorderThreshold, int expandDistance) : base()
		{
			this.closeToBorderThreshold = closeToBorderThreshold;
			this.expandDistance = expandDistance;
		}

		public ScreenCollection(IEnumerable<Screen> screens, int closeToBorderThreshold, int expandDistance)
			: this(closeToBorderThreshold, expandDistance)
		{
			foreach ( var screen in screens)
			{
				Add(screen);
			}
		}

		public void Add(Screen screen)
		{
			Add(new Screener(screen.Bounds, closeToBorderThreshold, expandDistance));
		}
	}
}

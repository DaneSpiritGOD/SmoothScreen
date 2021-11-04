using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoothScreen.Borders
{
	class ScreenBorders
	{
		readonly Screener screener;

		public ScreenBorders(Screener screener)
		{
			this.screener = screener ?? throw new ArgumentNullException(nameof(screener));

			items = new BorderCollection<Border>();
			items.Add(screener.GetTopBorder());
			items.Add(screener.GetRightBorder());
			items.Add(screener.GetBottomBorder());
			items.Add(screener.GetLeftBorder());
		}

		BorderCollection<Border> items;
		public IEnumerable<Border> Items => items;
	}
}

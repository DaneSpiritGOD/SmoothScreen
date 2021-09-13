using System;
using System.Drawing;

namespace SmoothScreen
{
	class Screener
	{
		readonly Rectangle screenBounds;

		readonly Rectangle topLeftRect;
		readonly Rectangle topRect;
		readonly Rectangle topRightRect;
		readonly Rectangle rightRect;
		readonly Rectangle bottomRightRect;
		readonly Rectangle bottomRect;
		readonly Rectangle bottomLeftRect;
		readonly Rectangle leftRect;

		public Screener(Rectangle screenBounds, int closeToBorderThreshold)
		{
			this.screenBounds = screenBounds;

			var x = screenBounds.X;
			var y = screenBounds.Y;
			var w = screenBounds.Width;
			var h = screenBounds.Height;
			var t = closeToBorderThreshold;
			var innerLeftX = x + t;
			var innerRightX = x + w - t;
			var innerTopY = y + t;
			var innerBottomY = y + h - t;
			var innerW = w - 2 * t;
			var innerH = h - 2 * t;

			topLeftRect = new Rectangle(x, y, t, t);
			topRect = new Rectangle(innerLeftX, y, innerW, t);
			topRightRect = new Rectangle(innerRightX, y, t, t);
			rightRect = new Rectangle(innerRightX, innerTopY, t, innerH);
			bottomRightRect = new Rectangle(innerRightX, innerBottomY, t, t);
			bottomRect = new Rectangle(innerLeftX, innerBottomY, innerW, t);
			bottomLeftRect = new Rectangle(x, innerBottomY, t, t);
			leftRect = new Rectangle(x, innerTopY, t, innerH);
		}

		public Border GetCloserBorder(Point point)
			=> point switch
			{
			};//!topLeftRect.Contains(point);

		public bool Own(Point point) => screenBounds.Contains(point);
	}
}

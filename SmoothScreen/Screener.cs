using System;
using System.Drawing;

namespace SmoothScreen
{
	class Screener
	{
		readonly Rectangle screenBounds;

#if DEBUG
		internal readonly Rectangle topLeftRect;
		internal readonly Rectangle topRect;
		internal readonly Rectangle topRightRect;
		internal readonly Rectangle rightRect;
		internal readonly Rectangle bottomRightRect;
		internal readonly Rectangle bottomRect;
		internal readonly Rectangle bottomLeftRect;
		internal readonly Rectangle leftRect;
#else
		readonly Rectangle topLeftRect;
		readonly Rectangle topRect;
		readonly Rectangle topRightRect;
		readonly Rectangle rightRect;
		readonly Rectangle bottomRightRect;
		readonly Rectangle bottomRect;
		readonly Rectangle bottomLeftRect;
		readonly Rectangle leftRect;
#endif

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
		{
			switch (point)
			{
				case var pt when topRect.Contains(pt):
					return Border.Top;
				case var pt when rightRect.Contains(pt):
					return Border.Right;
				case var pt when bottomRect.Contains(pt):
					return Border.Bottom;
				case var pt when leftRect.Contains(pt):
					return Border.Left;

				case var pt when topLeftRect.Contains(pt):
					return pt.X - topLeftRect.Left > pt.Y - topLeftRect.Top ? Border.Top : Border.Left;
				case var pt when topRightRect.Contains(pt):
					return topRightRect.Right - 1 - pt.X > pt.Y - topRightRect.Top ? Border.Top : Border.Right;
				case var pt when bottomRightRect.Contains(pt):
					return bottomRightRect.Right - pt.X > bottomRightRect.Bottom - pt.Y ? Border.Bottom : Border.Right;
				case var pt when bottomLeftRect.Contains(pt):
					return pt.X - bottomLeftRect.Left > bottomLeftRect.Bottom - 1 - pt.Y ? Border.Bottom : Border.Left;
			}

			return Border.None;
		}

		public bool Own(Point point) => screenBounds.Contains(point);
	}
}

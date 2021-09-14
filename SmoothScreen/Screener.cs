using System;
using System.Drawing;

namespace SmoothScreen
{
	class Screener
	{
		readonly Rectangle screenBounds;

		readonly Rectangle _topLeftRect;
		readonly Rectangle _topRect;
		readonly Rectangle _topRightRect;
		readonly Rectangle _rightRect;
		readonly Rectangle _bottomRightRect;
		readonly Rectangle _bottomRect;
		readonly Rectangle _bottomLeftRect;
		readonly Rectangle _leftRect;

#if DEBUG
		internal Rectangle topLeftRect => _topLeftRect;
		internal Rectangle topRect => _topRect;
		internal Rectangle topRightRect => _topRightRect;
		internal Rectangle rightRect => _rightRect;
		internal Rectangle bottomRightRect => _bottomRightRect;
		internal Rectangle bottomRect => _bottomRect;
		internal Rectangle bottomLeftRect => _bottomLeftRect;
		internal Rectangle leftRect => _leftRect;
#endif

		public static readonly Screener None = new Screener(new Rectangle(0, 0, 1, 1), 0);

		internal Screener(Rectangle screenBounds, int closeToBorderThreshold)
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

			_topLeftRect = new Rectangle(x, y, t, t);
			_topRect = new Rectangle(innerLeftX, y, innerW, t);
			_topRightRect = new Rectangle(innerRightX, y, t, t);
			_rightRect = new Rectangle(innerRightX, innerTopY, t, innerH);
			_bottomRightRect = new Rectangle(innerRightX, innerBottomY, t, t);
			_bottomRect = new Rectangle(innerLeftX, innerBottomY, innerW, t);
			_bottomLeftRect = new Rectangle(x, innerBottomY, t, t);
			_leftRect = new Rectangle(x, innerTopY, t, innerH);
		}

		public BorderBase GetCloserBorder(Point point)
		{
			switch (point)
			{
				case var pt when topRect.Contains(pt):
					return topBorder;
				case var pt when rightRect.Contains(pt):
					return rightBorder;
				case var pt when bottomRect.Contains(pt):
					return bottomBorder;
				case var pt when leftRect.Contains(pt):
					return leftBorder;

				case var pt when topLeftRect.Contains(pt):
					return pt.X - topLeftRect.Left > pt.Y - topLeftRect.Top ? topBorder : leftBorder;
				case var pt when topRightRect.Contains(pt):
					return topRightRect.Right - 1 - pt.X > pt.Y - topRightRect.Top ? topBorder : rightBorder;
				case var pt when bottomRightRect.Contains(pt):
					return bottomRightRect.Right - pt.X > bottomRightRect.Bottom - pt.Y ? bottomBorder : rightBorder;
				case var pt when bottomLeftRect.Contains(pt):
					return pt.X - bottomLeftRect.Left > bottomLeftRect.Bottom - 1 - pt.Y ? bottomBorder : leftBorder;
			}

			return noneBorder;
		}

		public bool Own(Point point) => screenBounds.Contains(point);

		public override string ToString() => this == None ? nameof(None) : screenBounds.ToString();

		public override bool Equals(object obj) => obj is Screener screener && screenBounds.Equals(screener.screenBounds);
		public override int GetHashCode() => HashCode.Combine(screenBounds);

		BorderBase _leftBorder;
		BorderBase leftBorder => _leftBorder ??= new LeftBorder(this);

		BorderBase _topBorder;
		BorderBase topBorder => _topBorder ??= new TopBorder(this);

		BorderBase _rightBorder;
		BorderBase rightBorder => _rightBorder ??= new RightBorder(this);

		BorderBase _bottomBorder;
		BorderBase bottomBorder => _bottomBorder ??= new BottomBorder(this);

		BorderBase _noneBorder;
		BorderBase noneBorder => _noneBorder ??= new NoneBorder(this);

		class LeftBorder : BorderBase
		{
			public LeftBorder(Screener screen) : base(screen)
			{
			}
		}

		class TopBorder : BorderBase
		{
			public TopBorder(Screener screen) : base(screen)
			{
			}
		}

		class RightBorder : BorderBase
		{
			public RightBorder(Screener screen) : base(screen)
			{
			}
		}

		class BottomBorder : BorderBase
		{
			public BottomBorder(Screener screen) : base(screen)
			{
			}
		}

		class NoneBorder : BorderBase
		{
			public NoneBorder(Screener screen) : base(screen)
			{
			}
		}
	}
}

using System;
using System.Drawing;

namespace SmoothScreen
{
	class Screener
	{
		public Rectangle Bounds { get; }
		public int Width => Bounds.Width;
		public int Height => Bounds.Height;
		public bool Contains(Point point) => Bounds.Contains(point);

		readonly int closeToBorderThreshold;
		readonly int expandDistance;

		public static readonly Screener None = new Screener(new Rectangle(0, 0, 1, 1), 0, 0);

		internal Screener(Rectangle screenBounds, int closeToBorderThreshold, int expandDistance)
		{
			Bounds = screenBounds;
			this.closeToBorderThreshold = closeToBorderThreshold;
			this.expandDistance = expandDistance;
		}

		public bool Own(Point point) => Bounds.Contains(point);

		public Screener Expand()
		{
			return new Screener(
				new Rectangle(
					Bounds.X - expandDistance,
					Bounds.Y - expandDistance,
					Bounds.Width + 2 * expandDistance,
					Bounds.Height + 2 * expandDistance),
				closeToBorderThreshold,
				expandDistance);
		}

		public override string ToString() => this == None ? nameof(None) : Bounds.ToString();

		public override bool Equals(object obj) => obj is Screener screener && Bounds.Equals(screener.Bounds);
		public override int GetHashCode() => HashCode.Combine(Bounds);
	}
}

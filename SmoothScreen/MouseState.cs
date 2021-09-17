using System.Drawing;

namespace SmoothScreen
{
	readonly struct MouseState
	{
		public MouseState(Screener screen, Point point)
		{
			Screen = screen;
			Point = point;
		}

		public Screener Screen { get; }
		public Point Point { get; }
	}
}

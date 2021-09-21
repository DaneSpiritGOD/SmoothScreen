using System.Drawing;

namespace SmoothScreen
{
	static class RectangleExtensions
	{
		public static Point GetTopLeft(this Rectangle rectangle) => rectangle.Location;
		public static Point GetTopRight(this Rectangle rectangle) => new Point(rectangle.Right - 1, rectangle.Y);
		public static Point GetBottomRight(this Rectangle rectangle) => new Point(rectangle.Right - 1, rectangle.Bottom - 1);
		public static Point GetBottomLeft(this Rectangle rectangle) => new Point(rectangle.X, rectangle.Bottom - 1);
	}
}

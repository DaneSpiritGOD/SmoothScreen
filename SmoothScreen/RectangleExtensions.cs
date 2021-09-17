using System.Drawing;

namespace SmoothScreen
{
	static class RectangleExtensions
	{
		public static Line GetTop(this Rectangle rectangle) => new Line(rectangle.Location, new Point(rectangle.Right - 1, rectangle.Y));
		public static Line GetRight(this Rectangle rectangle) => new Line(new Point(rectangle.Right - 1, rectangle.Y), new Point(rectangle.Right - 1, rectangle.Bottom - 1));
		public static Line GetBottom(this Rectangle rectangle) => new Line(new Point(rectangle.Right - 1, rectangle.Bottom - 1), new Point(rectangle.X, rectangle.Bottom - 1));
		public static Line GetLeft(this Rectangle rectangle) => new Line(new Point(rectangle.X, rectangle.Bottom - 1), rectangle.Location);
	}
}

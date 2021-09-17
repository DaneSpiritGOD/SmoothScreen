using System.Drawing;

namespace SmoothScreen
{
	static class RectangleExtensions
	{
		public static Line GetTopLine(this Rectangle rectangle) => new Line(rectangle.Location, new Point(rectangle.Right - 1, rectangle.Y));
		public static Line GetRightLine(this Rectangle rectangle) => new Line(new Point(rectangle.Right - 1, rectangle.Y), new Point(rectangle.Right - 1, rectangle.Bottom - 1));
		public static Line GetBottomLine(this Rectangle rectangle) => new Line(new Point(rectangle.Right - 1, rectangle.Bottom - 1), new Point(rectangle.X, rectangle.Bottom - 1));
		public static Line GetLeftLine(this Rectangle rectangle) => new Line(new Point(rectangle.X, rectangle.Bottom - 1), rectangle.Location);
	}
}

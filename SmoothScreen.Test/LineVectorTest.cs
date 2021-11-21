using System.Drawing;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class LineVectorTest
	{
		[TestCase(3, 4, 4, 4, 0)]
		[TestCase(4, 3, 5, 4, 45)]
		[TestCase(4, 4, 4, 5, 90)]
		[TestCase(6, 6, 5, 7, 135)]
		[TestCase(-4, 3, -5, 3, 180)]
		[TestCase(-5, -7, -6, -8, 225)]
		[TestCase(9, 10, 9, 9, 270)]
		[TestCase(9, 10, 10, 9, 315)]
		public void TestAngle_NonUnit(int startX, int startY, int endX, int endY, double expectedAngle)
		{
			var vector = new LineVector(new Point(startX, startY), new Point(endX, endY));

			Assert.That(vector.Start, Is.EqualTo(new Point(startX, startY)));
			Assert.That(vector.End, Is.EqualTo(new Point(endX, endY)));
			Assert.That(vector.X, Is.EqualTo(endX - startX));
			Assert.That(vector.Y, Is.EqualTo(endY - startY));
			Assert.That(vector.Angle, Is.EqualTo(expectedAngle).Within(0.01d));
		}
	}
}

using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class LineTest
	{
		[Test]
		public void TestEquals()
		{
			var line1 = new Line(new Point(1, 1), new Point(2, 2));
			var line2 = new Line(new Point(1, 1), new Point(2, 2));

			Assert.That(line1, Is.EqualTo(line2));
		}

		[Test]
		public void TestNotEquals()
		{
			var line1 = new Line(new Point(1, 1), new Point(2, 2));
			var line2 = new Line(new Point(2, 1), new Point(2, 3));

			Assert.That(line1, Is.Not.EqualTo(line2));
		}
	}
}

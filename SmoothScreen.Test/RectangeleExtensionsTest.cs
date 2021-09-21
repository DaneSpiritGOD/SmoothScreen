using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class RectangeleExtensionsTest
	{
		[Test]
		public void TestGetTopLeft()
		{
			Assert.That(rect.GetTopLeft(), Is.EqualTo(new Point(0, 0)));
		}

		[Test]
		public void TestGetTopRight()
		{
			Assert.That(rect.GetTopRight(), Is.EqualTo(new Point(99, 0)));
		}

		[Test]
		public void TestGetBottomRight()
		{
			Assert.That(rect.GetBottomRight(), Is.EqualTo(new Point(99, 99)));
		}

		[Test]
		public void TestGetBottomLeft()
		{
			Assert.That(rect.GetBottomLeft(), Is.EqualTo(new Point(0, 99)));
		}

		Rectangle rect;

		[SetUp]
		public void SetUp()
		{
			rect = new Rectangle(0, 0, 100, 100);
		}
	}
}

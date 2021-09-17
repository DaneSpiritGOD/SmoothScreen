using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class RectangeleExtensionsTest
	{
		[Test]
		public void TestGetTop()
		{
			Assert.That(rect.GetTopLine(), Is.EqualTo(new Line(0, 0, 99, 0)));
		}

		[Test]
		public void TestGetRight()
		{
			Assert.That(rect.GetRightLine(), Is.EqualTo(new Line(99, 0, 99, 99)));
		}

		[Test]
		public void TestGetBottom()
		{
			Assert.That(rect.GetBottomLine(), Is.EqualTo(new Line(99, 99, 0, 99)));
		}

		[Test]
		public void TestGetLeft()
		{
			Assert.That(rect.GetLeftLine(), Is.EqualTo(new Line(0, 99, 0, 0)));
		}

		Rectangle rect;

		[SetUp]
		public void SetUp()
		{
			rect = new Rectangle(0, 0, 100, 100);
		}
	}
}

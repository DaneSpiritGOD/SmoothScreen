using System.Drawing;
using Moq;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class LineExtensionsTest
	{
		[Test]
		public void GetLineStartX()
		{
			Assert.That(line.GetStartX(), Is.EqualTo(1));
		}

		[Test]
		public void GetLineStartY()
		{
			Assert.That(line.GetStartY(), Is.EqualTo(2));
		}

		[Test]
		public void GetLineEndX()
		{
			Assert.That(line.GetEndX(), Is.EqualTo(3));
		}

		[Test]
		public void GetLineEndY()
		{
			Assert.That(line.GetEndY(), Is.EqualTo(4));
		}

		[Test]
		public void GetBorderStartX()
		{
			Assert.That(border.GetStartX(), Is.EqualTo(1));
		}

		[Test]
		public void GetBorderStartY()
		{
			Assert.That(border.GetStartY(), Is.EqualTo(2));
		}

		[Test]
		public void GetBorderEndX()
		{
			Assert.That(border.GetEndX(), Is.EqualTo(3));
		}

		[Test]
		public void GetBorderEndY()
		{
			Assert.That(border.GetEndY(), Is.EqualTo(4));
		}

		Line line;
		Border border;

		[SetUp]
		public void SetUp()
		{
			var screener = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
			line = new Line(new Point(1, 2), new Point(3, 4));
			border = new Mock<Border>(screener, line).Object;
		}
	}
}

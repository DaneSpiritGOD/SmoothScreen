using System.Drawing;
using NUnit.Framework;

namespace SmoothScreen.Test
{
	class BorderBaseTest
	{
		[Test]
		public void TestEqual()
		{
			var border1 = new TestBorder1(screener);
			var border2 = new TestBorder1(screener);

			Assert.That(border1, Is.EqualTo(border2));
		}

		[Test]
		public void TestNotEqual()
		{
			var border1 = new TestBorder1(screener);
			var border2 = new TestBorder2(screener);

			Assert.That(border1, Is.Not.EqualTo(border2));
		}

		[Test]
		public void TestGetHashCode_Equal()
		{
			var border1 = new TestBorder1(screener);
			var border2 = new TestBorder1(screener);

			Assert.That(border1.GetHashCode(), Is.EqualTo(border2.GetHashCode()));
		}

		[Test]
		public void TestGetHashCode_NotEqual()
		{
			var border1 = new TestBorder1(screener);
			var border2 = new TestBorder2(screener);

			Assert.That(border1.GetHashCode(), Is.Not.EqualTo(border2.GetHashCode()));
		}

		Screener screener;

		[SetUp]
		public void SetUp()
		{
			screener = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
		}

		class TestBorder1 : BorderBase
		{
			public TestBorder1(Screener screen) : base(screen)
			{
			}
		}

		class TestBorder2 : BorderBase
		{
			public TestBorder2(Screener screen) : base(screen)
			{
			}
		}
	}
}

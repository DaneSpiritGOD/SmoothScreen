using System;
using System.Drawing;
using System.Reflection;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	abstract class BorderBaseTest<T> where T : BorderBase
	{
		[TestCase("top", 0, 0, 100)]
		[TestCase("right", 99, 0, 100)]
		[TestCase("bottom", 99, 99, 100)]
		[TestCase("left", 0, 99, 100)]
		public void TestConstructor_Unit(string unit, int startX, int startY, int length)
		{
			Assert.DoesNotThrow(() => CreateBorder(unit, startX, startY, length));
		}

		[Test]
		public void TestConstructor_IncorrectUnit()
		{
			TestBorderException(new BorderVector(10, 20), 0, 0, 100, "Non-unit BorderVector is passed as unit.");
		}

		[TestCase(-1)]
		[TestCase(100)]
		public void TestConstructor_StartPointNotContained(int startX)
		{
			TestBorderException(BorderVector.TopUnit, startX, 0, 100, "Start point is not in screen.");
		}

		[Test]
		public void TestConstructor_LengthOverWidthOrHeight()
		{
			TestBorderException(BorderVector.TopUnit, 0, 0, 101, "Length is over bound.");
		}

		[TestCase(1, 100)]
		[TestCase(99, 2)]
		public void TestConstructor_EndPointNotContained(int startX, int length)
		{
			TestBorderException(BorderVector.TopUnit, startX, 0, length, "End point is not in screen.");
		}

		void TestBorderException(BorderVector borderVector, int startX, int startY, int length, string expectedMessage)
		{
			var tie = Assert.Catch<TargetInvocationException>(() => CreateBorder(borderVector, startX, startY, length));
			Assert.That(tie.InnerException, Is.TypeOf<BorderException>());
			Assert.That(tie.InnerException.Message, Is.EqualTo(expectedMessage));
		}

		protected Screener screener;

		[SetUp]
		public void SetUp()
		{
			screener = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
		}

		protected T CreateBorder(BorderVector unit, Point startPoint, int length)
			=> (T)Activator.CreateInstance(typeof(T), screener, unit, startPoint, length);

		protected T CreateBorder(BorderVector unit, int startX, int startY, int length)
			=> CreateBorder(unit, new Point(startX, startY), length);

		protected T CreateBorder(string unit, int startX, int startY, int length)
			=> CreateBorder(unit.ConvertToUnit(), new Point(startX, startY), length);
	}
}

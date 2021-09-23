using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
			var tie = Assert.Catch<TargetInvocationException>(() => CreateBorder(new BorderVector(10, 20), 0, 0, 100));
			Assert.That(tie.InnerException, Is.TypeOf<BorderException>());
			Assert.That(tie.InnerException.Message, Is.EqualTo("Non-unit BorderVector is passed as unit."));
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

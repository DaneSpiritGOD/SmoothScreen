//using System;
//using System.Drawing;
//using System.Reflection;
//using NUnit.Framework;
//using SmoothScreen.Borders;

//namespace SmoothScreen.Test
//{
//	abstract class BorderBaseTest<T> where T : BorderBase, IComparable<T>
//	{
//		[TestCase("top", 0, 0, 100)]
//		[TestCase("right", 99, 0, 50)]
//		[TestCase("bottom", 99, 49, 100)]
//		[TestCase("left", 0, 49, 50)]
//		public void TestConstructor_Unit(string unit, int startX, int startY, int length)
//		{
//			Assert.DoesNotThrow(() => CreateBorder(unit, startX, startY, length));
//		}

//		[Test]
//		public void TestConstructor_IncorrectUnit()
//		{
//			TestConstructorException(new BorderVector(10, 20), 0, 0, 100, "Non-unit BorderVector is passed as unit.");
//		}

//		[TestCase(-1)]
//		[TestCase(100)]
//		public void TestConstructor_StartPointNotContained(int startX)
//		{
//			TestConstructorException(LineVector.TopUnit, startX, 0, 100, "Start point is not in screen.");
//		}

//		[TestCase(1, 100)]
//		[TestCase(99, 2)]
//		public void TestConstructor_EndPointNotContained(int startX, int length)
//		{
//			TestConstructorException(LineVector.TopUnit, startX, 0, length, "End point is not in screen.");
//		}

//		[Test]
//		public void TestCompare_NotSameScreen()
//		{
//			var border1 = CreateBorder(LineVector.TopUnit, new Point(0, 0), 100);

//			var screener2 = new Screener(new Rectangle(0, 0, 100, 100), 5, 10);
//			var border2 = CreateBorder(screener2, LineVector.TopUnit, new Point(0, 0), 100);

//			TestCompareException(border1, border2, "Same screen is required.");
//		}

//		void TestConstructorException(LineVector borderVector, int startX, int startY, int length, string expectedMessage)
//		{
//			var tie = Assert.Catch<TargetInvocationException>(() => CreateBorder(borderVector, startX, startY, length));
//			Assert.That(tie.InnerException, Is.TypeOf<BorderException>());
//			Assert.That(tie.InnerException.Message, Is.EqualTo(expectedMessage));
//		}

//		protected void TestCompareException(T border1, T border2, string expectedMessage)
//		{
//			var be = Assert.Catch<BorderException>(() => border1.CompareTo(border2));
//			Assert.That(be.Message, Is.EqualTo(expectedMessage));
//		}

//		static readonly protected Screener screener = new Screener(new Rectangle(0, 0, 100, 50), 5, 10);

//		static protected T CreateBorder(Screener screener, LineVector unit, Point startPoint, int length)
//			=> (T)Activator.CreateInstance(typeof(T), screener, unit, startPoint, length);

//		static protected T CreateBorder(LineVector unit, Point startPoint, int length)
//			=> (T)Activator.CreateInstance(typeof(T), screener, unit, startPoint, length);

//		static protected T CreateBorder(LineVector unit, int startX, int startY, int length)
//			=> CreateBorder(unit, new Point(startX, startY), length);

//		static protected T CreateBorder(string unit, int startX, int startY, int length)
//			=> CreateBorder(unit.ConvertToUnit(), new Point(startX, startY), length);
//	}
//}

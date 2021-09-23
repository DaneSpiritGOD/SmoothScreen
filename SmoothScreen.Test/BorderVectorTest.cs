using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class BorderVectorTest
	{
		[Test]
		public void TestTopUnit()
		{
			var unit = BorderVector.TopUnit;
			Assert.That(unit.X, Is.EqualTo(1));
			Assert.That(unit.Y, Is.EqualTo(0));
		}

		[Test]
		public void TestRightUnit()
		{
			var unit = BorderVector.RightUnit;
			Assert.That(unit.X, Is.EqualTo(0));
			Assert.That(unit.Y, Is.EqualTo(1));
		}

		[Test]
		public void TestBottomUnit()
		{
			var unit = BorderVector.BottomUnit;
			Assert.That(unit.X, Is.EqualTo(-1));
			Assert.That(unit.Y, Is.EqualTo(0));
		}

		[Test]
		public void TestLeftUnit()
		{
			var unit = BorderVector.LeftUnit;
			Assert.That(unit.X, Is.EqualTo(0));
			Assert.That(unit.Y, Is.EqualTo(-1));
		}

		[Test]
		public void TestEquals_True()
		{
			var vector1 = new BorderVector(1, 1);
			var vector2 = new BorderVector(1, 1);

			Assert.That(vector1, Is.EqualTo(vector2));
		}

		[Test]
		public void TestEquals_False()
		{
			var vector1 = new BorderVector(1, 1);
			var vector2 = new BorderVector(1, 2);

			Assert.That(vector1, Is.Not.EqualTo(vector2));
		}

		[Test]
		public void TestCompare()
		{
			Assert.That(BorderVector.TopUnit, Is.LessThan(BorderVector.RightUnit));
			Assert.That(BorderVector.RightUnit, Is.LessThan(BorderVector.BottomUnit));
			Assert.That(BorderVector.BottomUnit, Is.LessThan(BorderVector.LeftUnit));
		}

		[Test]
		public void TestCannotCompare()
		{
			var vector1 = new BorderVector(1, 1);

			Assert.That(() => BorderVector.TopUnit.CompareTo(vector1), Throws.TypeOf<BorderException>());
			Assert.That(() => vector1.CompareTo(BorderVector.TopUnit), Throws.TypeOf<BorderException>());

			Assert.That(() => BorderVector.RightUnit.CompareTo(vector1), Throws.TypeOf<BorderException>());
			Assert.That(() => vector1.CompareTo(BorderVector.RightUnit), Throws.TypeOf<BorderException>());

			Assert.That(() => BorderVector.BottomUnit.CompareTo(vector1), Throws.TypeOf<BorderException>());
			Assert.That(() => vector1.CompareTo(BorderVector.BottomUnit), Throws.TypeOf<BorderException>());

			Assert.That(() => BorderVector.LeftUnit.CompareTo(vector1), Throws.TypeOf<BorderException>());
			Assert.That(() => vector1.CompareTo(BorderVector.LeftUnit), Throws.TypeOf<BorderException>());

			var vector2 = new BorderVector(1, 1);
			Assert.That(() => vector1.CompareTo(vector2), Throws.TypeOf<BorderException>());
			Assert.That(() => vector2.CompareTo(vector1), Throws.TypeOf<BorderException>());
		}

		[TestCase(1, 2, 3, 4, 11)]
		[TestCase(3, 4, 1, 2, 11)]
		public void TestDot(int vectorX1, int vectorY1, int vectorX2, int vectorY2, int expectedResult)
		{
			var vector1 = new BorderVector(vectorX1, vectorY1);
			var vector2 = new BorderVector(vectorX2, vectorY2);

			Assert.That(BorderVector.Dot(vector1, vector2), Is.EqualTo(expectedResult));
		}
	}
}

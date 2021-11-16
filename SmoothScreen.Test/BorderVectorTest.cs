using System;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class BorderVectorTest
	{
		[TestCase("top", 1, 0)]
		[TestCase("right", 0, 1)]
		[TestCase("bottom", -1, 0)]
		[TestCase("left", 0, -1)]
		public void TestTopUnit(string flag, int expectedX, int expectedY)
		{
			var unit = flag.ConvertToUnit();
			Assert.That(unit.X, Is.EqualTo(expectedX));
			Assert.That(unit.Y, Is.EqualTo(expectedY));
		}

		[TestCase("top", 0)]
		[TestCase("right", 90)]
		[TestCase("bottom", 180)]
		[TestCase("left", 270)]
		public void TestAngle_Unit(string flag, double expectedAngle)
		{
			var unit = flag.ConvertToUnit();
			Assert.That(unit.Angle, Is.EqualTo(expectedAngle).Within(0.01d));
		}

		[TestCase(1, 1, 45)]
		[TestCase(-1, 1, 135)]
		[TestCase(-1, -1, 225)]
		[TestCase(1, -1, 315)]
		public void TestAngle_NonUnit(int x, int y, double expectedAngle)
		{
			var vector = new BorderVector(x, y);
			Assert.That(vector.Angle, Is.EqualTo(expectedAngle).Within(0.01d));
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

		[TestCase("top", "right")]
		[TestCase("right", "bottom")]
		[TestCase("bottom", "left")]
		public void TestCanCompare(string leftOperand, string rightOperand)
		{
			Assert.That(leftOperand.ConvertToUnit(), Is.LessThan(rightOperand.ConvertToUnit()));
		}

		[TestCase(1, 2, 3, 4, 11)]
		[TestCase(3, 4, 1, 2, 11)]
		public void TestDot(int vectorX1, int vectorY1, int vectorX2, int vectorY2, int expectedResult)
		{
			var vector1 = new BorderVector(vectorX1, vectorY1);
			var vector2 = new BorderVector(vectorX2, vectorY2);

			Assert.That(BorderVector.Dot(vector1, vector2), Is.EqualTo(expectedResult));
		}

		[Test]
		public void TestMultiply_Left()
		{
			var left = new BorderVector(1, 2);
			var right = 3;

			Assert.That(left * right, Is.EqualTo(new BorderVector(3, 6)));
		}

		[Test]
		public void TestMultiply_Right()
		{
			var left = 3;
			var right = new BorderVector(1, 2);

			Assert.That(left * right, Is.EqualTo(new BorderVector(3, 6)));
		}

		[Test]
		public void TestPlus()
		{
			var left = new BorderVector(1, 2);
			var right = new BorderVector(3, 4);

			Assert.That(left + right, Is.EqualTo(new BorderVector(4, 6)));
		}

		[Test]
		public void TestMinus()
		{
			var left = new BorderVector(1, 2);
			var right = new BorderVector(3, 4);

			Assert.That(left - right, Is.EqualTo(new BorderVector(-2, -2)));
		}

		[Test]
		public void TestLength()
		{
			var vector = new BorderVector(3, 4);
			Assert.That(vector.Length(), Is.EqualTo(5));
		}

		[Test]
		public void TestLengthSquared()
		{
			var vector = new BorderVector(3, 4);
			Assert.That(vector.LengthSquared(), Is.EqualTo(25));
		}

		[Test]
		public void TestGetRelation_Orthometric()
		{
			var vector1 = new BorderVector(3, 4);
			var vector2 = new BorderVector(4, -3);
			Assert.That(BorderVector.GetRelation(vector1, vector2), Is.EqualTo(BorderVectorRelation.Orthometric));
		}

		[Test]
		public void TestGetRelation_AcuteAngle()
		{
			var vector1 = new BorderVector(3, 4);
			var vector2 = new BorderVector(3, -2);
			Assert.That(BorderVector.GetRelation(vector1, vector2), Is.EqualTo(BorderVectorRelation.AcuteAngle));
		}

		[Test]
		public void TestGetRelation_Obtuse()
		{
			var vector1 = new BorderVector(3, 4);
			var vector2 = new BorderVector(3, -4);
			Assert.That(BorderVector.GetRelation(vector1, vector2), Is.EqualTo(BorderVectorRelation.ObtuseAngle));
		}

		[Test]
		public void TestGetRelation_SameLineSameDirection()
		{
			var vector1 = new BorderVector(3, 4);
			var vector2 = new BorderVector(6, 8);
			Assert.That(BorderVector.GetRelation(vector1, vector2), Is.EqualTo(BorderVectorRelation.SameLineSameDirection));
		}

		[Test]
		public void TestGetRelation_SameLineReverseDirection()
		{
			var vector1 = new BorderVector(3, 4);
			var vector2 = new BorderVector(-3, -4);
			Assert.That(BorderVector.GetRelation(vector1, vector2), Is.EqualTo(BorderVectorRelation.SameLineReverseDirection));
		}

		[TestCase(0, 0, -3, -4)]
		[TestCase(-3, -4, 0, 0)]
		public void TestGetRelation_Zero(int startX1, int startY1, int startX2, int startY2)
		{
			var vector1 = new BorderVector(startX1, startY1);
			var vector2 = new BorderVector(startX2, startY2);
			Assert.That(() => BorderVector.GetRelation(vector1, vector2), Throws.TypeOf<NotSupportedException>());
		}

		[TestCase("top")]
		[TestCase("right")]
		[TestCase("bottom")]
		[TestCase("left")]
		public void TestIsUnit_True(string flag)
		{
			var unit = flag.ConvertToUnit();
			Assert.That(BorderVector.IsUnit(unit), Is.True);
		}

		[Test]
		public void TestIsUnit_False()
		{
			var unit = new BorderVector(1, 1);
			Assert.That(BorderVector.IsUnit(unit), Is.False);
		}
	}
}

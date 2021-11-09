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

		[TestCase("top")]
		[TestCase("right")]
		[TestCase("bottom")]
		[TestCase("left")]
		[TestCase("")]
		public void TestCannotCompare(string flag)
		{
			var vector1 = new BorderVector(1, 1);
			var vector2 = !string.IsNullOrEmpty(flag) ? flag.ConvertToUnit() : new BorderVector(1, 1);

			Assert.That(() => vector2.CompareTo(vector1), Throws.TypeOf<BorderException>());
			Assert.That(() => vector1.CompareTo(vector2), Throws.TypeOf<BorderException>());
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
		public void TestGetRelation_Other()
		{
			var vector1 = new BorderVector(3, 4);
			var vector2 = new BorderVector(3, -3);
			Assert.That(BorderVector.GetRelation(vector1, vector2), Is.EqualTo(BorderVectorRelation.Other));
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
		public void TestIsAxis_True(string flag)
		{
			var unit = flag.ConvertToUnit();
			Assert.That(BorderVector.IsAxis(unit), Is.True);
		}

		[Test]
		public void TestIsAxis_False()
		{
			var unit = new BorderVector(1, 1);
			Assert.That(BorderVector.IsAxis(unit), Is.False);
		}
	}
}

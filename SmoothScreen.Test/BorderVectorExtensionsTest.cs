using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class BorderVectorExtensionsTest
	{
		[Test]
		public void TestTop()
		{
			Assert.That("top".ConvertToUnit(), Is.EqualTo(BorderVector.TopUnit));
		}

		[Test]
		public void TestRight()
		{
			Assert.That("right".ConvertToUnit(), Is.EqualTo(BorderVector.RightUnit));
		}

		[Test]
		public void TestBottom()
		{
			Assert.That("bottom".ConvertToUnit(), Is.EqualTo(BorderVector.BottomUnit));
		}

		[Test]
		public void TestLeft()
		{
			Assert.That("left".ConvertToUnit(), Is.EqualTo(BorderVector.LeftUnit));
		}
	}
}

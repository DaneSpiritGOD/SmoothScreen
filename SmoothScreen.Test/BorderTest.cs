using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	class BorderTest : BorderBaseTest<Border>
	{
		[Test]
		public void TestCompare_RequireDistinctUnit()
		{
			var border1 = CreateBorder(BorderVector.TopUnit, new Point(0, 0), 100);
			var border2 = CreateBorder(BorderVector.TopUnit, new Point(0, 0), 100);
			TestCompareException(border1, border2, "Distinct axis is required.");
		}

		[TestCaseSource(nameof(GetSourcesForCompare))]
		public void TestCompare(Border border1, Border border2, int exptectedResult)
		{
			Assert.That(border1.CompareTo(border2), Is.EqualTo(exptectedResult));
		}

		[TestCaseSource(nameof(GetScreenBorders))]
		public void TestDoesClingTo()
		{
		}

		static IEnumerable<TestCaseData> GetScreenBorders()
		{
			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 10, 10), 0, 0), Array.Empty<string>());

			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 50, 10), 0, 0), Array.Empty<string>());
			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 51, 10), 0, 0), Array.Empty<string>());
			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 52, 10), 0, 0), Array.Empty<string>());

			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 10, 50), 0, 0), Array.Empty<string>());
			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 10, 51), 0, 0), Array.Empty<string>());
			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 10, 52), 0, 0), Array.Empty<string>());

			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 50, 50), 0, 0), Array.Empty<string>());
			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 51, 51), 0, 0), Array.Empty<string>());
			yield return new TestCaseData(new Screener(new Rectangle(-50, -50, 52, 52), 0, 0), Array.Empty<string>());
		}

		static IEnumerable<TestCaseData> GetSourcesForCompare()
		{
			var units = new[]
			{
				("top", 0, 0, 100, 0),
				("right", 99, 0, 50, 90),
				("bottom", 99, 49, 100, 180),
				("left", 0, 49, 50, 270),
			};

			foreach (var unit1 in units)
			{
				foreach (var unit2 in units)
				{
					if (unit1 == unit2)
					{
						continue;
					}

					yield return new TestCaseData(
						CreateBorder(unit1.Item1, unit1.Item2, unit1.Item3, unit1.Item4),
						CreateBorder(unit2.Item1, unit2.Item2, unit2.Item3, unit2.Item4),
						unit1.Item5 - unit2.Item5);
				}
			}
		}
	}
}

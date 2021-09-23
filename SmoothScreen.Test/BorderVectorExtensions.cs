using System;
using SmoothScreen.Borders;

namespace SmoothScreen.Test
{
	static class BorderVectorExtensions
	{
		public static BorderVector ConvertToUnit(this string unit)
			=> unit switch
			{
				"top" => BorderVector.TopUnit,
				"right" => BorderVector.RightUnit,
				"bottom" => BorderVector.BottomUnit,
				"left" => BorderVector.LeftUnit,
				_ => throw new NotImplementedException(),
			};
	}
}

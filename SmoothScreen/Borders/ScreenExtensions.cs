namespace SmoothScreen.Borders
{
	static class ScreenExtensions
	{
		public static Border GetTopBorder(this Screener screener)
			=> new Border(screener, BorderVector.TopUnit, screener.Bounds.GetTopLeft(), screener.Width);

		public static Border GetRightBorder(this Screener screener)
			=> new Border(screener, BorderVector.RightUnit, screener.Bounds.GetTopRight(), screener.Height);

		public static Border GetBottomBorder(this Screener screener)
			=> new Border(screener, BorderVector.BottomUnit, screener.Bounds.GetBottomRight(), screener.Width);

		public static Border GetLeftBorder(this Screener screener)
			=> new Border(screener, BorderVector.LeftUnit, screener.Bounds.GetBottomLeft(), screener.Height);
	}
}

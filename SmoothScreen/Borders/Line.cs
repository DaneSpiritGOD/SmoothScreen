namespace System.Drawing
{
	public struct Line
	{
		public Line(Point start, Point end)
		{
			Start = start;
			End = end;
		}

		public Point Start { get; }
		public Point End { get; }
	}
}

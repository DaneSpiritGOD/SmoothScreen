namespace System.Drawing
{
	// TODO: Replace by Vector
	public readonly struct Line
	{
		public Line(Point start, Point end)
		{
			Start = start;
			End = end;
		}

		public Line(int startX, int startY, int endX, int endY) : this(new Point(startX, startY), new Point(endX, endY))
		{
		}

		public Point Start { get; }
		public Point End { get; }

		public readonly bool Equals(Line line) => Start.Equals(line.Start) && End.Equals(line.End);
	}
}

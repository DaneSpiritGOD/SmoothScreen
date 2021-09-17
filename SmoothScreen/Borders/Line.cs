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

		public bool Equals(Line line) => Start.Equals(line.Start) && End.Equals(line.End);
	}
}

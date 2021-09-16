using System.Collections.Generic;
using System.Drawing;

namespace SmoothScreen
{
	internal abstract class Border
	{
		public Screener Owner { get; }
		protected readonly Line line;

		protected Border(Screener screen, Line line)
		{
			Owner = screen;
			this.line = line;
		}

		public Border Next { get; set; }
	}

	class LeftBorder : Border
	{
		public LeftBorder(Screener screen, Line line) : base(screen, line)
		{
		}
	}

	class TopBorder : Border
	{
		public TopBorder(Screener screen, Line line) : base(screen, line)
		{
		}
	}

	class RightBorder : Border
	{
		public RightBorder(Screener screen, Line line) : base(screen, line)
		{
		}
	}

	class BottomBorder : Border
	{
		public BottomBorder(Screener screen, Line line) : base(screen, line)
		{
		}
	}

	class NoneBorder : Border
	{
		static readonly Line LineForNone = new Line(new Point(0, 0), new Point(0, 0));

		public NoneBorder(Screener screen, Line line) : base(screen, LineForNone)
		{
		}
	}
}

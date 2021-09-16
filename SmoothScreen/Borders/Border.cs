using System.Collections.Generic;

namespace SmoothScreen
{
	internal abstract class Border
	{
		private readonly Screener screen;

		protected Border(Screener screen)
		{
			this.screen = screen;
		}

		public override bool Equals(object obj)
			=> obj.GetType() == GetType() && EqualityComparer<Screener>.Default.Equals(screen, (obj as Border).screen);
		public override int GetHashCode() => System.HashCode.Combine(screen, GetType().FullName);
	}

	class LeftBorder : Border
	{
		public LeftBorder(Screener screen) : base(screen)
		{
		}
	}

	class TopBorder : Border
	{
		public TopBorder(Screener screen) : base(screen)
		{
		}
	}

	class RightBorder : Border
	{
		public RightBorder(Screener screen) : base(screen)
		{
		}
	}

	class BottomBorder : Border
	{
		public BottomBorder(Screener screen) : base(screen)
		{
		}
	}

	class NoneBorder : Border
	{
		public NoneBorder(Screener screen) : base(screen)
		{
		}
	}
}

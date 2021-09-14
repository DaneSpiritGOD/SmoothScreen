using System.Collections.Generic;

namespace SmoothScreen
{
	internal abstract class BorderBase
	{
		private readonly Screener screen;

		protected BorderBase(Screener screen)
		{
			this.screen = screen;
		}

		public override bool Equals(object obj)
			=> obj.GetType() == GetType() && EqualityComparer<Screener>.Default.Equals(screen, (obj as BorderBase).screen);
		public override int GetHashCode() => System.HashCode.Combine(screen, GetType().FullName);
	}
}

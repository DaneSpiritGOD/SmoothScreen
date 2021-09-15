using System.Drawing;

namespace SmoothScreen
{
	// TODO: create state from pool
	// TODO: create collection of queue for state (fixed length array)
	readonly struct MouseState
	{
		public Screener Screen { get; }
		public Point Point { get; }
	}
}

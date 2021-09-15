using System.Drawing;

namespace SmoothScreen
{
	// TODO: create state from pool
	// TODO: create collection of queue for state (fixed length array)
	struct MouseState
	{
		public Screener Screen { get; set; }
		public Point Point { get; set; }
	}
}

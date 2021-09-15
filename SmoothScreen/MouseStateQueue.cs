using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SmoothScreen
{
	class MouseStateQueue
	{
		readonly MouseState[] items;
		int head;
		int tail;

		public MouseStateQueue(int capacity)
		{
			items = new MouseState[capacity];
			head = tail = -1;
		}

		public int Length => tail - head + (tail < head ? items.Length : 0);

		public void Add(Screener screen, Point point)
		{
			if (Length < items.Length)
			{
				items[tail++] = new MouseState
				{
					Screen = screen,
					Point = point
				};
			}
			else
			{

			}
		}
	}
}

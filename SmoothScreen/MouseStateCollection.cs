namespace SmoothScreen
{
	class MouseStateCollection : FixLengthCollection<MouseState>
	{
		public MouseStateCollection(int capacity) : base(capacity)
		{
		}

		protected override int GetItemsIndex(int index) => base.GetItemsIndex(Length - 1 - index);
	}
}

using System;

namespace SmoothScreen.Borders
{
	class BorderException : Exception
	{
		public BorderException()
		{
		}

		public BorderException(string message) : base(message)
		{
		}

		public BorderException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

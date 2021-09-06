using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace SmoothScreen
{
	class Program
	{
		static void Main(string[] args)
		{
			Hook.GlobalEvents().MouseMoveExt += Program_MouseMove;
			Application.Run();
		}

		private static void Program_MouseMove(object sender, MouseEventArgs e)
		{
			Debug.WriteLine(e.Location);
			if (e.X > 1000 && e.X < 1500)
			{
				Cursor.Position = new Point(e.X + 10, e.Y);
			}
		}
	}
}

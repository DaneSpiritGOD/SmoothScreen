using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoothScreen.Borders
{
	static class LineVectorExtensions
	{
		public static bool IsCollinearAndSameDirection(LineVector lineVector1, LineVector lineVector2)
		{
			if (lineVector1.IsZero)
			{
				return false;
			}

			if (lineVector2.IsZero)
			{
				return false;
			}

			if (lineVector1.Angle != lineVector2.Angle)
			{
				return false;
			}

			var lineVector3 = new LineVector(lineVector1.Start, lineVector2.Start);
			if (!lineVector3.IsZero && !IsAngleZero(lineVector1.Angle - lineVector3.Angle))
			{
				return false;
			}

			var lineVector4 = new LineVector(lineVector1.Start, lineVector2.End);
			if (!lineVector4.IsZero && !IsAngleZero(lineVector1.Angle - lineVector4.Angle))
			{
				return false;
			}

			return true;

			static bool IsAngleZero(double angle) => angle == 0 || Math.Abs(angle) == 180;
		}
	}
}

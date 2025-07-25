﻿using System.Drawing;
using System.Drawing.Drawing2D;

namespace NovaUI.NetCore.Helpers
{
    internal static class GraphicsHelper
    {
        public static GraphicsPath Round(this Rectangle rect, int radius)
		{
			return rect.Round(radius, true);
		}

        public static GraphicsPath Round(this RectangleF rect, int radius)
        {
            return rect.Round(radius, true);
        }

        public static GraphicsPath Round(this Rectangle rect, int radius, bool topLeft = true, bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
        {
			GraphicsPath path = new();
			radius *= 2;

			path.StartFigure();

			if (topLeft) path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
			else
			{
				path.AddLine(rect.X, rect.Y + radius, rect.X, rect.Y);
				path.AddLine(rect.X, rect.Y, rect.X + radius, rect.Y);
			}

			if (topRight) path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
			else
			{
				path.AddLine(rect.X + rect.Width - radius, rect.Y, rect.X + rect.Width, rect.Y);
				path.AddLine(rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + radius);
			}

			if (bottomRight) path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
			else
			{
				path.AddLine(rect.X + rect.Width, rect.Y + rect.Height - radius, rect.X + rect.Width, rect.Y + rect.Height);
				path.AddLine(rect.X + rect.Width, rect.Y + rect.Height, rect.X + rect.Width - radius, rect.Y + rect.Height);
			}

			if (bottomLeft) path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
			else
			{
				path.AddLine(rect.X + radius, rect.Y + rect.Height, rect.X, rect.Y + rect.Height);
				path.AddLine(rect.X, rect.Y + rect.Height, rect.X, rect.Y + rect.Height - radius);
			}

			path.CloseFigure();

			return path;
		}

        public static GraphicsPath Round(this RectangleF rect, int radius, bool topLeft = true, bool topRight = true, bool bottomLeft = true, bool bottomRight = true)
		{
			GraphicsPath path = new();
			radius *= 2;

			path.StartFigure();

			if (topLeft) path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
			else
			{
				path.AddLine(rect.X, rect.Y + radius, rect.X, rect.Y);
				path.AddLine(rect.X, rect.Y, rect.X + radius, rect.Y);
			}

			if (topRight) path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
			else
			{
				path.AddLine(rect.X + rect.Width - radius, rect.Y, rect.X + rect.Width, rect.Y);
				path.AddLine(rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + radius);
			}

			if (bottomRight) path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
			else
			{
				path.AddLine(rect.X + rect.Width, rect.Y + rect.Height - radius, rect.X + rect.Width, rect.Y + rect.Height);
				path.AddLine(rect.X + rect.Width, rect.Y + rect.Height, rect.X + rect.Width - radius, rect.Y + rect.Height);
			}

			if (bottomLeft) path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
			else
			{
				path.AddLine(rect.X + radius, rect.Y + rect.Height, rect.X, rect.Y + rect.Height);
				path.AddLine(rect.X, rect.Y + rect.Height, rect.X, rect.Y + rect.Height - radius);
			}

			path.CloseFigure();

			return path;
		}

		public static Rectangle Rescale(this Rectangle rect, int posOffset, int sizeOffset)
		{
			RectangleF res = ((RectangleF)rect).Rescale(posOffset, sizeOffset);
			return new((int)res.X, (int)res.Y, (int)res.Width, (int)res.Height);
		}

		public static RectangleF Rescale(this RectangleF rect, int posOffset, int sizeOffset)
		{
			return new(rect.X + posOffset, rect.Y + posOffset, rect.Width + sizeOffset, rect.Height + sizeOffset);
		}
	}
}

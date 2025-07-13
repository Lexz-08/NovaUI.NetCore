using System.Drawing.Drawing2D;

namespace NovaUI.NetCore.Helpers
{
    public sealed class Bitmaps
    {
		private const int pos = 11;

		private Bitmaps() { }

		public static Bitmaps Instance => new();

		public Bitmap this[int BmpSize, Color BmpColor, BitmapType BmpType]
		{
			get
			{
				Bitmap bmp = new Bitmap(BmpSize, BmpSize);
				Rectangle rect = new Rectangle(0, 0, BmpSize, BmpSize);

				switch (BmpType)
				{
					case BitmapType.Close:
						using (Graphics g = Graphics.FromImage(bmp))
						{
							g.SmoothingMode = SmoothingMode.HighQuality;
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + pos, rect.X + pos),
								new Point(rect.X + rect.Width - pos, rect.Y + rect.Height - pos));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + rect.Width - pos, rect.Y + pos),
								new Point(rect.X + pos, rect.Y + rect.Height - pos));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + pos, rect.X + pos),
								new Point(rect.X + rect.Width - pos, rect.Y + rect.Height - pos));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + rect.Width - pos, rect.Y + pos),
								new Point(rect.X + pos, rect.Y + rect.Height - pos));
						}
						break;
					case BitmapType.Maximize:
						using (Graphics g = Graphics.FromImage(bmp))
						{
							g.SmoothingMode = SmoothingMode.HighQuality;
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + pos, rect.Y + pos),
								new Point(rect.X + pos, rect.Y + rect.Height - pos));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + pos, rect.Y + rect.Height - pos),
								new Point(rect.X + rect.Width - pos, rect.Y + rect.Height - pos));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + rect.Width - pos, rect.Y + rect.Height - pos),
								new Point(rect.X + rect.Width - pos, rect.Y + pos));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + rect.Width - pos, rect.Y + pos),
								new Point(rect.X + pos, rect.Y + pos));
						}
						break;
					case BitmapType.Minimize:
						using (Graphics g = Graphics.FromImage(bmp))
						{
							g.SmoothingMode = SmoothingMode.HighQuality;
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + pos, rect.Y + (rect.Width / 2) + 1),
								new Point(rect.X + rect.Width - pos, rect.Y + (rect.Width / 2) + 1));
						}
						break;
					case BitmapType.Restore:
						int spaceDiff = 2;

						using (Graphics g = Graphics.FromImage(bmp))
						{
							g.SmoothingMode = SmoothingMode.HighQuality;
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + pos, rect.Y + pos + spaceDiff),
								new Point(rect.X + pos, rect.Y + rect.Height - pos));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + pos, rect.Y + rect.Height - pos),
								new Point(rect.X + rect.Width - pos - spaceDiff, rect.Y + rect.Height - pos));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + rect.Width - pos - spaceDiff, rect.Y + rect.Height - pos),
								new Point(rect.X + rect.Width - pos - spaceDiff, rect.Y + pos + spaceDiff));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + rect.Width - pos - spaceDiff, rect.Y + pos + spaceDiff),
								new Point(rect.X + pos, rect.Y + pos + spaceDiff));

							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + pos + spaceDiff, rect.Y + pos),
								new Point(rect.X + pos + spaceDiff, rect.Y + pos + spaceDiff));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + pos + spaceDiff, rect.Y + pos),
								new Point(rect.X + rect.Width - pos, rect.Y + pos));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + rect.Width - pos, rect.Y + pos),
								new Point(rect.X + rect.Width - pos, rect.Y + rect.Height - pos - spaceDiff));
							g.DrawLine(new Pen(BmpColor),
								new Point(rect.X + rect.Width - pos, rect.Y + rect.Height - pos - spaceDiff),
								new Point(rect.X + rect.Width - pos - spaceDiff, rect.Y + rect.Height - pos - spaceDiff));
						}
						break;
				}

				return bmp;
			}
		}
	}

	public enum BitmapType
	{
		Close,
		Maximize,
		Minimize,
		Restore
	}
}

using System.ComponentModel;

using NovaUI.NetCore.Helpers.LibMain;

namespace NovaUI.NetCore.Controls
{
	[ToolboxBitmap(typeof(Label))]
	[DefaultEvent("Click")]
	public class NovaLabel : Label
	{
		public NovaLabel()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;

			Font = new("Segoe UI", 9f);
			BackColor = Constants.PrimaryColor;
			ForeColor = Constants.TextColor;
		}
	}
}

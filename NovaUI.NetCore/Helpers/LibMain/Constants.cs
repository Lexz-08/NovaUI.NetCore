namespace NovaUI.NetCore.Helpers.LibMain
{
	public static class Constants
	{
		public static readonly Color PrimaryColor   = Color.FromArgb(25, 25, 25);
		public static readonly Color SecondaryColor = Color.FromArgb(50, 50, 50);
		public static readonly Color BorderColor    = Color.FromArgb(75, 75, 75);
		public static readonly Color TextColor      = Color.FromArgb(235, 235, 235);
		public static readonly Color AccentColor    = Color.DodgerBlue.BlendWidth(Color.Blue).BlendWidth(Color.SteelBlue);

		public static readonly StringFormat CenterAlign = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
		public static readonly StringFormat LeftAlign   = new() { Alignment = StringAlignment.Near,   LineAlignment = StringAlignment.Center };
		public static readonly StringFormat RightAlign  = new() { Alignment = StringAlignment.Far,    LineAlignment = StringAlignment.Center };
	}
}

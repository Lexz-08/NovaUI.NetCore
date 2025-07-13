﻿using System.ComponentModel;
using System.Diagnostics;

using NovaUI.NetCore.Helpers;
using NovaUI.NetCore.Helpers.LibMain;

namespace NovaUI.NetCore.Controls
{
	[ToolboxBitmap(typeof(LinkLabel))]
	[DefaultEvent("Click")]
	public class NovaLinkLabel : Label
	{
		private readonly SolidBrush textBrush = Color.Transparent.ToBrush();
		private readonly SolidBrush linkBrush = Color.Transparent.ToBrush();

		private string link = "https://www.google.com/";
		private Color linkColor = Constants.AccentColor;
		private bool useUserSchemeCursor = true;
		private Cursor originalCursor = Cursors.Hand;

		private readonly StringFormat textAlign = Constants.CenterAlign;
		private bool mouseHover = false;

		[Category("Property"), Description("Occurs when the value of the Link property changes.")]
		public event EventHandler? LinkChanged;

		[Category("Property"), Description("Occurs when the value of the LinkColor property changes.")]
		public event EventHandler? LinkColorChanged;

		protected virtual void OnLinkChanged(EventArgs e)
		{
			LinkChanged?.Invoke(this, e);
			Invalidate();
		}

		protected virtual void OnLinkColorChanged(EventArgs e)
		{
			LinkColorChanged?.Invoke(this, e);
			Invalidate();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Behavior"), Description("Gets or sets the link opened when the control is clicked.")]
		public string Link
		{
			get => link;
			set { link = value; OnLinkChanged(EventArgs.Empty); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance"), Description("Gets or sets the foreground color of the control when hovered over.")]
		public Color LinkColor
		{
			get => linkColor;
			set
			{
				linkColor = value;
				if (linkBrush.Color != value) linkBrush.Color = value;
				OnLinkColorChanged(EventArgs.Empty);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Behavior"), Description("Gets or sets a value indicating whether the control will use the user-selected system scheme cursor.")]
		public bool UseUserSchemeCursor
		{
			get => useUserSchemeCursor;
			set { useUserSchemeCursor = value; Invalidate(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance"), Description("Gets or sets the cursor that is displayed when the mouse pointer is over the control.")]
		public override Cursor Cursor
		{
			get => base.Cursor;
			set
			{
				base.Cursor = value;
				if (!useUserSchemeCursor) originalCursor = value;

				OnCursorChanged(EventArgs.Empty);
				Invalidate();
			}
		}

		public NovaLinkLabel()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;

			Font = new("Segoe UI", 9f);
			linkBrush = LinkColor.ToBrush();
			BackColor = Constants.PrimaryColor;
			ForeColor = Constants.TextColor;
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			mouseHover = true;
			Font = new(Font, FontStyle.Underline);
			if (useUserSchemeCursor) Win32.GetRegistryCursor(Win32.RegistryCursor.Hand, this);
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			mouseHover = false;
			Font = new(Font, FontStyle.Regular);
			Invalidate();
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);

			Process.Start(link);
		}

		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			if (textBrush.Color != ForeColor) textBrush.Color = ForeColor;
			Invalidate();
		}

		protected override void OnTextAlignChanged(EventArgs e)
		{
			base.OnTextAlignChanged(e);

			switch (TextAlign)
			{
				case ContentAlignment.TopLeft:
					textAlign.Alignment = StringAlignment.Near;
					textAlign.LineAlignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopCenter:
					textAlign.Alignment = StringAlignment.Center;
					textAlign.LineAlignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopRight:
					textAlign.Alignment = StringAlignment.Far;
					textAlign.LineAlignment = StringAlignment.Near;
					break;
				case ContentAlignment.MiddleLeft:
					textAlign.Alignment = StringAlignment.Near;
					textAlign.LineAlignment = StringAlignment.Center;
					break;
				case ContentAlignment.MiddleCenter:
					textAlign.Alignment = StringAlignment.Center;
					textAlign.LineAlignment = StringAlignment.Center;
					break;
				case ContentAlignment.MiddleRight:
					textAlign.Alignment = StringAlignment.Far;
					textAlign.LineAlignment = StringAlignment.Center;
					break;
				case ContentAlignment.BottomLeft:
					textAlign.Alignment = StringAlignment.Near;
					textAlign.LineAlignment = StringAlignment.Far;
					break;
				case ContentAlignment.BottomCenter:
					textAlign.Alignment = StringAlignment.Center;
					textAlign.LineAlignment = StringAlignment.Far;
					break;
				case ContentAlignment.BottomRight:
					textAlign.Alignment = StringAlignment.Far;
					textAlign.LineAlignment = StringAlignment.Far;
					break;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.Clear(BackColor);

			e.Graphics.DrawString(Text, Font, textBrush,
				new Rectangle(0, 0, Width, Height), textAlign);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				textBrush?.Dispose();
				linkBrush?.Dispose();
			}
		}
	}
}

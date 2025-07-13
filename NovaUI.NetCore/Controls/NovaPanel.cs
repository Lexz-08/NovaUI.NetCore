﻿using System.ComponentModel;

using NovaUI.NetCore.Helpers;
using NovaUI.NetCore.Helpers.LibMain;

namespace NovaUI.NetCore.Controls
{
	[ToolboxBitmap(typeof(Panel))]
	[DefaultEvent("Click")]
	public class NovaPanel : Panel
	{
		private readonly Pen borderPen = Color.Transparent.ToPen();
		private readonly Pen backPen = Color.Transparent.ToPen();
		private readonly SolidBrush backBrush = Color.Transparent.ToBrush();

		private Color borderColor = Constants.BorderColor;
		private int borderWidth = 1;
		private int borderRadius = 6;

		[Category("Property"), Description("Occurs when the value of the BorderColor property changes.")]
		public event EventHandler? BorderColorChanged;

		[Category("Property"), Description("Occurs when the value of the BorderWidth property changes.")]
		public event EventHandler? BorderWidthChanged;

		[Category("Property"), Description("Occurs when the value of the BorderRadius property changes.")]
		public event EventHandler? BorderRadiusChanged;

		protected virtual void OnBorderColorChanged(EventArgs e)
		{
			BorderColorChanged?.Invoke(this, e);
			Invalidate();
		}

		protected virtual void OnBorderWidthChanged(EventArgs e)
		{
			BorderWidthChanged?.Invoke(this, e);
			Invalidate();
		}

		protected virtual void OnBorderRadiusChanged(EventArgs e)
		{
			BorderRadiusChanged?.Invoke(this, e);
			Invalidate();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance"), Description("Gets or sets the border color of the control.")]
		public Color BorderColor
		{
			get => borderColor;
			set
			{
				borderColor = value;
				if (borderPen.Color != value) borderPen.Color = value;
				OnBorderColorChanged(EventArgs.Empty);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance"), Description("Gets or sets the border width of the control.")]
		public int BorderWidth
		{
			get => borderWidth;
			set
			{
				borderWidth = value;
				if (borderPen.Width != value) borderPen.Width = value;
				OnBorderWidthChanged(EventArgs.Empty);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Category("Appearance"), Description("Gets or sets the border radius of the control.")]
		public int BorderRadius
		{
			get => borderRadius;
			set { borderRadius = value; OnBorderRadiusChanged(EventArgs.Empty); }
		}

		public NovaPanel()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;

			Font = new("Segoe UI", 9f);
			borderPen = new Pen(borderColor, borderWidth);
			BackColor = Constants.PrimaryColor;
			ForeColor = Constants.TextColor;
			Size = new(250, 200);
			Region = Region.FromHrgn(Win32.CreateRoundRectRgn(0, 0, Width + 1, Height + 1, borderRadius, borderRadius));
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			Region = Region.FromHrgn(Win32.CreateRoundRectRgn(0, 0, Width + 1, Height + 1, borderRadius, borderRadius));
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			Region = Region.FromHrgn(Win32.CreateRoundRectRgn(0, 0, Width + 1, Height + 1, borderRadius, borderRadius));
		}

		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.OnParentBackColorChanged(e);
			Invalidate();
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (backBrush.Color != BackColor) backBrush.Color = BackColor;
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.Clear(Parent != null ? Parent.BackColor : Color.Transparent);

			if (borderRadius > 0)
			{
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				if (borderWidth > 0)
				{
					e.Graphics.FillPath(backBrush,
						new Rectangle(borderWidth - 1, borderWidth - 1, Width - (borderWidth * 2) + 1, Height - (borderWidth * 2) + 1)
						.Rescale(DesignMode ? 2 : 0, DesignMode ? -4 : 0).Round(Math.Max(1, borderRadius - borderWidth)));
					e.Graphics.DrawPath(borderPen,
						new RectangleF(borderWidth / 2f, borderWidth / 2f, Width - borderWidth - 1, Height - borderWidth - 1)
						.Rescale(DesignMode ? 2 : 0, DesignMode ? -4 : 0).Round(borderRadius));
				}
				else
				{
					e.Graphics.FillPath(backBrush,
						new Rectangle(0, 0, Width - 1, Height - 1).Round(borderRadius));
					e.Graphics.DrawPath(backPen,
						new Rectangle(0, 0, Width - 1, Height - 1).Round(borderRadius));
				}
			}
			else
			{
				if (borderWidth > 0)
				{
					e.Graphics.FillRectangle(backBrush,
						new Rectangle(borderWidth, borderWidth, Width - (borderWidth * 2), Height - (borderWidth * 2))
						.Rescale(DesignMode ? 2 : 0, DesignMode ? -4 : 0));
					e.Graphics.DrawRectangle(backPen,
						new RectangleF(borderWidth / 2f, borderWidth / 2f, Width - borderWidth - 1, Height - borderWidth - 1)
						.Rescale(DesignMode ? 2 : 0, DesignMode ? -4 : 0));
				}
				else e.Graphics.FillRectangle(backBrush, new Rectangle(0, 0, Width, Height));
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				borderPen?.Dispose();
				backPen?.Dispose();
				backBrush?.Dispose();
			}
		}
	}
}

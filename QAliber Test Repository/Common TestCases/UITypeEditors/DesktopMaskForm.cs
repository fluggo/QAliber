using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QAliber.Repository.CommonTestCases.UITypeEditors
{
	public partial class DesktopMaskForm : Form
	{
		public DesktopMaskForm()
		{
			InitializeComponent();
			int width = 0;
			foreach (Screen scr in Screen.AllScreens)
			{
				width += scr.Bounds.Width;
			}
			Size = new Size(width, Screen.PrimaryScreen.Bounds.Height);
			
		}

		public string ImageFile
		{
			get
			{
				return imageFile;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			string text = "Select the image area by dragging the mouse, when you're done, right click the mouse or press Escape";
			SizeF size = e.Graphics.MeasureString(text, Font);
			e.Graphics.DrawString(text, 
				Font, Brushes.White, new PointF(Width / 2 - size.Width / 2, 100));
			if (isMouseDown)
			{
				e.Graphics.DrawRectangle(new Pen(Color.Red, 4f), rect);
			}
			base.OnPaint(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = true;
				startPoint = new Point(e.X, e.Y);
			}
			if (e.Button == MouseButtons.Right)
			{
				try
				{
					SaveImage();
					Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error Saving Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (isMouseDown)
			{
				endPoint = new Point(e.X, e.Y);
				rect = new Rectangle(
					Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y),
					Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));
				Refresh();
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = false;
			}
			base.OnMouseUp(e);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				try
				{
					SaveImage();
					Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error Saving Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			base.OnKeyUp(e);
		}
		private void SaveImage()
		{
			DialogResult dr = saveFileDialog.ShowDialog();
			if (dr == DialogResult.OK)
			{
				Visible = false;
				Bitmap desktop = Logger.Slideshow.ScreenCapturer.Capture(false);
				Bitmap image = desktop.Clone(rect, desktop.PixelFormat);
				image.Save(saveFileDialog.FileName);
				imageFile = saveFileDialog.FileName;
				Visible = true;
			}
		}

		Rectangle rect = new Rectangle();
		Point endPoint = new Point();
		Point startPoint = new Point();
		bool isMouseDown = false;
		string imageFile = string.Empty;
	}
}

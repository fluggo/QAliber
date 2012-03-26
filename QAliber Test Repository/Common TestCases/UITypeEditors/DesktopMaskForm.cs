/*	  
 * Copyright (C) 2010 QAlibers (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 */
 
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

			_desktop = Logger.Slideshow.ScreenCapturer.Capture( false );
		}

		public string ImageFile
		{
			get
			{
				return saveFileDialog.FileName;
			}
			set {
				try {
					saveFileDialog.FileName = value;
				}
				catch {}
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
				e.Graphics.DrawRectangle(new Pen(Color.Red, 1f), rect);
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
				SaveImage();
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
				SaveImage();
			}
			base.OnKeyUp(e);
		}
		private void SaveImage()
		{
			if( rect.IsEmpty ) {
				DialogResult = DialogResult.Cancel;
				return;
			}

			DialogResult dr = saveFileDialog.ShowDialog();
			if (dr == DialogResult.OK)
			{
				try {
					Visible = false;
					Bitmap image = _desktop.Clone( rect, _desktop.PixelFormat );
					image.Save(saveFileDialog.FileName);
				}
				catch( Exception ex ) {
					MessageBox.Show( this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop );
					DialogResult = DialogResult.Cancel;
				}
			}

			DialogResult = dr;
		}

		Rectangle rect = new Rectangle();
		Point endPoint = new Point();
		Point startPoint = new Point();
		bool isMouseDown = false;
		Bitmap _desktop = null;
	}
}

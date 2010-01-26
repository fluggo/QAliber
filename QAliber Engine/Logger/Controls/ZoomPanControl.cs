using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace QAliber.Logger.Controls
{
	public class ZoomPanControl : PictureBox
	{

		public ZoomPanControl()
		{
			InitializeComponent();
			ContextMenuStrip = contextMenuStrip;
		}

		public float Zoom
		{
			get { return zoomFactor; }
			set { zoomFactor = value; }
		}

		public static float LastZoom
		{
			get { return lastZoom; }
		}

		public static Point LastPan
		{
			get { return lastPan; }
		}

		public string ImageFile
		{
			get { return imageFile; }
			set 
			{ 
				imageFile = value;
				if (imageFile != null && File.Exists(imageFile))
				{
					image = Image.FromFile(imageFile);
				}
				else
					image = null;
				Refresh();
			}
		}

		public Point PanLocation
		{
			get { return panLocation; }
			set { panLocation = value; }
		}

		public static ZoomPanActionType Action
		{
			get { return currentAction; }
			set { currentAction = value; }
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (startMousePos.X == 0 && startMousePos.Y == 0)
					startMousePos = new Point(e.X, e.Y);
			}
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				startMousePos = new Point(0, 0);
				lastZoomFactor = zoomFactor;
				lastPanLocation = panLocation;
			}
			base.OnMouseUp(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				switch (currentAction)
				{
					case ZoomPanActionType.Zoom:
						zoomFactor = lastZoomFactor + (e.Y - startMousePos.Y) * 4 / 300f;
						zoomFactor = zoomFactor > 4 ? 4 : zoomFactor;
						zoomFactor = zoomFactor < 0.25f ? 0.25f : zoomFactor;
						break;
					case ZoomPanActionType.Pan:
						panLocation = new Point(lastPanLocation.X + e.X - startMousePos.X, lastPanLocation.Y + e.Y - startMousePos.Y);
						break;
					case ZoomPanActionType.None:
						break;
					default:
						break;
				}
				
				Refresh();
			}
			base.OnMouseMove(e);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			if (image != null)
			{
				g.PageUnit = GraphicsUnit.Pixel;
				g.ResetTransform();
				//g.DrawRectangle(new Pen(Color.Red, 2f), 2, 2, g.ClipBounds.Width - 4, g.ClipBounds.Height - 4);
				
				g.ScaleTransform(zoomFactor, zoomFactor);
				g.DrawImage(image, panLocation.X, panLocation.Y);
				
			}
			base.OnPaint(pe);
		}

		private void resetMenuItem_Click(object sender, EventArgs e)
		{
			if (image != null)
			{
				panLocation = new Point(0, 0);
				float xFactor = (float)Width / image.Width;
				float yFactor = (float)Height / image.Height;
				zoomFactor = xFactor < yFactor ? xFactor : yFactor;
				Refresh();
			}
		}

		private void resetHalfMenuItem_Click(object sender, EventArgs e)
		{
			if (image != null)
			{
				panLocation = new Point(0, 0);
				float xFactor = (float)Width / (image.Width / 2);
				float yFactor = (float)Height / image.Height;
				zoomFactor = xFactor < yFactor ? xFactor : yFactor;
				Refresh();
			}
		}

		private void showFileMenuItem_Click(object sender, EventArgs e)
		{
			if (File.Exists(imageFile))
				Process.Start("explorer", "/select, \"" + imageFile + "\""); 
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.resetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetHalfMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.resetMenuItem,
			this.resetHalfMenuItem,
			this.showFileMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip1";
			this.contextMenuStrip.Size = new System.Drawing.Size(174, 70);
			// 
			// resetMenuItem
			// 
			this.resetMenuItem.Name = "resetMenuItem";
			this.resetMenuItem.Size = new System.Drawing.Size(173, 22);
			this.resetMenuItem.Text = "Fit Image To Box";
			this.resetMenuItem.Click += new System.EventHandler(this.resetMenuItem_Click);
			// 
			// resetHalfMenuItem
			// 
			this.resetHalfMenuItem.Name = "resetHalfMenuItem";
			this.resetHalfMenuItem.Size = new System.Drawing.Size(173, 22);
			this.resetHalfMenuItem.Text = "Fit Half Image";
			this.resetHalfMenuItem.Click += new System.EventHandler(this.resetHalfMenuItem_Click);
			// 
			// showFileMenuItem
			// 
			this.showFileMenuItem.Name = "showFileMenuItem";
			this.showFileMenuItem.Size = new System.Drawing.Size(173, 22);
			this.showFileMenuItem.Text = "Show File in Explorer";
			this.showFileMenuItem.Click += new EventHandler(showFileMenuItem_Click);
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		private ToolStripMenuItem resetMenuItem;
		private ToolStripMenuItem resetHalfMenuItem;
		private ToolStripMenuItem showFileMenuItem;

		private string imageFile;
		private Image image;

		private Point startMousePos = new Point(0, 0);
		private float zoomFactor = 0.7f;
		private float lastZoomFactor = 0.7f;
		private Point lastPanLocation = new Point(0, 0);
		private Point panLocation = new Point(0, 0);
		private ContextMenuStrip contextMenuStrip;
		private System.ComponentModel.IContainer components;
		
		private static ZoomPanActionType currentAction = ZoomPanActionType.None;
		private static float lastZoom = 1f;
		private static Point lastPan = new Point(0, 0);

		
		
		

	
	}

	public enum ZoomPanActionType
	{
		Zoom,
		Pan,
		None
	}

 
}

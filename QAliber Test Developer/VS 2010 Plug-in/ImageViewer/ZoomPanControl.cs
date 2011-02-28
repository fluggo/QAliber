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
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using EnvDTE80;
using QAliber.VS2005.Plugin.Commands;

namespace QAliber.VS2005.Plugin
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

		public Image ZoomPanImage
		{
			get { return image; }
			set
			{
				image = value;
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

		public Point GetOriginalXY(Point xy)
		{
			Point result = new Point();
			result.X = (int)(xy.X / zoomFactor - panLocation.X);
			result.Y = (int)(xy.Y / zoomFactor - panLocation.Y);
			return result;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (startMousePos.X == 0 && startMousePos.Y == 0)
					startMousePos = new Point(e.X, e.Y);
				if (currentAction == ZoomPanActionType.Selection)
				{
					selEndMousePos = selStartMousePos = e.Location;
					selPanLocation = panLocation;//new Point((int)(panLocation.X * zoomFactor), (int)(panLocation.Y * zoomFactor));
					selZoomFactor = zoomFactor;
				}
				//else
				//	  selEndMousePos = selStartMousePos = new Point(0, 0);
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
					case ZoomPanActionType.Selection:
						selEndMousePos = e.Location;
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
				
				g.ScaleTransform(zoomFactor, zoomFactor);
				g.DrawImage(image, panLocation.X, panLocation.Y);
				g.ResetTransform();
				if (selStartMousePos.X != 0 || selStartMousePos.Y != 0)
				{
					int top = selStartMousePos.Y < selEndMousePos.Y ? selStartMousePos.Y : selEndMousePos.Y;
					int left = selStartMousePos.X < selEndMousePos.X ? selStartMousePos.X : selEndMousePos.X;
					int width = Math.Abs(selEndMousePos.X - selStartMousePos.X);
					int height = Math.Abs(selEndMousePos.Y - selStartMousePos.Y);
					Rectangle box;
					
					box = new Rectangle(
					   (int)(left * zoomFactor / selZoomFactor + (panLocation.X - selPanLocation.X) * zoomFactor),
					   (int)(top  * zoomFactor / selZoomFactor + (panLocation.Y- selPanLocation.Y) * zoomFactor),
					   (int)(width * zoomFactor / selZoomFactor),
					   (int)(height * zoomFactor / selZoomFactor));
					
					Pen pen = new Pen(Brushes.Gray, 1.5f);
					pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
					g.DrawRectangle(pen, box);
				}
				
				
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

		private void generateResourceMenuItem_Click(object sender, EventArgs e)
		{
			Point origStart = GetOriginalXY(selStartMousePos);
			Point origEnd = GetOriginalXY(selEndMousePos);
			if (selStartMousePos.X != 0 || selStartMousePos.Y != 0)
			{
				int top = origStart.Y < origEnd.Y ? origStart.Y : origEnd.Y;
				int left = origStart.X < origEnd.X ? origStart.X : origEnd.X;
				int width = Math.Abs(origEnd.X - origStart.X);
				int height = Math.Abs(origEnd.Y - origStart.Y);
				Rectangle box = new Rectangle(left, top, width, height);
				Bitmap bitmap = ((Bitmap)image).Clone(box, ((Bitmap)image).PixelFormat);

				if (Statics.DTE.Solution.IsOpen)
				{
					foreach (EnvDTE.Project proj in Statics.DTE.Solution.Projects)
					{
						if (proj.Name.Contains("Test"))
						{
							try
							{
								ImageInputForm inputForm = new ImageInputForm();
								inputForm.ShowDialog();

								Microsoft.CSharp.CSharpCodeProvider codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
								string resFile = Path.GetDirectoryName(proj.FileName) + @"\Properties\Resources.resx";
								string resDesginFile = Path.GetDirectoryName(proj.FileName) + @"\Properties\Resources.Designer.cs";
								System.Resources.ResXResourceReader reader = new System.Resources.ResXResourceReader(resFile);
								using (System.Resources.ResXResourceWriter writer = new System.Resources.ResXResourceWriter(resFile + ".new"))
								{
									System.Collections.IDictionaryEnumerator iterator = reader.GetEnumerator();
									while (iterator.MoveNext())
									{
										writer.AddResource(iterator.Key.ToString(), iterator.Value);

									}
									writer.AddResource(inputForm.Input, bitmap);
									writer.Generate();
								}
								File.Copy(resFile + ".new", resFile, true);
								File.Delete(resFile + ".new");
								string[] unMatched;
								System.CodeDom.CodeCompileUnit unit = System.Resources.Tools.StronglyTypedResourceBuilder.Create(resFile, "Resources",
									proj.Properties.Item("DefaultNamespace").Value + ".Properties",
									codeProvider,
									true, out unMatched);
								using (StreamWriter designWriter = new StreamWriter(resDesginFile))
								{
									codeProvider.GenerateCodeFromCompileUnit(unit, designWriter,
									new System.CodeDom.Compiler.CodeGeneratorOptions());
								}
								MessageBox.Show("Image generation succeeded", "Resources Updated");
								return;
							}
							catch (Exception ex)
							{
								MessageBox.Show("Image generation failed\n" + ex.Message, "Resources Did Not Update");
							}
							
						}
					}
					MessageBox.Show("You need to have a project open, named *Test*", "Resources Did Not Update");
					return;
					
				}
				MessageBox.Show("You need to have a solution open with a project named *Test*", "Resources Did Not Update");
				return;
			}
			
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.resetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateResourceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.resetMenuItem,
			this.generateResourceMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip1";
			this.contextMenuStrip.Size = new System.Drawing.Size(241, 48);
			// 
			// resetMenuItem
			// 
			this.resetMenuItem.Name = "resetMenuItem";
			this.resetMenuItem.Size = new System.Drawing.Size(240, 22);
			this.resetMenuItem.Text = "Fit Image To Box";
			this.resetMenuItem.Click += new System.EventHandler(this.resetMenuItem_Click);
			// 
			// generateResourceMenuItem
			// 
			this.generateResourceMenuItem.Name = "generateResourceMenuItem";
			this.generateResourceMenuItem.Size = new System.Drawing.Size(240, 22);
			this.generateResourceMenuItem.Text = "Generate Resource From Selection";
			this.generateResourceMenuItem.Click += new System.EventHandler(this.generateResourceMenuItem_Click);
			this.contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		private ToolStripMenuItem resetMenuItem;
		
		private string imageFile;
		private Image image;

		private Point startMousePos = new Point(0, 0);
		internal Point selStartMousePos = new Point(0, 0);
		internal Point selEndMousePos = new Point(0, 0);
		private float selZoomFactor = 0;
		private float zoomFactor = 0.7f;
		private float lastZoomFactor = 0.7f;
		private Point selPanLocation = new Point(0, 0);
		private Point lastPanLocation = new Point(0, 0);
		private Point panLocation = new Point(0, 0);
		private ContextMenuStrip contextMenuStrip;
		private System.ComponentModel.IContainer components;
		
		private static ZoomPanActionType currentAction = ZoomPanActionType.None;
		private static float lastZoom = 1f;
		private ToolStripMenuItem generateResourceMenuItem;
		private static Point lastPan = new Point(0, 0);

		
		
		
		

	
	}

	public enum ZoomPanActionType
	{
		Zoom,
		Pan,
		Selection,
		None
	}

 
}

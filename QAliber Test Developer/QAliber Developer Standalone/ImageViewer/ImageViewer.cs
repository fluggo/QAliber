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
using System.IO;

namespace QAliber.VS2005.Plugin
{
	public partial class ImageViewer : Form, INotifyPropertyChanged
	{
		public ImageViewer(string codePath)
		{
			InitializeComponent();
			this.codePath = codePath;
			codeRichTextBox.Text = codePath;
			imageViewerBindingSource.DataSource = this;
		}

		public void SetImage(Image image)
		{
			zoomPanControl.ZoomPanImage = image;
			Refresh();
		}

		public bool PartialResName
		{
			get { return partResName != string.Empty; }
		}

		public bool EntireResName
		{
			get { return entireResName != string.Empty; }
		}

		public bool HasImageSelection
		{
			get { return zoomPanControl.selStartMousePos != new Point(0, 0) &&
				   zoomPanControl.selStartMousePos != zoomPanControl.selEndMousePos; }
		}

		private void CreateResourceFromImage(bool entire)
		{
			Bitmap bitmap = null;
			if (entire)
			{
				bitmap = (Bitmap)(zoomPanControl.ZoomPanImage.Clone());
			}
			else if (HasImageSelection && !entire)
			{
				bitmap = ((Bitmap)zoomPanControl.ZoomPanImage).Clone(partBox, ((Bitmap)zoomPanControl.ZoomPanImage).PixelFormat);
			}
			if (bitmap != null)
			{
				saveFileDialog.ShowDialog();
				if (!string.IsNullOrEmpty(saveFileDialog.FileName))
				{
					bitmap.Save(saveFileDialog.FileName);
					if (entire)
						entireResName = saveFileDialog.FileName;
					else
						partResName = saveFileDialog.FileName;
				}
			}
		
			
		}

		private void CreateCodeForImageCompare(bool entire)
		{
			if (Statics.Language == ProjectLanguage.VB)
				CreateCodeForImageCompareVB(entire);
			else
				CreateCodeForImageCompareCS(entire);
		}

		private void CreateCodeForReadingImage(bool entire)
		{
			if (Statics.Language == ProjectLanguage.VB)
				CreateCodeForReadingImageVB(entire);
			else
				CreateCodeForReadingImageCS(entire);
		}

		private void CreateCodeForImageCompareCS(bool entire)
		{
			string resName = "Properties.Resources.";
			resName += entire ? entireResName : partResName;
			StringBuilder code = new StringBuilder();
			code.AppendLine(string.Format("UIControlBase control = {0};" ,codePath));
			if (!entire)
			{
				code.AppendLine(string.Format("Rect box = new Rect({0}, {1}, {2}, {3});",
					partBox.Left, partBox.Top, partBox.Width, partBox.Height));
				code.AppendLine("System.Drawing.Bitmap runtimeImage = control.GetPartialImage(box);");
			}
			else
			{
				code.AppendLine("System.Drawing.Bitmap runtimeImage = control.GetImage();");
			}
			code.AppendLine(string.Format("QAliber.ImageHandling.ImageComparer imageComparer = new QAliber.ImageHandling.ImageComparer((System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile({0}), runtimeImage);", resName));
			code.AppendLine("bool compareResult = imageComparer.Compare();");

			codeRichTextBox.Text = code.ToString();

		}

		private void CreateCodeForImageCompareVB(bool entire)
		{
			string resName = vbNS + ".Resources.";
			resName += entire ? entireResName : partResName;
			StringBuilder code = new StringBuilder();
			code.AppendLine(string.Format("Dim control As UIControlBase = {0}", Recorder.RecordsDisplayer.ConvertCodePathToVB(codePath)));
			if (!entire)
			{
				code.AppendLine(string.Format("Dim box As New Rect({0}, {1}, {2}, {3})",
					partBox.Left, partBox.Top, partBox.Width, partBox.Height));
				code.AppendLine("Dim runtimeImage As System.Drawing.Bitmap = control.GetPartialImage(box)");
			}
			else
			{
				code.AppendLine("Dim runtimeImage As System.Drawing.Bitmap	= control.GetImage()");
			}
			code.AppendLine(string.Format("Dim imageComparer As New QAliber.ImageHandling.ImageComparer((System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile({0}), runtimeImage)", resName));
			code.AppendLine("Dim compareResult As Boolean = imageComparer.Compare()");
			
			codeRichTextBox.Text = code.ToString();

		}

		private void CreateCodeForReadingImageCS(bool entire)
		{
			StringBuilder code = new StringBuilder();
			code.AppendLine(string.Format("UIControlBase control = {0};", codePath));
			if (!entire)
			{
				code.AppendLine(string.Format("Rect box = new Rect({0}, {1}, {2}, {3});",
					partBox.Left, partBox.Top, partBox.Width, partBox.Height));
				code.AppendLine("System.Drawing.Bitmap runtimeImage = control.GetPartialImage(box);");
			}
			else
			{
				code.AppendLine("System.Drawing.Bitmap runtimeImage = control.GetImage();");
			}
			code.AppendLine("QAliber.ImageHandling.OCRItem ocrItem = new QAliber.ImageHandling.OCRItem(runtimeImage);");
			code.AppendLine("string ocrText = ocrItem.ProcessImage();");

			codeRichTextBox.Text = code.ToString();

		}

		private void CreateCodeForReadingImageVB(bool entire)
		{
			StringBuilder code = new StringBuilder();
			code.AppendLine(string.Format("Dim control As UIControlBase = {0}", Recorder.RecordsDisplayer.ConvertCodePathToVB(codePath)));
			if (!entire)
			{
				code.AppendLine(string.Format("Dim box As New Rect({0}, {1}, {2}, {3})",
					partBox.Left, partBox.Top, partBox.Width, partBox.Height));
				code.AppendLine("Dim runtimeImage As System.Drawing.Bitmap = control.GetPartialImage(box)");
			}
			else
			{
				code.AppendLine("Dim runtimeImage As System.Drawing.Bitmap	= control.GetImage()");
			}
			code.AppendLine("Dim ocrItem As New QAliber.ImageHandling.OCRItem(runtimeImage)");
			code.AppendLine("Dim ocrText As String = ocrItem.ProcessImage()");

			codeRichTextBox.Text = code.ToString();

		}
		
		private void toolStripSave_Click(object sender, EventArgs e)
		{
			saveFileDialog.ShowDialog();
			if (!string.IsNullOrEmpty(saveFileDialog.FileName))
			{
				zoomPanControl.ZoomPanImage.Save(saveFileDialog.FileName);
			}
		}

		private void zoomPanControl_MouseMove(object sender, MouseEventArgs e)
		{
			Point xy = zoomPanControl.GetOriginalXY(e.Location);
			toolStripLocation.Text = "X : " + xy.X + " Y : " + xy.Y;
			
		}

		private void zoomPanControl_MouseClick(object sender, MouseEventArgs e)
		{
			Point xy = zoomPanControl.GetOriginalXY(e.Location);
			string text = codePath + ".Click(MouseButtons." + e.Button + ", new Point(" + xy.X + ", " + xy.Y + "));";
			codeRichTextBox.Text = text;
		}

		private void zoomPanControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (HasImageSelection)
			{
				Point origStart = zoomPanControl.GetOriginalXY(zoomPanControl.selStartMousePos);
				Point origEnd = zoomPanControl.GetOriginalXY(zoomPanControl.selEndMousePos);
		   
				int top = origStart.Y < origEnd.Y ? origStart.Y : origEnd.Y;
				int left = origStart.X < origEnd.X ? origStart.X : origEnd.X;
				int width = Math.Abs(origEnd.X - origStart.X);
				int height = Math.Abs(origEnd.Y - origStart.Y);
				partBox = new Rectangle(left, top, width, height);
				NotifyPropertyChanged("HasImageSelection");
			}
		}

		private void zoomPicture_Click(object sender, EventArgs e)
		{
			panPicture.Checked = false;
			selectPicture.Checked = false;
			if (!zoomPicture.Checked)
			{
				ZoomPanControl.Action = ZoomPanActionType.None;
				zoomPanControl.Cursor = Cursors.Default;
			}
			else
			{
				ZoomPanControl.Action = ZoomPanActionType.Zoom;
				//zoomPanControl.Cursor = new Cursor(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "")) + @"\Resources\Zoom.cur");
			}
		}

		private void panPicture_Click(object sender, EventArgs e)
		{
			zoomPicture.Checked = false;
			selectPicture.Checked = false;
			if (!panPicture.Checked)
			{
				ZoomPanControl.Action = ZoomPanActionType.None;
				zoomPanControl.Cursor = Cursors.Default;
			}
			else
			{
				ZoomPanControl.Action = ZoomPanActionType.Pan;
				zoomPanControl.Cursor = Cursors.Hand;
			}
		}

		private void selectPicture_Click(object sender, EventArgs e)
		{
			zoomPicture.Checked = false;
			panPicture.Checked = false;
			if (!selectPicture.Checked)
			{
				ZoomPanControl.Action = ZoomPanActionType.None;
				zoomPanControl.Cursor = Cursors.Default;
			}
			else
			{
				ZoomPanControl.Action = ZoomPanActionType.Selection;
				zoomPanControl.Cursor = Cursors.Hand;
			}
		}

		private void linkCreatePartialResource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			CreateResourceFromImage(false);
		}

		private void linkCreateResourceEntire_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			CreateResourceFromImage(true);
		}

		private void linkGeenrateClickCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}

		private void linkGeneratePartialCompareCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			CreateCodeForImageCompare(false);
		}

		private void linkGenerateEntireCompareCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			CreateCodeForImageCompare(true);
		}

		private void linkGeneratePartialReadCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			CreateCodeForReadingImage(false);
		}

		private void linkGenerateEntireReadCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			CreateCodeForReadingImage(true);
		}

		private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.Clear();
			Clipboard.SetText(codeRichTextBox.Text, TextDataFormat.UnicodeText);
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}


		#endregion

		private string codePath;
		private string entireResName = string.Empty;
		private string partResName = string.Empty;
		private string vbNS = string.Empty;
		private Rectangle partBox;

	}
}
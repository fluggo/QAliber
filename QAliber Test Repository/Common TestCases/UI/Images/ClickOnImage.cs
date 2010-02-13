using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using System.Drawing;
using QAliber.ImageHandling;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	public class ClickOnImage : TestCase
	{
		public ClickOnImage()
		{
			name = "Click On Image";
			icon = Properties.Resources.Mouse;
		}

		private string file = "";

		[Category("Image")]
		[Editor(typeof(QAliber.Repository.CommonTestCases.UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DisplayName("Image File")]
		[Description("The image to look for in the desktop (the image format must be bmp)")]
		public string File
		{
			get { return file; }
			set { file = value; }
		}

		private MouseButtons button = MouseButtons.Left;

		[Category("Mouse")]
		[DisplayName("Button")]
		[Description("The mouse button to click")]
		public MouseButtons Button
		{
			get { return button; }
			set { button = value; }
		}
	
		public override void Body()
		{
			Bitmap mainImage =	QAliber.Engine.Controls.Desktop.UIA.GetImage();
			Bitmap subImage = Bitmap.FromFile(file) as Bitmap;
			ImageFinder imageFinder = new ImageFinder(mainImage, subImage);
			System.Drawing.Point p = imageFinder.Find();
			if (p.X < 0)
			{
				Log.Default.Error("Couldn't find the image within the desktop");
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			}
			else
			{
				int x = p.X + subImage.Width / 2;
				int y = p.Y + subImage.Height / 2;
				QAliber.Engine.Controls.Desktop.UIA.Click(button, new System.Windows.Point(x, y));
				actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			}
		}

		public override string Description
		{
			get
			{
				return "Looking for image to click on from " + file;
			}
			set
			{
				base.Description = value;
			}
		}

	}


}

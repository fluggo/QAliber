using System;
using System.Collections.Generic;
using System.Text;
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using System.Drawing;
using QAliber.RemotingModel;
using QAliber.ImageHandling;
using System.Diagnostics;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	public class WaitForImages : TestCase
	{
		public WaitForImages()
		{
			name = "Wait For Image";
			icon = Properties.Resources.Bitmap;
		}

		private int timeout = 10000;

		[Category("Image")]
		[Description("The timeout in miliseconds, to wait for the specified image")]
		public int Timeout 
		{
			get { return timeout; }
			set { timeout = value; }
		}

		private string file = "";

		[Category("Image")]
		[Editor(typeof(QAliber.Repository.CommonTestCases.UITypeEditors.DesktopGrabberTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DisplayName("Image File")]
		[Description("The image to look for in the desktop (the image format must be bmp)")]
		public string File
		{
			get { return file; }
			set { file = value; }
		}


		private Rect rect = new Rect();

		[Category("Image")]
		[DisplayName("Rectangle Found")]
		[Description("The region where the image was found in the desktop")]
		public Rect RectFound
		{
			get { return rect; }
		}


		public override void Body()
		{
			Bitmap mainImage = Logger.Slideshow.ScreenCapturer.Capture(false);
			Bitmap subImage = Bitmap.FromFile(file) as Bitmap;
			ImageFinder imageFinder = new ImageFinder(mainImage, subImage);
			Stopwatch watch = new Stopwatch();
			watch.Start();
			while (watch.ElapsedMilliseconds < timeout + 3000)
			{
				rect = imageFinder.Find();
				if (rect.X >= 0)
				{
					Log.Default.Info("Image was found at " + rect);
					actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
					return;
				}
				mainImage = Logger.Slideshow.ScreenCapturer.Capture(false);
				imageFinder = new ImageFinder(mainImage, subImage);
			}

			Log.Default.Error("Couldn't find the image within the desktop in the timeout given");
			actualResult = QAliber.RemotingModel.TestCaseResult.Failed;

		}

		public override string Description
		{
			get
			{
				return "Waiting for image from file '" + file + "' for " + timeout + " miliseconds";
			}
			set
			{
				base.Description = value;
			}
		}

	}


}

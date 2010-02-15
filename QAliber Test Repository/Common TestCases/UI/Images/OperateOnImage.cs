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
using QAliber.Engine;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	public abstract class OperateOnImage : TestCase
	{
		protected OperateOnImage()
		{
		}

		protected string file = "";

		[Category("Image")]
		[Editor(typeof(QAliber.Repository.CommonTestCases.UITypeEditors.DesktopGrabberTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DisplayName("Image File")]
		[Description("The image to look for in the desktop (the image format must be bmp)")]
		public string File
		{
			get { return file; }
			set { file = value; }
		}

		

		public override void Body()
		{
			Bitmap mainImage = Logger.Slideshow.ScreenCapturer.Capture(false);
			Bitmap subImage = Bitmap.FromFile(file) as Bitmap;
			ImageFinder imageFinder = new ImageFinder(mainImage, subImage);
			System.Windows.Rect r = imageFinder.Find();
			if (r.X < 0)
			{
				Log.Default.Error("Couldn't find the image within the desktop");
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
			}
			else
			{
				int x = (int)(r.X + r.Width / 2);
				int y = (int)(r.Y + r.Height / 2);
				switch (actionType)
				{
					case ControlActionType.MoveMouse:
						QAliber.Engine.Controls.Desktop.UIA.MoveMouseTo(new System.Windows.Point(x, y));
						break;
					case ControlActionType.Click:
						QAliber.Engine.Controls.Desktop.UIA.Click(button, new System.Windows.Point(x, y));
						break;
					case ControlActionType.DoubleClick:
						QAliber.Engine.Controls.Desktop.UIA.DoubleClick(button, new System.Windows.Point(x, y));
						break;
					case ControlActionType.Drag:
						QAliber.Engine.Controls.Desktop.UIA.Drag(button, new System.Windows.Point(x, y), new System.Windows.Point(x + dragOffset.Width, y + dragOffset.Height));
						break;
					case ControlActionType.Write:
						QAliber.Engine.Controls.Desktop.UIA.Click(MouseButtons.Left, new System.Windows.Point(x, y));
						QAliber.Engine.Controls.Desktop.UIA.Write(keys);
						break;
					default:
						throw new ArgumentException("Can't understand action " + actionType);
				}
				
				actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			}
		}

		public override string Description
		{
			get
			{
				return string.Format("Looking for image to {0} on from file '{1}'", actionType, file);
			}
			set
			{
				base.Description = value;
			}
		}

		protected ControlActionType actionType;
		protected MouseButtons button;
		protected string keys;
		protected System.Drawing.Size dragOffset;

	}


}

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
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using System.Drawing;
using QAliber.ImageHandling;
using QAliber.Engine;
using System.Xml.Serialization;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	[XmlType("OperateOnImage", Namespace=Util.XmlNamespace)]
	public abstract class OperateOnImage : TestCase
	{
		protected OperateOnImage( string name ) : base( name )
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

		

		public override void Body( TestRun run )
		{
			Bitmap mainImage = Logger.Slideshow.ScreenCapturer.Capture(false);
			Bitmap subImage = Bitmap.FromFile(file) as Bitmap;
			ImageFinder imageFinder = new ImageFinder(mainImage, subImage);

			Rectangle r;
			double correlation = imageFinder.Find( out r );

			if( correlation < 0.85 ) {
				LogFailedByExpectedResult("Couldn't find the image within the desktop", "");
				ActualResult = TestCaseResult.Failed;
			}
			else
			{
				int x = r.X + r.Width / 2;
				int y = r.Y + r.Height / 2;
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

				ActualResult = TestCaseResult.Passed;
			}
		}

		public override string Description
		{
			get
			{
				return string.Format("Looking for image to {0} on from file '{1}'", actionType, file);
			}
		}

		protected ControlActionType actionType;
		protected MouseButtons button;
		protected string keys;
		protected System.Drawing.Size dragOffset;

	}


}

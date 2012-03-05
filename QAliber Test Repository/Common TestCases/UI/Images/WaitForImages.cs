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
using QAliber.RemotingModel;
using QAliber.ImageHandling;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	[XmlType("WaitForImages", Namespace=Util.XmlNamespace)]
	public class WaitForImages : TestCase
	{
		public WaitForImages() : base( "Wait for Image" )
		{
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
		[Description("The image to look for in the desktop. The image can be BMP, GIF, JPEG, PNG, or TIFF.")]
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
			// Fix up a relative path
			string path = Path.Combine( Path.GetDirectoryName( Scenario.Filename ), file );

			if( !System.IO.File.Exists( path ) ) {
				Log.Default.Error( "Could not find image file",
					"Could not find image file at " + path, EntryVerbosity.Normal );
				ActualResult = TestCaseResult.Failed;
				return;
			}

			Bitmap mainImage = Logger.Slideshow.ScreenCapturer.Capture(false);
			Bitmap subImage = new Bitmap( Image.FromFile( path ) );
			ImageFinder imageFinder = new ImageFinder(mainImage, subImage);
			Stopwatch watch = new Stopwatch();
			watch.Start();
			while (watch.ElapsedMilliseconds < timeout + 3000)
			{
				rect = imageFinder.Find();
				if (rect.X >= 0)
				{
					LogPassedByExpectedResult("Image was found at " + rect, "");
					actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
					return;
				}
				mainImage = Logger.Slideshow.ScreenCapturer.Capture(false);
				imageFinder = new ImageFinder(mainImage, subImage);
			}

			LogFailedByExpectedResult("Couldn't find the image within the desktop in the timeout given", "");
			actualResult = QAliber.RemotingModel.TestCaseResult.Failed;

		}

		public override string Description
		{
			get
			{
				return "Waiting for image from file '" + file + "' for " + timeout + " miliseconds";
			}
		}

	}


}

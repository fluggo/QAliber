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
			Icon = Properties.Resources.Bitmap;
		}

		private string file = "";

		[Category("Behavior")]
		[Editor(typeof(QAliber.Repository.CommonTestCases.UITypeEditors.DesktopGrabberTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DisplayName("Image File")]
		[Description("The image to look for in the desktop. The image can be BMP, GIF, JPEG, PNG, or TIFF.")]
		[DefaultValue("")]
		public string File
		{
			get { return file; }
			set { file = value; }
		}

		private int timeout = 10000;

		[Category("Behavior")]
		[Description("The timeout in milliseconds, to wait for the specified image.")]
		[DefaultValue(10000)]
		public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}

		private int _correlationPercent = 85;

		[Category("Behavior")]
		[DisplayName("Match Percent")]
		[Description("How closely the image should match, in percentage.")]
		[DefaultValue(85)]
		[TypeConverter(typeof(PercentageInt32Converter))]
		public int CorrelationPercent
		{
			get { return _correlationPercent; }
			set {
				if( value < 0 || value > 100 )
					throw new ArgumentOutOfRangeException( "value", "Percentage must be between 0 and 100." );

				_correlationPercent = value;
			}
		}


		private Rectangle rect = new Rectangle();

		[Category("Results")]
		[DisplayName("Rectangle Found")]
		[Description("The region where the image was found in the desktop.")]
		[XmlIgnore]
		public Rectangle RectFound
		{
			get { return rect; }
		}


		public override void Body()
		{
			// Fix up a relative path
			string path = Path.Combine( Path.GetDirectoryName( Scenario.Filename ), file );

			if( !System.IO.File.Exists( path ) ) {
				Log.Error( "Could not find image file",
					"Could not find image file at " + path, EntryVerbosity.Normal );
				ActualResult = TestCaseResult.Failed;
				return;
			}

			Bitmap mainImage = Logger.Slideshow.ScreenCapturer.Capture(false);
			Bitmap subImage = new Bitmap( Image.FromFile( path ) );
			ImageFinder imageFinder = new ImageFinder(mainImage, subImage);

			double correlation;

			Stopwatch watch = new Stopwatch();
			watch.Start();

			do {
				correlation = imageFinder.Find( out rect );

				if( Math.Round( correlation, 4 ) + 0.00005 >= (_correlationPercent / 100.0) ) {
					LogPassedByExpectedResult( "Image was found at " + rect, string.Format( "Match percentage: {0:p}", correlation ) );
					ActualResult = TestCaseResult.Passed;
					return;
				}

				mainImage = Logger.Slideshow.ScreenCapturer.Capture(false);
				imageFinder = new ImageFinder(mainImage, subImage);
			} while( watch.ElapsedMilliseconds < timeout + 3000 );

			LogFailedByExpectedResult("Couldn't find the image within the desktop in the timeout given", "");
			Log.Info( "Best match", string.Format(
				"Best match was at {0}, but only matched {1:p} (expected {2})", rect, correlation, _correlationPercent ) );
			ActualResult = TestCaseResult.Failed;

		}

		public override string Description
		{
			get
			{
				return "Waiting for image from file '" + file + "' for " + timeout + " miliseconds";
			}
		}

		public override object Clone() {
			WaitForImages result = (WaitForImages) base.Clone();

			result.rect = new Rectangle();

			return result;
		}
	}


}

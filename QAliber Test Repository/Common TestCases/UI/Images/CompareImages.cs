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
using System.Xml.Serialization;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	[XmlType("CompareImages", Namespace=Util.XmlNamespace)]
	public class CompareImages : TestCase
	{
		public CompareImages() : base( "Compare Images" )
		{
			Icon = Properties.Resources.Bitmap;
		}

		private string file1 = "";

		[Category("Image")]
		[Editor(typeof(QAliber.Repository.CommonTestCases.UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DisplayName("1st File To Compare")]
		public string File1
		{
			get { return file1; }
			set { file1 = value; }
		}

		private string file2 = "";

		[Category("Image")]
		[Editor(typeof(QAliber.Repository.CommonTestCases.UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DisplayName("2nd File To Compare")]
		public string File2
		{
			get { return file2; }
			set { file2 = value; }
		}

		private int tolerance = 5;

		[Category("Image")]
		[DisplayName("Tolerance")]
		[Description("The amount (in percents) of allowed different pixels")]
		public int Tolerance
		{
			get { return tolerance; }
			set 
			{
				if (value < 0 || value > 100)
					throw new ArgumentException("Tolerance must be between 0 and 100");
				tolerance = value; 
			}
		}

		private bool postDifference;

		[Category("Image")]
		[DisplayName("Report Difference")]
		[Description("Should the difference be reported to the log")]
		public bool PostDifference
		{
			get { return postDifference; }
			set { postDifference = value; }
		}
	
	

	
		public override void Body()
		{
			ActualResult = TestCaseResult.Passed;
			Bitmap image1 = Bitmap.FromFile(file1) as Bitmap;
			Bitmap image2 = Bitmap.FromFile(file2) as Bitmap;
			if (postDifference)
			{
				Log.Default.Image(image1, "Image of '" + file1 + "'");
				Log.Default.Image(image2, "Image of '" + file2 + "'");
			}
			if (image1.Width != image2.Width || image1.Height != image2.Height)
			{
				Log.Default.Error("Sizes are different", image1.Size + "\n" + image2.Size);
				ActualResult = TestCaseResult.Failed;
			}
			else
			{
				double totalPixels = image1.Width * image1.Height;
				double diffPixels = 0;
				Bitmap diffImage = new Bitmap(image1.Width, image1.Height);
				for (int i = 0; i < image1.Width; i++)
				{
					for (int j = 0; j < image1.Height; j++)
					{
						Color c1 = image1.GetPixel(i, j);
						Color c2 = image2.GetPixel(i, j);
						if (c1 != c2)
							diffPixels++;
						Color diffColor = Color.FromArgb(c2.ToArgb() - c1.ToArgb());
						diffImage.SetPixel(i, j, diffColor);
					}
				}
				if (diffPixels / totalPixels > (double)tolerance / 100.0)
				{
					Log.Default.Error("Images are not equal!");
					Log.Default.Image(diffImage, "Difference Image");
					ActualResult = TestCaseResult.Failed;
				}
			}
		}

		public override string Description
		{
			get
			{
				return "Comparing images for pathes '" + file1 + "' and '" + file2 + "'";
			}
		}

	}


}

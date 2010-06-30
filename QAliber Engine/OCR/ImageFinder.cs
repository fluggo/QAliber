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
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using Emgu.CV;


namespace QAliber.ImageHandling
{
	/// <summary>
	/// This class, can help you locate an image within an image.
	/// An exact match is done, so even if 1 pixel will be different it won't find the location
	/// </summary>
	public class ImageFinder
	{
		/// <summary>
		/// Constructs the ImageFinder class
		/// </summary>
		/// <param name="main">The image to search</param>
		/// <param name="sub">The partial image to look for in the 'main' image</param>
		/// <param name="tolerance">The tolerance in percents that find will work above it</param>
		public ImageFinder(Bitmap main, Bitmap sub, int tolerance)
		{
			this.main = main;
			this.sub = sub;
			maxTolerance = (double)tolerance / 100.0;
		}

		public ImageFinder(Bitmap main, Bitmap sub) : this(main, sub, 85)
		{
			
		}

		/// <summary>
		/// Finds an image within an image
		/// <remarks>
		/// An exact match is found.
		/// Image formats should be 24bpp
		/// </remarks>
		/// </summary>
		/// <returns>(-1, -1, 0, 0) if no match was found, otherwise the rectangle of the sub image within the main image</returns>
		public System.Windows.Rect Find()
		{
			Image<Emgu.CV.Structure.Bgr, Byte> m = new Image<Emgu.CV.Structure.Bgr, Byte>(main);
			Image<Emgu.CV.Structure.Bgr, Byte> s = new Image<Emgu.CV.Structure.Bgr, Byte>(sub);
			Image<Emgu.CV.Structure.Gray, float> r = m.MatchTemplate(s, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);
			double[] min, max;
			int width = s.Width, height = s.Height;
			Point[] minLoc, maxLoc;
			r.MinMax(out min, out max, out minLoc, out maxLoc);

			m = null;
			s = null;
			r = null;

			GC.Collect();
			if (max[0] < maxTolerance)
				return new System.Windows.Rect(-1, -1, 0, 0);
			else
				return new System.Windows.Rect(maxLoc[0].X, maxLoc[0].Y, width, height);

		}


		double maxTolerance;
		Bitmap main, sub;
	}
}

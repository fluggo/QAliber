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
		public ImageFinder(Bitmap main, Bitmap sub)
		{
			this.main = main;
			this.sub = sub;
		}

		/// <summary>
		/// Finds an image within an image
		/// </summary>
		/// <param name="area">On return, the region that best matched the image.</param>
		/// <remarks>
		/// Image formats should be 24bpp.
		/// </remarks>
		/// <returns>A number between -1.0 and 1.0, where 1.0 is an exact match.</returns>
		public double Find( out Rectangle area )
		{
			using (var m = new Image<Emgu.CV.Structure.Bgr, Byte>(main))
			{
				using (var s = new Image<Emgu.CV.Structure.Bgr, Byte>(sub))
				{
					using (var r = m.MatchTemplate(s, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED))
					{
						double[] min, max;
						int width = s.Width, height = s.Height;
						Point[] minLoc, maxLoc;
						r.MinMax(out min, out max, out minLoc, out maxLoc);

						area = new Rectangle( maxLoc[0].X, maxLoc[0].Y, width, height );
						return max[0];
					}
				}
			}

			

		}


		Bitmap main, sub;
	}
}

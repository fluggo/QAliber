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
		/// <remarks>
		/// An exact match is found.
		/// Image formats should be 24bpp
		/// </remarks>
		/// </summary>
		/// <returns>(-1, -1, 0, 0) if no match was found, otherwise the rectangle of the sub image within the main image</returns>
		public System.Windows.Rect Find()
		{
			Point[] points = new Point[(main.Width - sub.Width) * (main.Height - sub.Height)];
			Stopwatch sw = new Stopwatch();
			sw.Start();
			int offset = 0;
			for (int i = 0; i < main.Width - sub.Width; i++)
			{
				offset = i * (main.Height - sub.Height);
				for (int j = 0; j < main.Height - sub.Height; j++)
				{
					points[offset + j] = new Point(i, j);
				}
			}
			List<Point> filteredList = new List<Point>();
			Color bgColor = FindBGColor();
			//Console.WriteLine("Find BG Color took : " + sw.ElapsedMilliseconds);
			BitmapData mainData = main.LockBits(
				new Rectangle(0, 0, main.Width, main.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			BitmapData subData = sub.LockBits(
				new Rectangle(0, 0, sub.Width, sub.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			unsafe
			{
				Byte* pSubBase = (Byte*)subData.Scan0.ToPointer();
				Byte* pMainBase = (Byte*)mainData.Scan0.ToPointer();
				int subLoc, mainLoc;
				for (int i = 0; i < sub.Width; i++)
				{
					for (int j = 0; j < sub.Height; j++)
					{
						//Color expVal = sub.GetPixel(i, j);
						subLoc = j * subData.Stride + i * 3;
						Color expVal = Color.FromArgb(
							(int)pSubBase[subLoc + 2], (int)pSubBase[subLoc + 1], (int)pSubBase[subLoc]);
						if (expVal != bgColor)
						{
							filteredList.Clear();
							for (int k = 0; k < points.Length; k++)
							{
								if (points[k].X >= 0)
								{
									mainLoc = (points[k].Y + j) * mainData.Stride + (points[k].X + i) * 3;
									Color actVal = Color.FromArgb(
										   (int)pMainBase[mainLoc + 2], (int)pMainBase[mainLoc + 1], (int)pMainBase[mainLoc]);
									
									if (actVal == expVal)
										filteredList.Add(points[k]);
								}
							}
							points = filteredList.ToArray();
							if (points.Length == 0)
							{
								main.UnlockBits(mainData);
								sub.UnlockBits(subData);
								return new System.Windows.Rect(-1, -1, 0, 0);
							}
							if (points.Length == 1)
							{
							   // Console.WriteLine("Find exact match took : " + sw.ElapsedMilliseconds);
								main.UnlockBits(mainData);
								sub.UnlockBits(subData);
								return new System.Windows.Rect(points[0].X, points[0].Y, sub.Width, sub.Height);
							}
						}
					}
				}
			}
			
			main.UnlockBits(mainData);
			sub.UnlockBits(subData);
			return new System.Windows.Rect(points[0].X, points[0].Y, sub.Width, sub.Height);
		}


		private Color FindBGColor()
		{
			Dictionary<Color, int> colorsTable = new Dictionary<Color, int>();
			for (int i = 0; i < sub.Width; i++)
			{
				for (int j = 0; j < sub.Height; j++)
				{
					Color c = sub.GetPixel(i, j);
					if (colorsTable.ContainsKey(c))
						colorsTable[c] = colorsTable[c] + 1;
					else
						colorsTable.Add(c, 1);
				}
			}
			Color maxColor = Color.Black;
			int maxCount = 0;
			Dictionary<Color, int>.Enumerator it = colorsTable.GetEnumerator();
			while (it.MoveNext())
			{
				if (it.Current.Value > maxCount)
				{
					maxCount = it.Current.Value;
					maxColor = it.Current.Key;
				}
			}
			return maxColor;
		}

		Bitmap main, sub;
	}
}

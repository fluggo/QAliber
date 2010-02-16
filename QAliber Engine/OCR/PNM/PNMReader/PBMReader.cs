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
using System.Drawing;
using QAliber.ImageHandling.DataReader;

namespace QAliber.ImageHandling.PNMReader
{
	/// <summary>
	/// Summary description for PBMReader.
	/// </summary>
	internal class PBMReader:IPNMReader
	{		
		#region IPNMReader Members

		public System.Drawing.Image ReadImageData(IPNMDataReader dr, int width, int height)
		{
			try
			{
				int x, y;
				byte val,WHITE =255, BLACK=0 ;
				Bitmap im;

				im = new Bitmap(width, height);
								
				for(y = 0; y < height; y++)
				{
					for(x = 0; x < width; x++)
					{					
						//writing 2D matrix of pixles
						val = dr.ReadByte();
						if(val>0)
							im.SetPixel(x,y,Color.FromArgb(WHITE,WHITE,WHITE));
						else
							im.SetPixel(x,y,Color.FromArgb(BLACK,BLACK,BLACK));
					}
				}

				return im;
			}
			catch
			{
				throw;
			}
			finally
			{
				dr.Close();
			}
		}

		#endregion
	}
}


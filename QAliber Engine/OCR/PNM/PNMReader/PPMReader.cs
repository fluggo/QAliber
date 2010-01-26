using System;
using System.Drawing;
using QAliber.ImageHandling.DataReader;

namespace QAliber.ImageHandling.PNMReader
{
	/// <summary>
	/// Summary description for PPMReader.
	/// </summary>
	internal class PPMReader:IPNMReader
	{		
		#region IPNMReader Members

		public System.Drawing.Image ReadImageData(IPNMDataReader dr, int width, int height)
		{
			try
			{
				int x, y;
				byte valR,valG,valB;
				Bitmap im;

				im = new Bitmap(width, height);
								
				for(y = 0; y < height; y++)
				{
					for(x = 0; x < width; x++)
					{					
						//writing 2D matrix of pixles
						valR = dr.ReadByte();
						valG = dr.ReadByte();
						valB = dr.ReadByte();
						im.SetPixel(x,y,Color.FromArgb(valR,valG,valB));
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


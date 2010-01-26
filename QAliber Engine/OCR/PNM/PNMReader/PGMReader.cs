using System;
using System.Drawing;
using QAliber.ImageHandling.DataReader;

namespace QAliber.ImageHandling.PNMReader
{
	/// <summary>
	/// Summary description for PGMReader.
	/// </summary>
	internal class PGMReader:IPNMReader
	{		
		#region IPNMReader Members

		public System.Drawing.Image ReadImageData(IPNMDataReader dr, int width, int height)
		{
			try
			{
				int x, y;
				byte val;
				Bitmap im;

				im = new Bitmap(width, height);
								
				for(y = 0; y < height; y++)
				{
					for(x = 0; x < width; x++)
					{					
						//writing 2D matrix of pixles
						val = dr.ReadByte();
						im.SetPixel(x,y,Color.FromArgb(val,val,val));
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


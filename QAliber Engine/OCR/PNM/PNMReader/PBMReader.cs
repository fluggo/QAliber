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


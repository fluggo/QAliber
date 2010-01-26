using System;
using System.Drawing;
using QAliber.ImageHandling.DataWriter;

namespace QAliber.ImageHandling.PNMWriter
{
	/// <summary>
	/// Summary description for PGMWriter.
	/// </summary>
	internal class PGMWriter : IPNMWriter
	{
		#region IPNMWriter Members

		public void WriteImageData(IPNMDataWriter dw, System.Drawing.Image im)
		{
			int i = 0;

			//convert im to grey scale and write to output file
			for(int y=0;y<im.Height;y++)
			{
				for(int x=0;x<im.Width;x++)
				{
					Color c=((Bitmap)im).GetPixel(x,y);
					int luma = (int)(c.R*0.3 + c.G*0.59+ c.B*0.11);
					dw.WriteByte((byte)luma);
					i++;

					//one line cannot contain more than 70 chars					
					if((dw is ASCIIDataWriter) && i>=17)
					{
						i = 0;
						dw.WriteByte((byte)'\n');
					}
				}
			}

			dw.Close();
		}

		#endregion
	}
}

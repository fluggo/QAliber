using System;
using System.Drawing;
using QAliber.ImageHandling.DataWriter;

namespace QAliber.ImageHandling.PNMWriter
{
	/// <summary>
	/// Summary description for PGMWriter.
	/// </summary>
	internal class PPMWriter : IPNMWriter
	{
		#region IPNMWriter Members

		public void WriteImageData(IPNMDataWriter dw, System.Drawing.Image im)
		{
			//convert im to grey scale and write to output file
			int i = 0;
			for(int y=0;y<im.Height;y++)
			{
				for(int x=0;x<im.Width;x++)
				{
					Color c=((Bitmap)im).GetPixel(x,y);					
					dw.WriteByte((byte)c.R);
					dw.WriteByte((byte)c.G);
					dw.WriteByte((byte)c.B);
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

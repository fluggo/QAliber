using System;
using System.Drawing;
using QAliber.ImageHandling.DataWriter;

namespace QAliber.ImageHandling.PNMWriter
{
	/// <summary>
	/// Summary description for PBMWriter.
	/// </summary>
	internal class PBMWriter : IPNMWriter
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
					int luma = (int)(c.R + c.G + c.B);

					if(luma>0)
						dw.WriteByte((byte)1);
					else
						dw.WriteByte((byte)0);

					i++;

					//one line cannot contain more than 70 chars										
					if(i>=34)
					{
						i = 0;
						dw.WriteLine(string.Empty);
					}
				}
			}

			dw.Close();
		}

		#endregion
	}
}

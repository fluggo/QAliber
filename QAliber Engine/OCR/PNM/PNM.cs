using System;
using System.IO;
using QAliber.ImageHandling.DataReader;
using QAliber.ImageHandling.DataWriter;
using QAliber.ImageHandling.PNMWriter;
using QAliber.ImageHandling.PNMReader;

namespace QAliber.ImageHandling
{
	/// <summary>
	/// Summary description for PNM.
	/// </summary>
	internal class PNM
	{
		public PNM()
		{
			//
			// TODO: Add constructor logic here
			//
		}		
		public static System.Drawing.Image ReadPNM(string FilePath)
		{			
			char fchar;
			int max, width, height;
			string line, ftype;

			FileStream fs = new FileStream
				(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
		 
			/* Read first line, and test if it is propoer PNM type */	
			line = ((char)fs.ReadByte()).ToString() + ((char)fs.ReadByte()).ToString();
			ftype = line;
			//read off the endline char
			fs.ReadByte();

			PNMEncoding encoding = PNMFactory.GetPNMEncoding(ftype);
			IPNMDataReader dr = PNMFactory.GetIPNMDataReader(fs, encoding);
			
			if (dr == null) 
			{
				throw new Exception
					("Currently only Binary and ASCII encoding is supported");
			}
						
			IPNMReader imReader = PNMFactory.GetIPNMReader(PNMFactory.GetPNMType(ftype));

			if (imReader == null) 
			{
				throw new Exception
					("Currently only PBM, PGM and PNM Image Types are supported");
			}
				
			/* Read lines, ignoring those starting with Comment Character, until the				
				Image Dimensions are read. */								
			do
			{
				//read first char to determine if its a comment
				line = dr.ReadLine();
				if(line.Length==0)
					fchar = '#';
				else
					fchar = line.Substring(0,1).ToCharArray(0,1)[0];						
			}
			while(fchar == '#');
				
			string[] toks = line.Split(new char[]{' '}); 
			//read height and width
			width = int.Parse(toks[0]);
			height = int.Parse(toks[1]);
				
			if(ftype!="P1")
			{
				/* Read lines, ignoring those starting with Comment Character, until the
					maximum pixel value is read. */
				do
				{
					//read first char to determine if its a comment
					line = dr.ReadLine();
					if(line.Length==0)
						fchar = '#';
					else
						fchar = line.Substring(0,1).ToCharArray(0,1)[0];					
				}
				while(fchar == '#');

				max = int.Parse(line);

				if (! (max == 255)) 
				{
					Console.WriteLine
						("Warning, max value for pixels in this image is not 255");
				}
			}

			return imReader.ReadImageData(dr, width, height);
		}

		public static void WritePNM(string FilePath, System.Drawing.Image im)
		{
			string ext = FilePath.Substring(FilePath.Length-4,4).ToLower();
			switch(ext)
			{
				case ".pbm":
					WritePNM(FilePath, im, PNMEncoding.ASCIIEncoding, PNMType.PBM);
					break;

				case ".pgm":
					WritePNM(FilePath, im, PNMEncoding.BinaryEncoding, PNMType.PGM);
					break;

				case ".ppm":
					WritePNM(FilePath, im, PNMEncoding.BinaryEncoding, PNMType.PPM);
					break;
			}		
		}

		public static void WritePNM(string FilePath, System.Drawing.Image im, PNMEncoding encoding)
		{
			WritePNM(FilePath, im, encoding, PNMType.PGM);
		}

		public static void WritePNM(string FilePath, System.Drawing.Image im, PNMEncoding encoding, PNMType ptype)
		{
			
			FileStream fs = new FileStream(FilePath, FileMode.Create,FileAccess.Write,FileShare.None);
			
			IPNMDataWriter dw = PNMFactory.GetIPNMDataWriter(fs, encoding);

			if(dw==null)
			{
				throw new Exception
					("Currently only Binary and ASCII encoding is supported");
			}
		
			try
			{	
				//write image header
				dw.WriteLine(PNMFactory.GetMagicWord(ptype,encoding));				
				dw.WriteLine(im.Width.ToString() + " " + im.Height.ToString());
				if(ptype != PNMType.PBM )
					dw.WriteLine("255");

				IPNMWriter imWriter = PNMFactory.GetIPNMWriter(ptype);
				imWriter.WriteImageData(dw, im);	
			}
			catch{throw;}

		}

	}
}

using System;
using System.IO;
using QAliber.ImageHandling.DataReader;
using QAliber.ImageHandling.DataWriter;
using QAliber.ImageHandling.PNMReader;
using QAliber.ImageHandling.PNMWriter;

namespace QAliber.ImageHandling
{
	//Image data can be either in Binary or ASCII
	public enum PNMEncoding
	{
		BinaryEncoding,
		ASCIIEncoding
	}

	//PNM Type to enforce
	public enum PNMType
	{
		PBM, //1 bit
		PGM, //8 bit
		PPM  //24 bit
	}

	/// <summary>
	/// Summary description for PNMFactory.
	/// </summary>
	internal class PNMFactory
	{
		public static string GetMagicWord(PNMType ptype, PNMEncoding encoding)
		{
			if(ptype == PNMType.PGM && encoding == PNMEncoding.ASCIIEncoding)
				return "P2";

			if(ptype == PNMType.PGM && encoding == PNMEncoding.BinaryEncoding)
				return "P5";

			if(ptype == PNMType.PPM && encoding == PNMEncoding.ASCIIEncoding)
				return "P3";

			if(ptype == PNMType.PPM && encoding == PNMEncoding.BinaryEncoding)
				return "P6";

			if(ptype == PNMType.PBM && encoding == PNMEncoding.ASCIIEncoding)
				return "P1";

			if(ptype == PNMType.PBM && encoding == PNMEncoding.BinaryEncoding)
				throw new Exception("PBM files are only written in ASCII encoding.");
			
			return "P5";
		}

		public static PNMType GetPNMType(string MagicWord)
		{
			switch(MagicWord)
			{
				case "P1":				
					return PNMType.PBM;

				case "P2":
				case "P5":
					return PNMType.PGM;

				case "P6":
				case "P3":
					return PNMType.PPM;
			}

			return PNMType.PGM;
		}

		public static PNMEncoding GetPNMEncoding(string MagicWord)
		{
			switch(MagicWord)
			{
				case "P1":
				case "P2":
				case "P3":
					return PNMEncoding.ASCIIEncoding;
				
				case "P5":
				case "P6":				
					return PNMEncoding.BinaryEncoding;
			}

			return PNMEncoding.BinaryEncoding;
		}

		public static string GetMaxPixel(PNMType ptype)
		{
			switch(ptype)
			{
				case PNMType.PBM: return "-1";// dun put in header
			}
			return "255";
		}

		public static IPNMReader GetIPNMReader(PNMType ptype)
		{
			IPNMReader imReader = null;
			
			switch(ptype)
			{
				case PNMType.PBM:
					imReader = new PBMReader();
					break;

				case PNMType.PGM:
					imReader = new PGMReader();
					break;
				
				case PNMType.PPM:
					imReader = new PPMReader();
					break;
			}

			return imReader;
		}

		public static IPNMWriter GetIPNMWriter(PNMType ptype)
		{
			IPNMWriter imWriter = null;
			
			switch(ptype)
			{
				case PNMType.PBM:
					imWriter = new PBMWriter();
					break;

				case PNMType.PGM:
					imWriter = new PGMWriter();
					break;

				case PNMType.PPM:
					imWriter = new PPMWriter();
					break;
			}

			return imWriter;
		}

		public static IPNMDataReader GetIPNMDataReader
			(FileStream fs, PNMEncoding encoding)
		{
			IPNMDataReader dr = null;
			
			switch(encoding)
			{
				case PNMEncoding.ASCIIEncoding:
					dr = new ASCIIDataReader(fs);
					break;

				case PNMEncoding.BinaryEncoding:
					dr = new BinaryDataReader(fs);
					break;
			}

			return dr;
		}
		
		public static IPNMDataWriter GetIPNMDataWriter
			(FileStream fs, PNMEncoding encoding)
		{
			IPNMDataWriter dw = null;
			
			switch(encoding)
			{
				case PNMEncoding.ASCIIEncoding:
					dw = new ASCIIDataWriter(fs);
					break;

				case PNMEncoding.BinaryEncoding:
					dw = new BinaryDataWriter(fs);
					break;
			}

			return dw;
		}

	}
}



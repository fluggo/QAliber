using System;
using System.IO;
using System.Collections;

namespace QAliber.ImageHandling.DataReader
{
	/// <summary>
	/// Summary description for ASCIIDataReader.
	/// </summary>
	internal class ASCIIDataReader:IPNMDataReader
	{
		private char[] WhiteSpaces = new char[]{' ','\t','\r','\n'};
		private StreamReader sr; 
		private Queue toks;		

		public ASCIIDataReader(FileStream fs)
		{
			toks = new Queue();
			sr = new StreamReader((Stream)fs,System.Text.ASCIIEncoding.ASCII);
		}
		
		public string ReadLine()
		{
			return sr.ReadLine();
		}
		
		public byte ReadByte()
		{	
			if(toks!=null &&  toks.Count>0)
			{						
				return byte.Parse(toks.Dequeue().ToString());
			}

			string line  = sr.ReadLine();

			if(line == null) 
				throw new Exception("Unexpected end of file");

			string[] data =line.Split(WhiteSpaces);
	
			foreach(string str in data)
			{
				//put only non trivial tokens
				if(str.Length>0)
					toks.Enqueue(str);
			}

			//recurrsion
			return ReadByte();
		}

		public void Close()
		{
			sr.Close();
		}
	}
}

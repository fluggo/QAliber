using System;
using System.IO;
using System.Text;

namespace QAliber.ImageHandling.DataReader
{
	/// <summary>
	/// Summary description for BinaryDataReader.
	/// </summary>
	internal class BinaryDataReader:IPNMDataReader
	{
		private BinaryReader br; 
		private char EndLine = '\n';
		public BinaryDataReader(FileStream fs)
		{			
			br = new BinaryReader((Stream)fs);
		}

		public string ReadLine()
		{
			byte cur;
			StringBuilder sb = new StringBuilder();
		
			cur =(byte) br.ReadByte();
			while(cur!=EndLine)
			{
				sb.Append(((char)cur).ToString());
				cur = br.ReadByte();
			}

			return sb.ToString();
		}

		public byte ReadByte()
		{
			return br.ReadByte();
		}

		public void Close()
		{
			br.Close();
		}
	}
}


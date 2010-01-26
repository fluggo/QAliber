using System;
using System.IO;

namespace QAliber.ImageHandling.DataWriter
{
	/// <summary>
	/// Summary description for BinaryDataWriter.
	/// </summary>
	internal class BinaryDataWriter:IPNMDataWriter
	{
		private BinaryWriter bw;
		public BinaryDataWriter(FileStream fs)
		{
			bw = new BinaryWriter((Stream)fs);
		}

		#region IPNMDataWriter Members

		public void WriteLine(string line)
		{
			bw.Write(System.Text.Encoding.ASCII.GetBytes(line+"\n"));			
		}

		public void WriteByte(byte data)
		{
			bw.Write(data);
		}

		public void Close()
		{
			bw.Flush();
			bw.Close();
		}

		#endregion
	}
}

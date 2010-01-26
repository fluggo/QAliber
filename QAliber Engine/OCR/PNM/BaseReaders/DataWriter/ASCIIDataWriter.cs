using System;
using System.IO;

namespace QAliber.ImageHandling.DataWriter
{
	/// <summary>
	/// Summary description for ASCIIDataWriter.
	/// </summary>
	internal class ASCIIDataWriter:IPNMDataWriter
	{
		private StreamWriter sw;
		public ASCIIDataWriter(FileStream fs)
		{
			sw = new StreamWriter((Stream)fs);
		}

		#region IPNMDataWriter Members

		public void WriteLine(string line)
		{
			sw.WriteLine(line);
		}

		public void WriteByte(byte data)
		{
			sw.Write(data.ToString()+" ");
		}

		public void Close()
		{
			sw.Flush();
			sw.Close();
		}

		#endregion
	}
}

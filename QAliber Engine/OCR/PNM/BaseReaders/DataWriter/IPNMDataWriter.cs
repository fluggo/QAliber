using System;

namespace QAliber.ImageHandling.DataWriter
{
	/// <summary>
	/// Summary description for IPNMDataWriter.
	/// </summary>
	internal interface IPNMDataWriter
	{
		void WriteLine(string line);
		void WriteByte(byte data);
		void Close();
	}
}

using System;
using QAliber.ImageHandling.DataWriter;

namespace QAliber.ImageHandling
{
	/// <summary>
	/// Summary description for IPNMWriter.
	/// </summary>
	internal interface IPNMWriter
	{
		void WriteImageData(IPNMDataWriter dw, System.Drawing.Image im);
	}
}

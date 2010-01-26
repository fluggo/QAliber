using System;
using QAliber.ImageHandling.DataReader;

namespace QAliber.ImageHandling
{
	/// <summary>
	/// Summary description for IPNMReader.
	/// </summary>
	internal interface IPNMReader
	{
		System.Drawing.Image ReadImageData(IPNMDataReader dr, int width, int height);
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace QAliber.ImageHandling
{
	/// <summary>
	/// Provides a mechanism to read text from bitmaps
	/// </summary>
	public class OCRItem
	{
		/// <summary>
		/// Initializes a new instance of OCRItem
		/// </summary>
		/// <param name="image">The bitmap to read from</param>
		public OCRItem(Bitmap image)
		{
			PNM.WritePNM(startPath + @"\tmp.pgm", image);
			
		}

		/// <summary>
		/// Initializes a new instance of OCRItem
		/// </summary>
		/// <param name="filename">The image file to read from</param>
		public OCRItem(string filename)
		{
			Image image = Bitmap.FromFile(filename);
			PNM.WritePNM(startPath + @"\tmp.pgm", image);

		}

		/// <summary>
		/// Do the actual OCR reading
		/// </summary>
		/// <remarks>For good results, make sure:
		/// The text is large enough
		/// The background and foreground of the text are in contrast, and each has its same color 
		/// Try to give a bitmap with less "noise" as possible
		/// </remarks>
		/// <returns>the text that was extracted from the image</returns>
		public string ProcessImage()
		{

			ProcessStartInfo gocrStartInfo = new ProcessStartInfo(ocrPath + @"\gocr048", "\"" + startPath + @"\tmp.pgm""");
			gocrStartInfo.WorkingDirectory = ocrPath;
			gocrStartInfo.RedirectStandardOutput = true;
			gocrStartInfo.UseShellExecute = false;

			Process gocrProcess = Process.Start(gocrStartInfo);
			string text = gocrProcess.StandardOutput.ReadToEnd();
			gocrProcess.WaitForExit();

			File.Delete(startPath + @"\tmp.pgm");

			return text;
		}

		private static string ocrPath
		{
			get
			{
				object val = Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\QAlibers","OCRPath", "");
				return val.ToString();
			}
		}

		private static string startPath
		{
			get
			{
				return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			}
		}

	   

		
	}
}

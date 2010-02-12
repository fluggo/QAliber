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

		/// <summary>
		/// Return the rectangle of the text found in an image
		/// </summary>
		/// <param name="textToLook">The text to llook for in the image</param>
		/// <returns>The rectangle in pixels of the text that was found (relative to the upper left corner of the image</returns>
		public Rectangle GetTextArea(string textToLook)
		{

			ProcessStartInfo gocrStartInfo = new ProcessStartInfo(ocrPath + @"\gocr048", "\"" + startPath + @"\tmp.pgm"" -r");
			gocrStartInfo.WorkingDirectory = ocrPath;
			gocrStartInfo.RedirectStandardError = true;
			gocrStartInfo.UseShellExecute = false;

			Process gocrProcess = Process.Start(gocrStartInfo);
			string line = gocrProcess.StandardError.ReadLine();
			string text = "";
			textToLook = textToLook.Replace(" ", "");
			List<Rectangle> rects = new List<Rectangle>();
			while (line != null)
			{
				string[] fields = line.Split(';');
				if (fields.Length == 2)
				{
					text += fields[0];
					string[] nums = fields[1].Trim('(', ')').Split(',');
					rects.Add(new Rectangle(
						int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]), int.Parse(nums[3])));

				}
				line = gocrProcess.StandardError.ReadLine();
			}

			gocrProcess.WaitForExit();
			File.Delete(startPath + @"\tmp.pgm");

			int index = text.IndexOf(textToLook);
			if (index >= 0)
			{
				Rectangle res = rects[index];
				for (int i = index + 1; i < index + textToLook.Length; i++)
				{
					res = Rectangle.Union(res, rects[i]);
				}
				return res;
			}

			return new Rectangle();
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

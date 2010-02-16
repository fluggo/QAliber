/*	  
 * Copyright (C) 2010 QAlibers (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 */
 
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
		public System.Windows.Rect GetTextArea(string textToLook)
		{

			ProcessStartInfo gocrStartInfo = new ProcessStartInfo(ocrPath + @"\gocr048", "\"" + startPath + @"\tmp.pgm"" -r");
			gocrStartInfo.WorkingDirectory = ocrPath;
			gocrStartInfo.RedirectStandardError = true;
			gocrStartInfo.UseShellExecute = false;

			Process gocrProcess = Process.Start(gocrStartInfo);
			string line = gocrProcess.StandardError.ReadLine();
			string text = "";
			textToLook = textToLook.Replace(" ", "");
			List<System.Windows.Rect> rects = new List<System.Windows.Rect>();
			while (line != null)
			{
				string[] fields = line.Split(';');
				if (fields.Length == 2)
				{
					text += fields[0];
					string[] nums = fields[1].Trim('(', ')').Split(',');
					rects.Add(new System.Windows.Rect(
						int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]), int.Parse(nums[3])));

				}
				line = gocrProcess.StandardError.ReadLine();
			}

			gocrProcess.WaitForExit();
			File.Delete(startPath + @"\tmp.pgm");

			int index = text.IndexOf(textToLook);
			if (index >= 0)
			{
				System.Windows.Rect res = rects[index];
				for (int i = index + 1; i < index + textToLook.Length; i++)
				{
					res = System.Windows.Rect.Union(res, rects[i]);
				}
				return res;
			}

			return new System.Windows.Rect();
		}

		private static string ocrPath
		{
			get
			{
				try
				{
					object val = Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\QAlibers", "OCRPath", "");
					return val.ToString();
				}
				catch (NullReferenceException)
				{
					return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
				}
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

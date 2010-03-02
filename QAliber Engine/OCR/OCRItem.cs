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
			imageToRead = new Bitmap(image.Width * 2, image.Height * 2);

			//use a graphics object to draw the resized image into the bitmap
			using (Graphics graphics = Graphics.FromImage(imageToRead))
			{
				//set the resize quality modes to high quality
				graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				//draw the image into the target bitmap
				graphics.DrawImage(image, 0, 0, imageToRead.Width, imageToRead.Height);
			}

			
			
		}

		/// <summary>
		/// Initializes a new instance of OCRItem
		/// </summary>
		/// <param name="filename">The image file to read from</param>
		public OCRItem(string filename) : this((Bitmap)Bitmap.FromFile(filename))
		{
			
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

			tessnet2.Tesseract ocr = new tessnet2.Tesseract();

			if (allowedChars != string.Empty)
			{
				ocr.SetVariable("tessedit_char_whitelist", allowedChars);
			}
			ocr.Init(ocrPath + @"\tessdata", "eng", false); 
			List<tessnet2.Word> result = ocr.DoOCR(imageToRead, Rectangle.Empty);
			StringBuilder builder = new StringBuilder();
			int lastLine = 1;
			foreach (tessnet2.Word word in result)
			{
				if (word.Confidence < 255.0 * (120.0 - accuracy) / 100.0)
				{
					if (lastLine < word.LineIndex)
					{
						builder.Append("\n");
						lastLine = word.LineIndex;
					}
					builder.Append(word.Text + " ");
				}
			}
			return builder.ToString();
		}

		
		/// <summary>
		/// Return the rectangle of the text found in an image
		/// </summary>
		/// <param name="textToLook">The text to llook for in the image</param>
		/// <returns>The rectangle in pixels of the text that was found (relative to the upper left corner of the image</returns>
		public System.Windows.Rect GetTextArea(string textToLook)
		{
			tessnet2.Tesseract ocr = new tessnet2.Tesseract();

			ocr.Init(ocrPath + @"\tessdata", "eng", false);
			List<tessnet2.Word> result = ocr.DoOCR(imageToRead, Rectangle.Empty);
			StringBuilder builder = new StringBuilder();
			int lastLine = 1;
			foreach (tessnet2.Word word in result)
			{
				if (lastLine < word.LineIndex)
				{
					builder.Append("\n");
					lastLine = word.LineIndex;
				}
				builder.Append(word.Text + " ");
				
			}
			string text = builder.ToString();
			int minTop = int.MaxValue, minLeft = int.MaxValue, maxBottom = 0, maxRight = 0;
			if (text.Contains(textToLook))
			{
				foreach (tessnet2.Word word in result)
				{
					if (textToLook.Contains(word.Text))
					{
						minTop = Math.Min(minTop, word.Top);
						minLeft = Math.Min(minLeft, word.Left);
						maxBottom = Math.Max(maxBottom, word.Bottom);
						maxRight = Math.Max(maxRight, word.Right);
					}
				}
			}
			return new System.Windows.Rect(minTop, minLeft, maxRight - minLeft, maxBottom - minTop);
		}

	   
		/// <summary>
		/// The accuracy in percents (0 - 100) of each word recognition (if the word is recognized below the given accuracy it will be omitted)
		/// </summary>
		public int Accuracy
		{
			get { return accuracy; }
			set { accuracy = value; }
		}

		/// <summary>
		/// The allowed chars the ocr will attempt to recognize (e.g. to recognize digits only set it to "0123456789")
		/// </summary>
		public string AllowedChars
		{
			get { return allowedChars; }
			set { allowedChars = value; }
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

		private Bitmap imageToRead;
		private int accuracy = 0;
		private string allowedChars = string.Empty;

	   

		
	}
}

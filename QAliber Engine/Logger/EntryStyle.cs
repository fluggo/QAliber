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
using System.Xml.Serialization;

namespace QAliber.Logger
{
	/// <summary>
	/// Describes the style of an entry
	/// </summary>
	[Serializable]
	public class EntryStyle
	{
		/// <summary>
		/// Constructs a new style for a log entry with default
		/// </summary>
		public EntryStyle()
		{

		}

		internal EntryStyle(EntryVerbosity verbosity)
		{
			switch (verbosity)
			{
				case EntryVerbosity.Internal:
					fgColor = Color.DarkGray;
					break;
				case EntryVerbosity.Debug:
					fgColor = Color.Blue;
					break;
				case EntryVerbosity.Normal:
					break;
				case EntryVerbosity.Critical:
					fgColor = Color.Red;
					fontStyle = FontStyle.Bold;
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Constructs a new style for a log entry
		/// </summary>
		/// <param name="fontStyle">The style of the font the log will show</param>
		/// <param name="fgColor">The foreground color of the log's text</param>
		/// <param name="bgColor">The background color of the log's text</param>
		public EntryStyle(FontStyle fontStyle, Color fgColor, Color bgColor)
		{
			this.fontStyle = fontStyle;
			this.fgColor = fgColor;
			this.bgColor = bgColor;
		}

		private FontStyle fontStyle;

		/// <summary>
		/// The font style of an entry
		/// </summary>
		public FontStyle FontStyle
		{
			get { return fontStyle; }
			set { fontStyle = value; }
		}

		private Color fgColor = Color.Black;

		/// <summary>
		/// The foreground color of an entry
		/// </summary>
		[XmlIgnore]
		public Color ForegroundColor
		{
			get { return fgColor; }
			set { fgColor = value; }
		}

		private Color bgColor = Color.Transparent;

		/// <summary>
		/// The background color of an entry
		/// </summary>
		[XmlIgnore]
		public Color BackgroundColor
		{
			get { return bgColor; }
			set { bgColor = value; }
		}

		/// <summary>
		/// An additional way to set foreground color using ARGB as integer
		/// </summary>
		public int FGColorVal
		{
			get { return fgColor.ToArgb(); }
			set { fgColor = Color.FromArgb(value); }
		}

		/// <summary>
		/// An additional way to set background color using ARGB as integer
		/// </summary>
		public int BGColorVal
		{
			get { return bgColor.ToArgb(); }
			set { bgColor = Color.FromArgb(value); }
		}
	
	
	
	
	}
}

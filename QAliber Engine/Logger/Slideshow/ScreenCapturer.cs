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
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace QAliber.Logger.Slideshow
{
	/// <summary>
	/// Allows to capture the desktop
	/// </summary>
	public class ScreenCapturer
	{
		/// <summary>
		/// Captures the desktop
		/// </summary>
		/// <returns>The bitmap of the desktop</returns>
		public static Bitmap Capture(bool withCursor)
		{
			Rectangle screen = SystemInformation.VirtualScreen;
			Bitmap bitmap = new Bitmap( screen.Width, screen.Height );

			using( Graphics g = Graphics.FromImage( bitmap ) ) {
				g.CopyFromScreen( screen.X, screen.Y, 0, 0, bitmap.Size );

				if( withCursor ) {
					Cursor currentCursor = Cursor.Current;
					Point upperLeft = new Point( Cursor.Position.X - currentCursor.HotSpot.X,
						Cursor.Position.Y - currentCursor.HotSpot.Y );

					currentCursor.Draw( g, new Rectangle( upperLeft, currentCursor.Size ) );
				}

				g.Flush();
			}

			return bitmap;
		}

		public static Bitmap Capture()
		{
			return Capture(true);
		}
	}
}

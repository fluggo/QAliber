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
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Automation;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Windows.Point;

namespace QAliber.Engine.Win32
{
	/// <summary>
	/// A managed wrapper over some p/invoke functions in GDI32.dll
	/// </summary>
	public class GDI32
	{
		[DllImport("GDI32.dll")]
		public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
		[DllImport("GDI32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
		[DllImport("GDI32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice,
		   string lpszOutput, IntPtr lpInitData);
		[DllImport("GDI32.dll")]
		public static extern bool DeleteDC(IntPtr hdc);
		[DllImport("GDI32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);
		[DllImport("GDI32.dll")]
		public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
		[DllImport("GDI32.dll")]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		/// <summary>
		/// Captures a rectangle over the desktop
		/// </summary>
		/// <param name="r">The rectangle to capture</param>
		/// <returns>The captured image</returns>
		public static Bitmap GetImage(System.Windows.Rect r)
		{
			// Retrieve the handle to a display device context for the client 
			// area of the window. 
			IntPtr window = User32.GetDesktopWindow();
			IntPtr hDC = User32.GetWindowDC(window);
			// Create a memory device context compatible with the device. 
			IntPtr hDCMem = GDI32.CreateCompatibleDC(hDC);
			// Retrieve the width and height of window display elements.
			// Create a bitmap compatible with the device associated with the 
			// device context.
			IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hDC, (int)r.Width, (int)r.Height);
			IntPtr hOld = GDI32.SelectObject(hDCMem, hBitmap);
			// bitblt over
			GDI32.BitBlt(hDCMem, 0, 0, (int)r.Width, (int)r.Height, hDC, (int)r.Left, (int)r.Top, 0x00CC0020);
			// restore selection
			GDI32.SelectObject(hDCMem, hOld);
			// clean up 
			GDI32.DeleteDC(hDCMem);
			User32.ReleaseDC(window, hDC);
			Bitmap img = Bitmap.FromHbitmap(hBitmap);

			// Delete the bitmap object and free all resources associated with it. 
			GDI32.DeleteObject(hBitmap);
			
			return img;
		}

		/// <summary>
		/// Highlights a control on the desktop with flashing red rectangle
		/// </summary>
		/// <param name="control">The control to highlight</param>
		/// <param name="time">The time in miliseconds to flash it</param>
		public static void HighlightRectangleDesktop(QAliber.Engine.Controls.UIControlBase control, int time)
		{
			try
			{
				
				System.Windows.Rect r = control.Layout;
				int startX = 2;
				int startY = 2;
				IntPtr dc = IntPtr.Zero;
				
				dc = GDI32.CreateDC("DISPLAY", null, null, IntPtr.Zero);
				startX += (int)r.Left;
				startY += (int)r.Top;
				

				using (Graphics g = Graphics.FromHdc(dc))
				{
					Pen red = new Pen(Color.Red, 4);
					Pen darkRed = new Pen(Color.DarkRed, 4);
					Stopwatch sw = new Stopwatch();
					sw.Start();
					while (sw.ElapsedMilliseconds < time)
					{
						g.DrawRectangle(red, new Rectangle(startX, startY, (int)r.Width - 4, (int)r.Height - 4));
						System.Threading.Thread.Sleep(200);
						g.DrawRectangle(darkRed, new Rectangle(startX, startY, (int)r.Width - 4, (int)r.Height - 4));
						System.Threading.Thread.Sleep(200);
					}
					sw.Stop();
					red.Dispose();
					darkRed.Dispose();
				}

				User32.InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
				User32.UpdateWindow(IntPtr.Zero);
				GDI32.DeleteDC( dc );
			}
			catch (OverflowException)
			{
				MessageBox.Show("Cannot highlight control on screen", "Control is not visible?", MessageBoxButtons.OK, MessageBoxIcon.Error);

			}
		   

		}

		/// <summary>
		/// Highlight a window by a given automation element
		/// </summary>
		/// <param name="element">The UIAutomation element to highlight</param>
		public static void HighlightWindow(AutomationElement element)
		{
			try
			{
				// This is no worse than the original code, but it still doesn't draw
				// on multiple monitors. See here for a possible solution:
				// http://stackoverflow.com/questions/576476/get-devicecontext-of-entire-screen-with-multiple-montiors

				// TODO: The following code will not necessarily work in a DPI-aware environment;
				// see http://msdn.microsoft.com/en-us/library/aa970067.aspx
				IntPtr desktop = User32.GetDesktopWindow();
				IntPtr dc = User32.GetDCEx( desktop, IntPtr.Zero, 0 );

				using( Graphics g = Graphics.FromHdc( dc ) ) {
					Rect rect = element.Current.BoundingRectangle;

					using( Pen red = new Pen( Color.Red, 4 ) ) {
						g.DrawRectangle( red, new Rectangle( (int) rect.Left + 2, (int) rect.Top + 2, (int) rect.Width - 4, (int) rect.Height - 4 ) );
					}

					g.Flush();
				}

				User32.ReleaseDC( desktop, dc );

			}
			catch 
			{

			}
		}

		/// <summary>
		/// Refresh a region over the desktop
		/// </summary>
		/// <param name="element">The element to refresh</param>
		public static void RedrawWindow(AutomationElement element)
		{
			if (element != null)
			{
				try
				{
					IntPtr handle = IntPtr.Zero;
					if (element.Current.NativeWindowHandle == 0)
					{
						AutomationElement ancestor = TreeWalker.ControlViewWalker.GetParent(element);
						while (ancestor != null && ancestor.Current.NativeWindowHandle == 0)
							ancestor = TreeWalker.ControlViewWalker.GetParent(ancestor);
						if (ancestor != null)
							handle = new IntPtr(ancestor.Current.NativeWindowHandle);
					}
					else
						handle = new IntPtr(element.Current.NativeWindowHandle);

					User32.InvalidateRect(handle, IntPtr.Zero, true);
					User32.UpdateWindow(handle);
					User32.RedrawWindow(handle, IntPtr.Zero, IntPtr.Zero, (uint)(RedrawTypes.RDW_FRAME | RedrawTypes.RDW_INVALIDATE | RedrawTypes.RDW_UPDATENOW | RedrawTypes.RDW_ALLCHILDREN | RedrawTypes.RDW_ERASENOW | RedrawTypes.RDW_INTERNALPAINT));
				}
				catch
				{
					//TODO: unhandled exception
				}
				
			}

		}



	}

	/// <summary>
	/// A managed wrapper over some p/invoke functions in User32.dll
	/// </summary>
	public class User32
	{
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("User32.dll")]
		public static extern IntPtr GetDC(IntPtr hwnd);
		[DllImport("User32.dll")]
		public static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr clipRegion, int flags);
		[DllImport("user32.dll")]
		public static extern IntPtr GetActiveWindow();
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();
		[DllImport("User32.dll")]
		public static extern IntPtr GetDesktopWindow();
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
		public static extern short GetKeyState(int keyCode);
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetKeyboardLayout(uint threadId);
		[DllImport("user32.dll")]
		public static extern IntPtr GetMessageExtraInfo();
		[DllImport("User32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref Win32Rect rectangle);
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(IntPtr hWnd);
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hwindow, out uint processId);
		[DllImport("user32.dll")]
		public static extern bool InvalidateRect(IntPtr hwnd, IntPtr lpRect, bool bErase);
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern IntPtr LoadCursorFromFile(string path);
		[DllImport("User32.dll")]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
		[DllImport("user32.dll")]
		public static extern int ReleaseCapture();
		[DllImport("user32.dll")]
		public static extern int RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);
		[DllImport("user32.dll")]
		public static extern IntPtr SetCapture(IntPtr hWnd);
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		[DllImport("user32.dll")]
		public static extern bool UpdateWindow(IntPtr hwnd);
		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(System.Drawing.Point pt);
		[DllImport("user32.dll", SetLastError=true)]
		static extern int GetWindowLong( IntPtr hWnd, int nIndex );

		/// <summary>
		/// Gets the state of the caps lock key
		/// </summary>
		public static bool CapsLocked
		{
			get
			{
				return ((ushort)GetKeyState(0x14) & 0xffff) != 0;
			}
		}

		const int WS_EX_TOPMOST = 0x8;
		const int GWL_EXSTYLE = -20;

		public static bool GetAlwaysOnTop( IntPtr windowHandle ) {
			int style = GetWindowLong( windowHandle, GWL_EXSTYLE );

			if( style == 0 )
				Marshal.ThrowExceptionForHR( Marshal.GetHRForLastWin32Error() );

			return (style & WS_EX_TOPMOST) == WS_EX_TOPMOST;
		}
	}
}


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
using System.Windows.Forms;
using System.Windows.Automation;

namespace QAliber.Engine.Win32
{
	/// <summary>
	/// A managed wrapper over some p/invoke functions in GDI32.dll
	/// </summary>
	public class GDI32
	{
		[DllImport("GDI32.dll")]
		public static extern bool BitBlt(int hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, int hdcSrc, int nXSrc, int nYSrc, int dwRop);
		[DllImport("GDI32.dll")]
		public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);
		[DllImport("GDI32.dll")]
		public static extern int CreateCompatibleDC(int hdc);
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice,
		   string lpszOutput, IntPtr lpInitData);
		[DllImport("GDI32.dll")]
		public static extern bool DeleteDC(int hdc);
		[DllImport("GDI32.dll")]
		public static extern bool DeleteObject(int hObject);
		[DllImport("GDI32.dll")]
		public static extern int GetDeviceCaps(int hdc, int nIndex);
		[DllImport("GDI32.dll")]
		public static extern int SelectObject(int hdc, int hgdiobj);

		/// <summary>
		/// Captures a rectangle over the desktop
		/// </summary>
		/// <param name="r">The rectangle to capture</param>
		/// <returns>The captured image</returns>
		public static Bitmap GetImage(System.Windows.Rect r)
		{
			// Retrieve the handle to a display device context for the client 
			// area of the window. 
			IntPtr hDC = new IntPtr(User32.GetWindowDC(User32.GetDesktopWindow()));
			// Create a memory device context compatible with the device. 
			int hDCMem = GDI32.CreateCompatibleDC(hDC.ToInt32());
			// Retrieve the width and height of window display elements.
			// Create a bitmap compatible with the device associated with the 
			// device context.
			int hBitmap = GDI32.CreateCompatibleBitmap(hDC.ToInt32(), (int)r.Width, (int)r.Height);
			int hOld = GDI32.SelectObject(hDCMem, hBitmap);
			// bitblt over
			GDI32.BitBlt(hDCMem, 0, 0, (int)r.Width, (int)r.Height, hDC.ToInt32(), (int)r.Left, (int)r.Top, 0x00CC0020);
			// restore selection
			GDI32.SelectObject(hDCMem, hOld);
			// clean up 
			GDI32.DeleteDC(hDCMem);
			User32.ReleaseDC(hDC);
			Bitmap img = Bitmap.FromHbitmap(new IntPtr(hBitmap));

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
				User32.ReleaseDC(dc);
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

				System.Windows.Rect r = element.Current.BoundingRectangle;
				int startX = 2;
				int startY = 2;
				IntPtr dc = IntPtr.Zero;
				if (element.Current.NativeWindowHandle != 0)
					dc = new IntPtr(User32.GetWindowDC(element.Current.NativeWindowHandle));
				else
				{
					dc = GDI32.CreateDC("DISPLAY", null, null, IntPtr.Zero);
					startX += (int)r.Left;
					startY += (int)r.Top;
				}

				using (Graphics g = Graphics.FromHdc(dc))
				{
					using (Pen red = new Pen(Color.Red, 4))
					{
						g.DrawRectangle(red, new Rectangle(startX, startY, (int)r.Width - 4, (int)r.Height - 4));
					}
				}
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
		[DllImport("user32.dll")]
		public static extern IntPtr GetActiveWindow();
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();
		[DllImport("User32.dll")]
		public static extern int GetDesktopWindow();
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
		public static extern short GetKeyState(int keyCode);
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetKeyboardLayout(uint threadId);
		[DllImport("user32.dll")]
		public static extern IntPtr GetMessageExtraInfo();
		[DllImport("User32.dll")]
		public static extern int GetWindowDC(int hWnd);
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
		public static extern int ReleaseDC(int hWnd, int hDC);
		[DllImport("User32.dll")]
		public static extern void ReleaseDC(IntPtr dc);
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


	}
}

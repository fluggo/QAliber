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
			int cursorX = 0, cursorY = 0;
			Graphics g;
			Rectangle r;
			int width = 0;
			foreach (Screen scr in Screen.AllScreens)
			{
				width += scr.Bounds.Width;
			}
			int height = Screen.PrimaryScreen.Bounds.Height;
			//width and height also = GDI32.GetDeviceCaps(hdcSrc, 8), GDI32.GetDeviceCaps(hdcSrc, 10)
			int hdcSrc = User32.GetWindowDC(User32.GetDesktopWindow()),
			hdcDest = GDI32.CreateCompatibleDC(hdcSrc),
			hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
			GDI32.SelectObject(hdcDest, hBitmap);
			GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, 0x00CC0020);
			//GDI32.GetDeviceCaps(hdcSrc, 8), GDI32.GetDeviceCaps(hdcSrc, 10)
			Bitmap image = Image.FromHbitmap(new IntPtr(hBitmap));
			Bitmap cursor = null;
			if (withCursor)
				cursor = CaptureCursor(ref cursorX, ref cursorY);
			Cleanup(hBitmap, hdcSrc, hdcDest);
			if (cursor != null)
			{
				r = new Rectangle(cursorX, cursorY, cursor.Width, cursor.Height);
				g = Graphics.FromImage(image);
				g.DrawImage(cursor, r);
				g.Flush();
			}
			
			return image;
		}

		public static Bitmap Capture()
		{
			return Capture(true);
		}

		private static Bitmap CaptureCursor(ref int x, ref int y)
		{
			Bitmap bmp;
			IntPtr hicon;
			User32.CURSORINFO ci = new User32.CURSORINFO();
			User32.ICONINFO icInfo;
			ci.cbSize = Marshal.SizeOf(ci);
			if (User32.GetCursorInfo(out ci))
			{
				if (ci.flags == 1)
				{
					hicon = User32.CopyIcon(ci.hCursor);
					if (User32.GetIconInfo(hicon, out icInfo))
					{
						x = ci.ptScreenPos.x - ((int)icInfo.xHotspot);
						y = ci.ptScreenPos.y - ((int)icInfo.yHotspot);

						Icon ic = Icon.FromHandle(hicon);
						bmp = ic.ToBitmap();
						return bmp;
					}
				}
			}

			return null;
		}

		private static void Cleanup(int hBitmap, int hdcSrc, int hdcDest)
		{
			GDI32.SelectObject(hdcDest, hBitmap);
			User32.ReleaseDC(User32.GetDesktopWindow(), hdcSrc);
			GDI32.DeleteDC(hdcDest);
			GDI32.DeleteObject(hBitmap);
		}
	}

	#region Helper P/Invoke Classes

	class GDI32
	{
		[DllImport("GDI32.dll")]
		public static extern bool BitBlt(int hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, int hdcSrc, int nXSrc, int nYSrc, int dwRop);
		[DllImport("GDI32.dll")]
		public static extern int CreateCompatibleBitmap(int hdc, int nWidth, int nHeight);
		[DllImport("GDI32.dll")]
		public static extern int CreateCompatibleDC(int hdc);
		[DllImport("GDI32.dll")]
		public static extern bool DeleteDC(int hdc);
		[DllImport("GDI32.dll")]
		public static extern bool DeleteObject(int hObject);
		[DllImport("GDI32.dll")]
		public static extern int GetDeviceCaps(int hdc, int nIndex);
		[DllImport("GDI32.dll")]
		public static extern int SelectObject(int hdc, int hgdiobj);

	}

	class User32
	{
		[DllImport("User32.dll")]
		public static extern int GetDesktopWindow();
		[DllImport("User32.dll")]
		public static extern int GetWindowDC(int hWnd);
		[DllImport("User32.dll")]
		public static extern int ReleaseDC(int hWnd, int hDC);
		[DllImport("user32.dll")]
		public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
		[DllImport("user32.dll", EntryPoint = "GetCursorInfo")]
		public static extern bool GetCursorInfo(out CURSORINFO pci);
		[DllImport("user32.dll", EntryPoint = "CopyIcon")]
		public static extern IntPtr CopyIcon(IntPtr hIcon);
		[DllImport("user32.dll", EntryPoint = "GetIconInfo")]
		public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);


		[StructLayout(LayoutKind.Sequential)]
		public struct LASTINPUTINFO
		{
			public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

			[MarshalAs(UnmanagedType.U4)]
			public int cbSize;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 dwTime;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CURSORINFO
		{
			public Int32 cbSize;		// Specifies the size, in bytes, of the structure. 
			public Int32 flags;			// Specifies the cursor state. This parameter can be one of the following values:
			public IntPtr hCursor;			// Handle to the cursor. 
			public POINT ptScreenPos;		// A POINT structure that receives the screen coordinates of the cursor. 
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ICONINFO
		{
			public bool fIcon;		   // Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies 
			public Int32 xHotspot;	   // Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot 
			public Int32 yHotspot;	   // Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot 
			public IntPtr hbmMask;	   // (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon, 
			public IntPtr hbmColor;    // (HBITMAP) Handle to the icon color bitmap. This member can be optional if this 
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public Int32 x;
			public Int32 y;
		}


	}
	#endregion
}

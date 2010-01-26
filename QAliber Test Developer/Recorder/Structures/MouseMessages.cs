using System;
using System.Collections.Generic;
using System.Text;

namespace QAliber.Recorder.Structures
{
	public enum MouseMessages : int
	{
		WM_XBUTTONDBLCLK = 0x020D,
		WM_XBUTTONDOWN = 0x020B,
		WM_XBUTTONUP = 0x020C,
		WM_LBUTTONDBLCLK = 0x0203,
		WM_LBUTTONDOWN = 0x0201,
		WM_LBUTTONUP = 0x0202,
		WM_MBUTTONDBLCLK = 0x0209,
		WM_MBUTTONDOWN = 0x0207,
		WM_MBUTTONUP = 0x0208,
		WM_MOUSEACTIVATE = 0x0021,
		WM_MOUSEFIRST = 0x0200,
		WM_MOUSEHOVER = 0x02A1,
		WM_MOUSELAST = 0x020A,
		WM_MOUSELEAVE = 0x02A3,
		WM_MOUSEMOVE = 0x0200,
		WM_MOUSEWHEEL = 0x020A,
		WM_MOUSEHWHEEL = 0x020E,
		WM_RBUTTONDBLCLK = 0x0206,
		WM_RBUTTONDOWN = 0x0204,
		WM_RBUTTONUP = 0x0205
	}

	public enum KBMessages
	{
		WM_SYSKEYDOWN = 0x0104,
		WM_SYSKEYUP = 0x0105,
		WM_KEYDOWN = 0x0100,
		WM_KEYFIRST = 0x0100,
		WM_KEYLAST = 0x0108,
		WM_KEYUP = 0x0101
	}

	[Flags]
	public enum KBEvents
	{
		EXTENDEDKEY = 1,
		KEYUP = 2,
		UNICODE = 4,
		SCANCODE = 8
	}

}

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
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace QAliber.Engine.Win32
{
	/// <summary>
	/// Provides interface for some low level mouse and key interactions
	/// </summary>
	public class LowLevelInput
	{
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SendInput(uint nInputs, ref Win32Input pInputs, int cbSize);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern UInt32 SendInput(UInt32 numberOfInputs, Win32Input[] inputs, Int32 sizeOfInputStructure);

		[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, Keys[] lpKeyState, StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

		[DllImport("user32.dll", ExactSpelling = true)]
		private static extern bool GetKeyboardState(Keys[] keyStates);

		
		public static string ConvertKeyToUnicode(System.Windows.Input.Key key, IntPtr inputHandle)
		{
			if (inputHandle.ToInt32() == 0)
				inputHandle = InputLanguage.CurrentInputLanguage.Handle;
			StringBuilder result = new StringBuilder(2);
			Keys[] keyStates = new Keys[256];
			if (GetKeyboardState(keyStates))
			{
				ToUnicodeEx((uint)KeyInterop.VirtualKeyFromKey(key), 0, keyStates, result, result.Capacity, 0, inputHandle);
				if (result.Length > 0)
					return result[0].ToString();
			}
			return "";
		}

		/// <summary>
		/// Moves the mouse to the requested point
		/// </summary>
		/// <param name="to">The absolute point to move the mouse to</param>
		public static void MoveMouse(Point to)
		{
			
			ManagedWinapi.InputBlocker inputBlocker = null;
			if (PlayerConfig.Default.BlockUserInput)
				inputBlocker = new ManagedWinapi.InputBlocker();
			Stopwatch watch = new Stopwatch();
			Point from = new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
			int dx = to.X > from.X ? 1 : -1;
			int dy = to.Y > from.Y ? 1 : -1;

			double a = 0;
			if (from.X == to.X)
				a = double.MaxValue;
			else
				a = (from.Y - to.Y) / (from.X - to.X);
			if (Math.Abs(a) < 1)
			{

				watch.Start();
				for (int x = (int)from.X; x != (int)to.X; x += dx)
				{
					double y = from.Y + (x - from.X) * a;
					InjectLowMouseInput(MouseEvents.MOVE, new Point(x, y));
					if (PlayerConfig.Default.AnimateMouseCursor && watch.ElapsedMilliseconds < (x - (int)from.X))
					{
						System.Threading.Thread.Sleep(1);
					}
				}
				watch.Stop();
			}
			else
			{
				watch.Start();
				for (int y = (int)from.Y; y != (int)to.Y; y += dy)
				{
					double x = from.X + (y - from.Y) / a;
					InjectLowMouseInput(MouseEvents.MOVE, new Point(x, y));
					if (PlayerConfig.Default.AnimateMouseCursor && watch.ElapsedMilliseconds < (y - (int)from.Y))
					{
						System.Threading.Thread.Sleep(1);
					}
				}
				watch.Stop();
			}
			if (PlayerConfig.Default.BlockUserInput && inputBlocker != null)
				inputBlocker.Dispose();
			System.Threading.Thread.Sleep(PlayerConfig.Default.DelayAfterAction);
		}

		
		/// <summary>
		/// Clicks the mouse
		/// </summary>
		/// <param name="button">The button to click</param>
		/// <param name="point">The absolute point (desktop top left corener = 0,0) to click on</param>
		/// <param name="delayAfter">Should the action wait for 'delay after action' configuration </param>
		public static void Click(MouseButtons button, Point point, bool delayAfter)
		{
			ManagedWinapi.InputBlocker inputBlocker = null;
			if (PlayerConfig.Default.BlockUserInput)
				inputBlocker = new ManagedWinapi.InputBlocker(); 
			if (PlayerConfig.Default.AnimateMouseCursor && (point.X != 0 || point.Y != 0))
				MoveMouse(point);
			else
			{
				InjectLowMouseInput(MouseEvents.MOVE, point);
			}
			switch (button)
			{
				case MouseButtons.Left:
					InjectLowMouseInput(MouseEvents.LEFTDOWN, point);
					System.Threading.Thread.Sleep(50);
					InjectLowMouseInput(MouseEvents.LEFTUP, point);
					break;
				case MouseButtons.Middle:
					InjectLowMouseInput(MouseEvents.MIDDLEDOWN, point);
					System.Threading.Thread.Sleep(50);
					InjectLowMouseInput(MouseEvents.MIDDLEUP, point);
					break;
				case MouseButtons.Right:
					InjectLowMouseInput(MouseEvents.RIGHTDOWN, point);
					System.Threading.Thread.Sleep(50);
					InjectLowMouseInput(MouseEvents.RIGHTUP, point);
					break;
				case MouseButtons.XButton1:
				case MouseButtons.XButton2:
					InjectLowMouseInput(MouseEvents.XDOWN, point);
					System.Threading.Thread.Sleep(50);
					InjectLowMouseInput(MouseEvents.XUP, point);
					break;
				default:
					break;
			}
			if (PlayerConfig.Default.BlockUserInput && inputBlocker != null)
				inputBlocker.Dispose();
			if (delayAfter)
				System.Threading.Thread.Sleep(PlayerConfig.Default.DelayAfterAction);
		}

		public static void Click(MouseButtons button, Point point)
		{
			Click(button, point, true);
		}

		/// <summary>
		/// Double clicks the mouse
		/// </summary>
		/// <param name="button">The button to click</param>
		/// <param name="point">The absolute point (desktop top left corener = 0,0) to click on</param>
		public static void DoubleClick(MouseButtons button, Point point)
		{
			Click(button, point, false);
			System.Threading.Thread.Sleep(200);
			Click(button, point, false);
			System.Threading.Thread.Sleep(PlayerConfig.Default.DelayAfterAction);
		}

		/// <summary>
		/// Drags the mouse in a straight line between 2 points
		/// </summary>
		/// <param name="button">The button to press while drag is made</param>
		/// <param name="from">The absolute point (desktop top left corener = 0,0) to start the drag from</param>
		/// <param name="to">The absolute point (desktop top left corener = 0,0) to end the drag</param>
		public static void DragMouse(MouseButtons button, Point from, Point to)
		{
			MoveMouse(from);
			switch (button)
			{
				case MouseButtons.Left:
					InjectLowMouseInput(MouseEvents.LEFTDOWN, from);
					MoveMouse(to);
					InjectLowMouseInput(MouseEvents.LEFTUP, to);
					break;
				case MouseButtons.Middle:
					InjectLowMouseInput(MouseEvents.MIDDLEDOWN, from);
					MoveMouse(to);
					InjectLowMouseInput(MouseEvents.MIDDLEUP, to);
					break;
				case MouseButtons.Right:
					InjectLowMouseInput(MouseEvents.RIGHTDOWN, from);
					MoveMouse(to);
					InjectLowMouseInput(MouseEvents.RIGHTUP, to);
					break;
				case MouseButtons.XButton1:
				case MouseButtons.XButton2:
					InjectLowMouseInput(MouseEvents.XDOWN, from);
					MoveMouse(to);
					InjectLowMouseInput(MouseEvents.XUP, to);
					break;
				default:
					break;
			}
			System.Threading.Thread.Sleep(PlayerConfig.Default.DelayAfterAction);

			
		}

		/// <summary>
		/// Sends key strokes to the keyboard
		/// </summary>
		/// <param name="str">A unicode string of keys</param>
		public static void SendKeystrokes(string str)
		{
			ManagedWinapi.InputBlocker inputBlocker = null;
			if (PlayerConfig.Default.BlockUserInput)
				inputBlocker = new ManagedWinapi.InputBlocker();
			bool shouldPressDown = false;
			List<char> pressedDownChars = new List<char>();
			List<Key> pressedDownKeys = new List<Key>();
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];

				switch (c)
				{
					case '{':
						Key key = ParseKeyInBraces(str, i + 1, out i);
						if (key != Key.None)
						{
							if (shouldPressDown)
							{
								pressedDownKeys.Add(key);
								PressExtendedKey(key);
							}
							else
								TapExtendedKey(key);
						}
						break;
					case '(':
						shouldPressDown = true;
						break;
					case ')':
						shouldPressDown = false;
						foreach (char pressedChar in pressedDownChars)
						{
							ReleaseKey(pressedChar);
						}
						foreach (Key pressedKey in pressedDownKeys)
						{
							ReleaseExtendedKey(pressedKey);
						}
						pressedDownChars.Clear();
						pressedDownKeys.Clear();
						break;
					default:
						if (shouldPressDown)
						{
							pressedDownChars.Add(c);
							PressAnsiKey(c);
						}
						else
							TapKey(c);
						break;
				}
				
			}
			foreach (char pressedChar in pressedDownChars)
			{
				ReleaseAnsiKey(pressedChar);
			}
			foreach (Key pressedKey in pressedDownKeys)
			{
				ReleaseExtendedKey(pressedKey);
			}
			if (PlayerConfig.Default.BlockUserInput)
				inputBlocker.Dispose();

		}

		private static void InjectLowMouseInput(MouseEvents evt, Point p)
		{
			Win32Input input = new Win32Input();
			input.type = 0;
			input.mi = new MouseInput();
			input.mi.dwExtraInfo = IntPtr.Zero;
			input.mi.mouseData = 0;
			input.mi.time = 0;
			input.mi.dx = (int)(p.X * (65535f / Screen.PrimaryScreen.Bounds.Width));
			input.mi.dy = (int)(p.Y * (65535f / Screen.PrimaryScreen.Bounds.Height));
			input.mi.dwFlags = (uint)(evt | MouseEvents.MOVE | MouseEvents.ABSOLUTE);
			SendInput(1, ref input, Marshal.SizeOf(input));
		}

		private static void PressKey(char unicodeChar)
		{
			Win32Input input = new Win32Input();
			input.type = 1;
			input.ki = new KBInput();
			input.ki.dwExtraInfo = IntPtr.Zero;
			input.ki.time = 0;
			input.ki.wVk = 0;
			input.ki.wScan = unicodeChar;
			input.ki.dwFlags = 4;
			int i = 0;
			while (SendInput(1, ref input, Marshal.SizeOf(input)) <= 0)
			{
				if (i > 40)
					throw new TimeoutException("Could not press char " + unicodeChar);
				System.Threading.Thread.Sleep(25);
				i++;
			}
			System.Windows.Forms.Application.DoEvents();
		}

		private static void PressAnsiKey(char ansiChar)
		{
			try
			{
				Key key = (Key)Enum.Parse(typeof(Key), ansiChar.ToString().ToUpper());
				Win32Input input = new Win32Input();
				input.type = 1;
				input.ki = new KBInput();
				input.ki.dwExtraInfo = IntPtr.Zero;
				input.ki.time = 0;
				input.ki.wVk = (ushort)KeyInterop.VirtualKeyFromKey(key);
				input.ki.wScan = 0;
				input.ki.dwFlags = 4;
				int i = 0;
				while (SendInput(1, ref input, Marshal.SizeOf(input)) <= 0)
				{
					if (i > 40)
						throw new TimeoutException("Could not press char " + ansiChar);
					System.Threading.Thread.Sleep(25);
					i++;
				}
				System.Windows.Forms.Application.DoEvents();
			}
			catch (InvalidCastException)
			{

			}
		}

		private static void PressExtendedKey(Key key)
		{
			Win32Input input = new Win32Input();
			input.type = 1;
			input.ki = new KBInput();
			input.ki.dwExtraInfo = IntPtr.Zero;
			input.ki.time = 0;
			input.ki.wVk = (ushort)KeyInterop.VirtualKeyFromKey(key);
			input.ki.wScan = 0;
			input.ki.dwFlags = 1;
			int i = 0;
			while (SendInput(1, ref input, Marshal.SizeOf(input)) <= 0)
			{
				if (i > 40)
					throw new TimeoutException("Could not press char " + key);
				System.Threading.Thread.Sleep(25);
				i++;
			}
			System.Windows.Forms.Application.DoEvents();
		}

		private static void ReleaseKey(char unicodeChar)
		{
			Win32Input input = new Win32Input();
			input.type = 1;
			input.ki = new KBInput();
			input.ki.dwExtraInfo = IntPtr.Zero;
			input.ki.time = 0;
			input.ki.wVk = 0;
			input.ki.wScan = unicodeChar;
			input.ki.dwFlags = 6;
			int i = 0;
			while (SendInput(1, ref input, Marshal.SizeOf(input)) <= 0)
			{
				if (i > 40)
					throw new TimeoutException("Could not press char " + unicodeChar);
				System.Threading.Thread.Sleep(25);
				i++;
			}
			System.Windows.Forms.Application.DoEvents();
		}

		private static void ReleaseAnsiKey(char ansiChar)
		{
			try
			{
				Key key = (Key)Enum.Parse(typeof(Key), ansiChar.ToString().ToUpper());
				Win32Input input = new Win32Input();
				input.type = 1;
				input.ki = new KBInput();
				input.ki.dwExtraInfo = IntPtr.Zero;
				input.ki.time = 0;
				input.ki.wVk = (ushort)KeyInterop.VirtualKeyFromKey(key);
				input.ki.wScan = 0;
				input.ki.dwFlags = 6;
				int i = 0;
				while (SendInput(1, ref input, Marshal.SizeOf(input)) <= 0)
				{
					if (i > 40)
						throw new TimeoutException("Could not press char " + ansiChar);
					System.Threading.Thread.Sleep(25);
					i++;
				}
				System.Windows.Forms.Application.DoEvents();
			}
			catch (InvalidCastException)
			{

			}
		}

		private static void ReleaseExtendedKey(Key key)
		{
			Win32Input input = new Win32Input();
			input.type = 1;
			input.ki = new KBInput();
			input.ki.dwExtraInfo = IntPtr.Zero;
			input.ki.time = 0;
			input.ki.wVk = (ushort)KeyInterop.VirtualKeyFromKey(key);
			input.ki.wScan = 0;
			input.ki.dwFlags = 3;
			int i = 0;
			while (SendInput(1, ref input, Marshal.SizeOf(typeof(Win32Input))) <= 0)
			{
				if (i > 40)
					throw new TimeoutException("Could not press char " + key);
				System.Threading.Thread.Sleep(25);
				i++;
			}
			System.Windows.Forms.Application.DoEvents();
		}

		private static void TapKey(char unicodeChar)
		{
			PressKey(unicodeChar);
			System.Threading.Thread.Sleep(15);
			ReleaseKey(unicodeChar);
			System.Threading.Thread.Sleep(50);
		}

		private static void TapExtendedKey(Key key)
		{
			PressExtendedKey(key);
			System.Threading.Thread.Sleep(15);
			ReleaseExtendedKey(key);
			System.Threading.Thread.Sleep(10);
		}

		private static Key ParseKeyInBraces(string s, int startIndex, out int endIndex)
		{
			int i = startIndex;
			string name = "";
			while (s[i] != '}')
			{
				if (i >= s.Length)
				{
					endIndex = i;
					return Key.None;
				}
				name += s[i].ToString();
				i++;
			}
			endIndex = i;
			try
			{
				Key result = (Key)Enum.Parse(typeof(Key), name, true);
				return result;
			}
			catch
			{
				return Key.None;
			}
		}


		
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct MouseInput
	{
		public int dx;
		public int dy;
		public uint mouseData;
		public uint dwFlags;
		public uint time;
		public IntPtr dwExtraInfo;
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct KBInput
	{
		public ushort wVk;
		public ushort wScan;
		public uint dwFlags;
		public uint time;
		public IntPtr dwExtraInfo;
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct HWInput
	{
		public uint uMsg;
		public ushort wParamL;
		public ushort wParamH;
	}

	[Serializable]
	[StructLayout(LayoutKind.Explicit, Size = 28)]
	public struct Win32Input
	{
		[FieldOffset(0)]
		public int type;
		[FieldOffset(4)] //*
		public MouseInput mi;
		[FieldOffset(4)] //*
		public KBInput ki;
		[FieldOffset(4)] //*
		public HWInput hi;
	}

	[Flags]
	public enum MouseEvents
	{
		MOVE = 0x0001,
		LEFTDOWN = 0x0002,
		LEFTUP = 0x0004,
		RIGHTDOWN = 0x0008,
		RIGHTUP = 0x0010,
		MIDDLEDOWN = 0x0020,
		MIDDLEUP = 0x0040,
		XDOWN = 0x0080,
		XUP = 0x100,
		VIRTUALDESK = 0x0400,
		WHEEL = 0x0800,
		ABSOLUTE = 0x8000
	}

	[Flags]
	public enum RedrawTypes
	{
		RDW_INVALIDATE = 0x0001,
		RDW_INTERNALPAINT = 0x0002,
		RDW_ERASE = 0x0004,
		RDW_VALIDATE = 0x0008,
		RDW_NOINTERNALPAINT = 0x0010,
		RDW_NOERASE = 0x0020,
		RDW_NOCHILDREN = 0x0040,
		RDW_ALLCHILDREN = 0x0080,
		RDW_UPDATENOW = 0x100,
		RDW_ERASENOW = 0x0200,
		RDW_FRAME = 0x0400,
		RDW_NOFRAME = 0x0800
	}

	[Serializable, StructLayout(LayoutKind.Sequential)]
	public struct Win32Rect
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;


		public int Width
		{
			get { return Right - Left; }
		}

		public int Height
		{
			get { return Bottom - Top; }
		}
	
	}

	
}

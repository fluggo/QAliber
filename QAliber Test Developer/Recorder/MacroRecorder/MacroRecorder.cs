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
using ManagedWinapi.Hooks;
using QAliber.Engine.Win32;
using QAliber.Recorder.Structures;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QAliber.Recorder.MacroRecorder
{
	public class MacroRecorder
	{
		public MacroRecorder()
		{
			macroEntries = new List<MacroRecordEntry>();
			watch = new Stopwatch();
			kbHook = new LowLevelKeyboardHook();
			msHook = new LowLevelMouseHook();
			kbHook.Callback += new Hook.HookCallback(HookCallback);
			msHook.Callback += new Hook.HookCallback(HookCallback);
			kbHook.KeyIntercepted += new LowLevelKeyboardHook.KeyCallback(kbHook_KeyIntercepted);
			msHook.MouseIntercepted += new LowLevelMouseHook.MouseCallback(msHook_MouseIntercepted);
		}

		public List<MacroRecordEntry> Entries
		{
			get { return macroEntries; }
		}

		public void Record()
		{
			macroEntries.Clear();
			kbHook.StartHook();
			msHook.StartHook();
			watch.Reset();
			watch.Start();
			recording = true;
		}

		public void Stop()
		{
			kbHook.Unhook();
			msHook.Unhook();
			watch.Stop();
			recording = false;
			//Removing the last 5 messages
			if (macroEntries.Count > 5)
			{
				int index = macroEntries.Count - 5;
				macroEntries.RemoveRange(index, 5);
			}

		}

		public void Play()
		{
			long lastTime = 0;
			for (int i = 0; i < macroEntries.Count; i++)
			{
				Win32Input input = macroEntries[i].Input;
				long currentTime = macroEntries[i].Time;
				System.Threading.Thread.Sleep((int)(currentTime - lastTime));
				lastTime = currentTime;
				LowLevelInput.SendInput(1, ref input, Marshal.SizeOf(typeof(Win32Input)));
			}
		}

		public void Resume()
		{
			kbHook.StartHook();
			msHook.StartHook();
			watch.Start();
		}

		public void Save(string filename)
		{
			if (!recording)
			{
				using (FileStream writer = File.Create(filename))
				{
					BinaryFormatter bf = new BinaryFormatter();
					bf.Serialize(writer, macroEntries);
				}
			}
		}

		public void Load(string filename)
		{
			if (!recording)
			{
				using (FileStream reader = File.Open(filename, FileMode.Open))
				{
					BinaryFormatter bf = new BinaryFormatter();
					macroEntries = (List<MacroRecordEntry>)bf.Deserialize(reader);
				}
			}
		}

		private void msHook_MouseIntercepted(int msg, ManagedWinapi.Windows.POINT pt, int mouseData, int flags, int time, IntPtr dwExtraInfo, ref bool handled)
		{
			MouseEvents evt = 0;
			Win32Input input = new Win32Input();
			MouseMessages mouseMessage = (MouseMessages)msg;

			input.type = 0;
			input.mi = new MouseInput();
			input.mi.dx = (int)(pt.X * (65535f / (Screen.PrimaryScreen.Bounds.Width)));
			input.mi.dy = (int)(pt.Y * (65535f / Screen.PrimaryScreen.Bounds.Height));
			input.mi.mouseData = 0;
			switch (mouseMessage)
			{
				case MouseMessages.WM_MOUSEMOVE:
					evt = MouseEvents.MOVE;
					break;
				case MouseMessages.WM_LBUTTONDOWN:
					evt = MouseEvents.LEFTDOWN;
					break;
				case MouseMessages.WM_LBUTTONUP:
					evt = MouseEvents.LEFTUP;
					break;
				case MouseMessages.WM_MBUTTONDOWN:
					evt = MouseEvents.MIDDLEDOWN;
					break;
				case MouseMessages.WM_MBUTTONUP:
					evt = MouseEvents.MIDDLEUP;
					break;
				case MouseMessages.WM_RBUTTONDOWN:
					evt = MouseEvents.RIGHTDOWN;
					break;
				case MouseMessages.WM_RBUTTONUP:
					evt = MouseEvents.RIGHTUP;
					break;
				case MouseMessages.WM_MOUSEWHEEL:
				case MouseMessages.WM_MOUSEHWHEEL:
					evt = MouseEvents.WHEEL;
					input.mi.mouseData = (uint)(mouseData >> 16);
					break;
				case MouseMessages.WM_XBUTTONDOWN:
					evt = MouseEvents.XDOWN;
					input.mi.mouseData = (uint)(mouseData >> 16);
					break;
				case MouseMessages.WM_XBUTTONUP:
					evt = MouseEvents.XUP;
					input.mi.mouseData = (uint)(mouseData >> 16);
					break;
				default:
					break;
			}
			evt |= MouseEvents.ABSOLUTE;
			input.mi.dwFlags = (uint)evt;
			input.mi.time = 0;
			input.mi.dwExtraInfo = IntPtr.Zero;

			macroEntries.Add(new MacroRecordEntry(input, watch.ElapsedMilliseconds));

		}

		private void kbHook_KeyIntercepted(int msg, int vkCode, int scanCode, int flags, int time, IntPtr dwExtraInfo, ref bool handled)
		{
			Win32Input input = new Win32Input();
			input.type = 1;
			input.ki = new KBInput();
			KBMessages kbMessage = (KBMessages)msg;
			KBEvents evt = 0;
			switch (kbMessage)
			{
				case KBMessages.WM_KEYUP:
					evt = KBEvents.KEYUP;
					break;
				default:
					break;
			}
			input.ki.dwFlags = (uint)evt;
			input.ki.time = 0;
			input.ki.wVk = (ushort)vkCode;
			input.ki.wScan = (ushort)scanCode;
			input.mi.dwExtraInfo = IntPtr.Zero;
			macroEntries.Add(new MacroRecordEntry(input, watch.ElapsedMilliseconds));
		}

		private int HookCallback(int code, IntPtr wParam, IntPtr lParam, ref bool callNext)
		{
			callNext = true;
			return 0;
		}

		private bool recording = false;
		private Stopwatch watch;
		private LowLevelKeyboardHook kbHook;
		private LowLevelMouseHook msHook;
		private List<MacroRecordEntry> macroEntries;
	}
}

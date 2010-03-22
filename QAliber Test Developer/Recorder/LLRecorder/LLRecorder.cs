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
using System.Windows.Forms;
using QAliber.Engine.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Automation;
using System.Diagnostics;
using System.Threading;
using QAliber.Engine.Controls.Web;
using QAliber.Engine.Win32;
using System.Runtime.InteropServices;
using QAliber.Engine.Controls.UIA;

namespace QAliber.Recorder
{
	public class LLRecorder
	{
		public LLRecorder()
		{
			llkbHook = new LowLevelKeyboardHook();
			llMouseHook = new LowLevelMouseHook();
			llkbHook.KeyIntercepted += new LowLevelKeyboardHook.KeyCallback(llkbHook_KeyIntercepted);
			llMouseHook.MouseIntercepted += new LowLevelMouseHook.MouseCallback(llMouseHook_MouseIntercepted);
			entries = new List<IRecordEntry>();
			
		}

		public List<IRecordEntry> Entries
		{
			get { return entries; }
			set { entries = value; }
		}

		public void Record()
		{
			if (RecorderConfig.Default.RootControl is WebRoot)
			{
				Desktop.Web.BeforeNavigationInAnyPage += new EventHandler<NavigationEventArgs>(PageBeforeNavigation);
				Desktop.Web.AfterNavigationInAnyPage += new EventHandler<NavigationEventArgs>(PageAfterNavigation);
			}

			
			
			llkbHook.StartHook();
			llMouseHook.StartHook();
			entries.Clear();
			
		}

		public void Stop()
		{
			llkbHook.Unhook();
			llMouseHook.Unhook();
			foreach (Thread thread in threads)
			{
				thread.Join();
			}
			entries.Sort(new IndexSorter());
		}

		private void CreateMouseEntry(object entryObj)
		{
			MouseEntry entry = (MouseEntry)entryObj;
			try
			{
				lock (this)
				{
					UIControlBase control = ((IControlLocator)RecorderConfig.Default.RootControl).GetControlFromPoint(new Point(entry.Point.X, entry.Point.Y));
					if (control == null)
						return;
					if (control.Process != null)
					{
						foreach (string processName in RecorderConfig.Default.FilteredProcesses)
						{
							foreach (Process process in Process.GetProcessesByName(processName))
							{
								if (process.Id == control.Process.Id)
									return;
							}

						}
					}

					Point relPoint = new Point(entry.Point.X - control.Layout.Left, entry.Point.Y - control.Layout.Top);
					entry.LLEntry.IsMouseUp = entry.IsUp;
					entry.LLEntry.AbsPoint = new Point(entry.Point.X, entry.Point.Y);
					entry.LLEntry.Button = entry.Button;
					entry.LLEntry.RelativePoint = relPoint;
					entry.LLEntry.CodePath = control.CodePath;
					entry.LLEntry.Type = control.UIType;
					entry.LLEntry.Name = control.VisibleName;
					entries.Add(entry.LLEntry);
				}

			}

			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
		
		private void CreateKBEntry(object entryObj)
		{
			KBEntry entry = (KBEntry)entryObj;
			try
			{
				lock (this)
				{
					UIControlBase control = ((IControlLocator)RecorderConfig.Default.RootControl).GetFocusedElement();
					if (control == null)
						return;
					if (control.Process != null)
					{
						foreach (string processName in RecorderConfig.Default.FilteredProcesses)
						{
							foreach (Process process in Process.GetProcessesByName(processName))
							{
								if (process.Id == control.Process.Id)
									return;
							}

						}
					}

					entry.LLEntry.IsKeyUp = entry.IsUp;
					entry.LLEntry.Key = entry.Key;
					entry.LLEntry.InputHandle = entry.InputHandle;
					entry.LLEntry.CodePath = control.CodePath;
					entry.LLEntry.Type = control.UIType;
					entry.LLEntry.Name = control.VisibleName;
					entries.Add(entry.LLEntry);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

		}

		#region Events
		private void llMouseHook_MouseIntercepted(int msg, ManagedWinapi.Windows.POINT pt, int mouseData, int flags, int time, IntPtr dwExtraInfo, ref bool handled)
		{
			int lastIndex = 0;
			switch (msg)
			{
				case (int)Structures.MouseMessages.WM_LBUTTONDOWN:
					threads.Add(new Thread(new ParameterizedThreadStart(CreateMouseEntry)));
					lastIndex = threads.Count - 1;
					threads[lastIndex].SetApartmentState(ApartmentState.STA);
					threads[lastIndex].Start(
						new MouseEntry(false, MouseButtons.Left, pt));
					threads[lastIndex].Join();
					threads.RemoveAt(lastIndex);
					break;
				case (int)Structures.MouseMessages.WM_LBUTTONUP:
					threads.Add(new Thread(new ParameterizedThreadStart(CreateMouseEntry)));
					lastIndex = threads.Count - 1;
					threads[lastIndex].SetApartmentState(ApartmentState.STA);
					threads[lastIndex].Start(
						new MouseEntry(true, MouseButtons.Left, pt));
					threads[lastIndex].Join();
					threads.RemoveAt(lastIndex);
					break;
				case (int)Structures.MouseMessages.WM_MBUTTONDOWN:
					threads.Add(new Thread(new ParameterizedThreadStart(CreateMouseEntry)));
					lastIndex = threads.Count - 1;
					threads[lastIndex].SetApartmentState(ApartmentState.STA);
					threads[lastIndex].Start(
						new MouseEntry(false, MouseButtons.Middle, pt));
					threads[lastIndex].Join();
					threads.RemoveAt(lastIndex);
					break;
				case (int)Structures.MouseMessages.WM_MBUTTONUP:
					threads.Add(new Thread(new ParameterizedThreadStart(CreateMouseEntry)));
					lastIndex = threads.Count - 1;
					threads[lastIndex].SetApartmentState(ApartmentState.STA);
					threads[lastIndex].Start(
						new MouseEntry(true, MouseButtons.Middle, pt));
					threads[lastIndex].Join();
					threads.RemoveAt(lastIndex);
					break;
				case (int)Structures.MouseMessages.WM_RBUTTONDOWN:
					threads.Add(new Thread(new ParameterizedThreadStart(CreateMouseEntry)));
					lastIndex = threads.Count - 1;
					threads[lastIndex].SetApartmentState(ApartmentState.STA);
					threads[lastIndex].Start(
						new MouseEntry(false, MouseButtons.Right, pt));
					threads[lastIndex].Join();
					threads.RemoveAt(lastIndex);
					break;
				case (int)Structures.MouseMessages.WM_RBUTTONUP:
					threads.Add(new Thread(new ParameterizedThreadStart(CreateMouseEntry)));
					lastIndex = threads.Count - 1;
					threads[lastIndex].SetApartmentState(ApartmentState.STA);
					threads[lastIndex].Start(
						new MouseEntry(true, MouseButtons.Right, pt));
					threads[lastIndex].Join();
					threads.RemoveAt(lastIndex);
					break;
				default:
					break;
			}
			
		}

		private void llkbHook_KeyIntercepted(int msg, int vkCode, int scanCode, int flags, int time, IntPtr dwExtraInfo, ref bool handled)
		{
			bool isUp = flags >> 7 == 1;
			uint pid;
			Key key = KeyInterop.KeyFromVirtualKey(vkCode);
			uint tid = User32.GetWindowThreadProcessId(User32.GetForegroundWindow(), out pid);
			IntPtr inputHandle = User32.GetKeyboardLayout(tid);
			threads.Add(new Thread(new ParameterizedThreadStart(CreateKBEntry)));
			int lastIndex = threads.Count - 1;
			threads[lastIndex].SetApartmentState(ApartmentState.STA);
			threads[lastIndex].Start(
				new KBEntry(isUp, key, inputHandle));
			threads[lastIndex].Join();
			threads.RemoveAt(lastIndex);
		}

		private void PageBeforeNavigation(object sender, NavigationEventArgs e)
		{
			
			foreach (Thread thread in threads)
			{
				thread.Join();
			}
			
		}
		private void PageAfterNavigation(object sender, NavigationEventArgs e)
		{
			entries.Add(new LLRecordEntry("Desktop.Web.CurrentPage", "WaitForPage", "\"" + e.URL + "\""));
		}
		#endregion

		private List<Thread> threads = new List<Thread>();
		private List<IRecordEntry> entries;
		private LowLevelKeyboardHook llkbHook;
		private LowLevelMouseHook llMouseHook;

	}

	public class MouseEntry
	{
		public MouseEntry(bool isUp, MouseButtons button, ManagedWinapi.Windows.POINT point)
		{
			this.isUp = isUp;
			this.button = button;
			this.point = point;
			llEntry = new LLRecordEntry();
		}

		private bool isUp;

		public bool IsUp
		{
			get { return isUp; }
			set { isUp = value; }
		}

		private MouseButtons button;

		public MouseButtons Button
		{
			get { return button; }
			set { button = value; }
		}

		private ManagedWinapi.Windows.POINT point;

		public ManagedWinapi.Windows.POINT Point
		{
			get { return point; }
			set { point = value; }
		}

		private LLRecordEntry llEntry;

		public LLRecordEntry LLEntry
		{
			get { return llEntry; }
			set { llEntry = value; }
		}
	
	}

	public class KBEntry
	{
		public KBEntry(bool isUp, Key key, IntPtr inputHandle)
		{
			this.isUp = isUp;
			this.key = key;
			this.inputHandle = inputHandle;
			llEntry = new LLRecordEntry();
		}

		private Key key;

		public Key Key
		{
			get { return key; }
			set { key = value; }
		}
	
		private bool isUp;

		public bool IsUp
		{
			get { return isUp; }
			set { isUp = value; }
		}

		private IntPtr inputHandle;

		public IntPtr InputHandle
		{
			get { return inputHandle; }
			set { inputHandle = value; }
		}
	

		private LLRecordEntry llEntry;

		public LLRecordEntry LLEntry
		{
			get { return llEntry; }
			set { llEntry = value; }
		}
		
	}

	

	
}

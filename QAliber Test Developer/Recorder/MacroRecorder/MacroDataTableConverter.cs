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
using System.Windows.Forms;
using QAliber.Recorder.Structures;
using QAliber.Engine.Win32;

namespace QAliber.Recorder.MacroRecorder
{
	public class MacroDataTableConverter
	{
		public MacroDataTableConverter(MacroRecorder recorder, MacroRecordingsDataSet.MacroEntriesDataTable dataTable)
		{
			this.dataTable = dataTable;
			this.recorder = recorder;
		}

		public void ConvertToRaw()
		{
			recorder.Entries.Clear();
			MacroRecordEntry newEntry;
			Win32Input input;
			foreach (MacroRecordingsDataSet.MacroEntriesRow row in dataTable)
			{
				input = new Win32Input();
			   
				if (row.IsActionNull()) // kb input
				{
					input.type = 1;
					input.ki = new KBInput();

					input.ki.wVk = (ushort)System.Windows.Input.KeyInterop.VirtualKeyFromKey(
						(System.Windows.Input.Key)Enum.Parse(typeof(System.Windows.Input.Key), row.Key));
					input.ki.dwFlags |= (row.Pressed ? (uint)KBEvents.KEYUP : 0);
				}
				else // mouse input
				{
					input.type = 0;
					input.mi = new MouseInput();
					input.mi.dx = (int)(row.X * (65535f / (Screen.PrimaryScreen.Bounds.Width)));
					input.mi.dy = (int)(row.Y * (65535f / Screen.PrimaryScreen.Bounds.Height));
					int evt = (int)Enum.Parse(typeof(MouseEvents), row.Action);
					evt |= (int)MouseEvents.ABSOLUTE;
					input.mi.dwFlags = (uint)evt;
				}
				newEntry = new MacroRecordEntry(input, row.Time);
				recorder.Entries.Add(newEntry);
				
			}
		}

		public void ConvertToTable()
		{
			dataTable.Rows.Clear();
			int i = 0;
			foreach (MacroRecordEntry entry in recorder.Entries)
			{
				MacroRecordingsDataSet.MacroEntriesRow row = (MacroRecordingsDataSet.MacroEntriesRow)dataTable.NewRow();
				row.Time = (int)entry.Time;
				//row.OrigIndex = i;
				if (entry.Input.type == 0) //mouse input
				{
					row.X = (int)(entry.Input.mi.dx * Screen.PrimaryScreen.Bounds.Width / 65535f);
					row.Y = (int)(entry.Input.mi.dy * Screen.PrimaryScreen.Bounds.Height / 65535f);
					MouseEvents evt = (MouseEvents)(entry.Input.mi.dwFlags & 0x7fff);
					row.Action = evt.ToString();
				}
				else if (entry.Input.type == 1) //kb input
				{
					if ((entry.Input.ki.dwFlags & (uint)KBEvents.KEYUP) != 0)
						row.Pressed = false;
					else
						row.Pressed = true;
					row.Key = System.Windows.Input.KeyInterop.KeyFromVirtualKey(entry.Input.ki.wVk).ToString(); 
				}
				dataTable.Rows.Add(row);
				i++;
			}

		}

		private MacroRecordingsDataSet.MacroEntriesDataTable dataTable;
		private MacroRecorder recorder;
	}
}

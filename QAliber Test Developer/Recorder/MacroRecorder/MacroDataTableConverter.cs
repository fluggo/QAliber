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
		public MacroDataTableConverter(List<MacroRecordEntry> rawEntries, MacroRecordingsDataSet.MacroEntriesDataTable dataTable)
		{
			this.dataTable = dataTable;
			this.rawEntries = rawEntries;
		}

		public void ConvertToRaw()
		{
			List<MacroRecordEntry> newEntries = new List<MacroRecordEntry>();
			foreach (MacroRecordingsDataSet.MacroEntriesRow row in dataTable)
			{
				if (row.IsActionNull()) // kb input
				{
					rawEntries[row.OrigIndex].Input.ki.wVk = (ushort)System.Windows.Input.KeyInterop.VirtualKeyFromKey(
						(System.Windows.Input.Key)Enum.Parse(typeof(System.Windows.Input.Key), row.Key));
					rawEntries[row.OrigIndex].Input.ki.dwFlags |= (row.Pressed ? (uint)KBEvents.KEYUP : 0);
				}
				else // mouse input
				{
					rawEntries[row.OrigIndex].Input.mi.dx = (int)(row.X * (65535f / (Screen.PrimaryScreen.Bounds.Width)));
					rawEntries[row.OrigIndex].Input.mi.dy = (int)(row.Y * (65535f / Screen.PrimaryScreen.Bounds.Height));
					int evt = (int)Enum.Parse(typeof(MouseEvents), row.Action);
					evt |= (int)MouseEvents.ABSOLUTE;
					rawEntries[row.OrigIndex].Input.mi.dwFlags = (uint)evt;
				}
				rawEntries[row.OrigIndex].Time = row.Time;
				newEntries.Add(rawEntries[row.OrigIndex]);
			}
			rawEntries.Clear();
			foreach (MacroRecordEntry entry in newEntries)
				rawEntries.Add(entry);
			
		}

		public void ConvertToTable()
		{
			dataTable.Rows.Clear();
			int i = 0;
			foreach (MacroRecordEntry entry in rawEntries)
			{
				MacroRecordingsDataSet.MacroEntriesRow row = (MacroRecordingsDataSet.MacroEntriesRow)dataTable.NewRow();
				row.Time = (int)entry.Time;
				row.OrigIndex = i;
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
		private List<MacroRecordEntry> rawEntries;
	}
}

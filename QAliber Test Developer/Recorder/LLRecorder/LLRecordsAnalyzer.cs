using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Diagnostics;
namespace QAliber.Recorder
{
	public class LLRecordsAnalyzer
	{
		public LLRecordsAnalyzer(LLRecorder recorder)
		{
			this.recorder = recorder;
		}



		public List<IRecordEntry> AnalyzedLLEntries
		{
			get { return analyzedEntries; }
		}
	
		public void Analyze()
		{
			if (recorder.Entries.Count == 0)
				return;
			LLRecordEntry lastEntry = recorder.Entries[0] as LLRecordEntry;
			string keys = "";
			for (int i = 1; i < recorder.Entries.Count; i++)
			{
				LLRecordEntry entry = recorder.Entries[i] as LLRecordEntry;
				AnalyzedLLRecordEntry newEntry = null;

				#region Keys Handling
				if (entry.Key != System.Windows.Input.Key.None)
				{
					if (entry.CodePath != lastEntry.CodePath)
					{
						if (keys != "")
						{

							newEntry = new AnalyzedLLRecordEntry(lastEntry.CodePath, keys, entry.Time, lastEntry.Type, lastEntry.Name);
							keys = "";
						}
						keys += MapKeyToString(entry.Key, entry.InputHandle);

					}
					else
					{
						if (!entry.IsKeyUp && (lastEntry.Key != entry.Key || lastEntry.IsKeyUp))
						{
							keys += MapKeyToString(entry.Key, entry.InputHandle);
						}
					}
						
				}
				else if (keys != "" )
				{
					newEntry = new AnalyzedLLRecordEntry(lastEntry.CodePath, keys, entry.Time, lastEntry.Type, lastEntry.Name);
					keys = "";
				}
				#endregion

				#region Mouse Handling
				if (entry.Button != System.Windows.Forms.MouseButtons.None)
				{
					if (entry.IsMouseUp)
					{

						if (CalcDistance(entry.AbsPoint, lastEntry.AbsPoint) < 10)
						{

							if (analyzedEntries.Count > 0)
							{
								AnalyzedLLRecordEntry lastAnalyzedEntry = analyzedEntries[analyzedEntries.Count - 1] as AnalyzedLLRecordEntry;
								if (lastAnalyzedEntry.Action == "Click" &&
									lastAnalyzedEntry.CodePath == entry.CodePath &&
									lastAnalyzedEntry.Button == entry.Button &&
									entry.Time - lastAnalyzedEntry.Time < new TimeSpan(0, 0, 3) &&
									CalcDistance(entry.RelativePoint, lastAnalyzedEntry.RelativePoint1) < 10)
								{
									//Double-Click
									analyzedEntries.RemoveAt(analyzedEntries.Count - 1);
									newEntry = new AnalyzedLLRecordEntry(entry.CodePath, "DoubleClick", entry.Button, entry.RelativePoint,
										entry.Time, entry.Type, entry.Name);
									OutputEntry(newEntry);
									analyzedEntries.Add(newEntry);
									continue;
								}
							}
							//Click
							newEntry = new AnalyzedLLRecordEntry(lastEntry.CodePath, "Click", lastEntry.Button, lastEntry.RelativePoint,
								lastEntry.Time, lastEntry.Type, lastEntry.Name);
						}

						if (!lastEntry.IsMouseUp && CalcDistance(entry.AbsPoint, lastEntry.AbsPoint) > 10)
						{
							//Drag
							newEntry = new AnalyzedLLRecordEntry(lastEntry.CodePath,
								"Drag", entry.Button, lastEntry.RelativePoint, entry.Time, lastEntry.Type, lastEntry.Name);
							Point rel2 = new Point(lastEntry.RelativePoint.X + entry.AbsPoint.X - lastEntry.AbsPoint.X,
								lastEntry.RelativePoint.Y + entry.AbsPoint.Y - lastEntry.AbsPoint.Y);
							newEntry.RelativePoint2 = rel2;
						}
					}
					else
					{
						if (!lastEntry.IsMouseUp &&
							entry.Button == lastEntry.Button)
						{
							newEntry = new AnalyzedLLRecordEntry(lastEntry.CodePath, "Click", lastEntry.Button, lastEntry.RelativePoint,
								lastEntry.Time, lastEntry.Type, lastEntry.Name);
						}

					}
				}
				#endregion

				#region Misc Handling
				if (entry.Action != null)
				{
					if (newEntry != null && newEntry.CodePath != null && newEntry.CodePath.Length > 0)
					{
						OutputEntry(newEntry);
						analyzedEntries.Add(newEntry);
					}
					newEntry = new AnalyzedLLRecordEntry(entry.CodePath, entry.Time, entry.Action, entry.Name, entry.ActionParams);
				}
				#endregion
				if (newEntry != null && newEntry.CodePath != null && newEntry.CodePath.Length > 0)
				{
					OutputEntry(newEntry);
					analyzedEntries.Add(newEntry);
				}
				lastEntry = entry;
			}
			if (keys != "" && !keys.Contains(keysFiltering))
			{
				analyzedEntries.Add(new AnalyzedLLRecordEntry(lastEntry.CodePath, keys, lastEntry.Time, lastEntry.Type, lastEntry.Name));
			}
		}

		private double CalcDistance(Point p1, Point p2)
		{
			return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
		}

		private bool AreSameCodePath(string cp1, string cp2)
		{
			if (cp1 == cp2)
				return true;
			int index1 = cp1.LastIndexOf('[');
			int index2 = cp2.LastIndexOf('[');
			if (index1 < 0 || index2 < 0)
				return false;
			string last1 = cp1.Substring(index1 + 1);
			string last2 = cp2.Substring(index2 + 1);
			return (last1 == last2);


		}

		private void OutputEntry(AnalyzedLLRecordEntry entry)
		{
			Debug.WriteLine(string.Format("LL Analyzed : {0}\t{1}\r\n{2}", entry.Time, entry.CodePath, entry.Action));
		}

		private string MapKeyToString(System.Windows.Input.Key key, IntPtr inputHandle)
		{
			switch (key)
			{
				case System.Windows.Input.Key.Add:
				case System.Windows.Input.Key.Back:
				case System.Windows.Input.Key.CapsLock:
				case System.Windows.Input.Key.D0:
				case System.Windows.Input.Key.D1:
				case System.Windows.Input.Key.D2:
				case System.Windows.Input.Key.D3:
				case System.Windows.Input.Key.D4:
				case System.Windows.Input.Key.D5:
				case System.Windows.Input.Key.D6:
				case System.Windows.Input.Key.D7:
				case System.Windows.Input.Key.D8:
				case System.Windows.Input.Key.D9:
				case System.Windows.Input.Key.Delete:
				case System.Windows.Input.Key.Divide:
				case System.Windows.Input.Key.Down:
				case System.Windows.Input.Key.End:
				case System.Windows.Input.Key.Return:
				case System.Windows.Input.Key.Escape:
				case System.Windows.Input.Key.F1:
				case System.Windows.Input.Key.F10:
				case System.Windows.Input.Key.F11:
				case System.Windows.Input.Key.F12:
				case System.Windows.Input.Key.F13:
				case System.Windows.Input.Key.F14:
				case System.Windows.Input.Key.F15:
				case System.Windows.Input.Key.F16:
				case System.Windows.Input.Key.F17:
				case System.Windows.Input.Key.F18:
				case System.Windows.Input.Key.F19:
				case System.Windows.Input.Key.F2:
				case System.Windows.Input.Key.F20:
				case System.Windows.Input.Key.F21:
				case System.Windows.Input.Key.F22:
				case System.Windows.Input.Key.F23:
				case System.Windows.Input.Key.F24:
				case System.Windows.Input.Key.F3:
				case System.Windows.Input.Key.F4:
				case System.Windows.Input.Key.F5:
				case System.Windows.Input.Key.F6:
				case System.Windows.Input.Key.F7:
				case System.Windows.Input.Key.F8:
				case System.Windows.Input.Key.F9:
				case System.Windows.Input.Key.Home:
				case System.Windows.Input.Key.Insert:
				case System.Windows.Input.Key.RWin:
				case System.Windows.Input.Key.LWin:
				case System.Windows.Input.Key.Left:
				case System.Windows.Input.Key.LeftAlt:
				case System.Windows.Input.Key.RightAlt:
				case System.Windows.Input.Key.LeftCtrl:
				case System.Windows.Input.Key.RightCtrl:
				case System.Windows.Input.Key.LeftShift:
				case System.Windows.Input.Key.RightShift:
				case System.Windows.Input.Key.Multiply:
				case System.Windows.Input.Key.NumLock:
				case System.Windows.Input.Key.NumPad0:
				case System.Windows.Input.Key.NumPad1:
				case System.Windows.Input.Key.NumPad2:
				case System.Windows.Input.Key.NumPad3:
				case System.Windows.Input.Key.NumPad4:
				case System.Windows.Input.Key.NumPad5:
				case System.Windows.Input.Key.NumPad6:
				case System.Windows.Input.Key.NumPad7:
				case System.Windows.Input.Key.NumPad8:
				case System.Windows.Input.Key.NumPad9:
				case System.Windows.Input.Key.PageDown:
				case System.Windows.Input.Key.PageUp:
				case System.Windows.Input.Key.PrintScreen:
				case System.Windows.Input.Key.Right:
				case System.Windows.Input.Key.Space:
				case System.Windows.Input.Key.Subtract:
				case System.Windows.Input.Key.Tab:
				case System.Windows.Input.Key.Up:
					return "{" + key.ToString() + "}";
				default:
					return QAliber.Engine.Win32.LowLevelInput.ConvertKeyToUnicode(key, inputHandle);
					
			}
		}

		private List<IRecordEntry> analyzedEntries = new List<IRecordEntry>();
		private string keysFiltering = "Shift}{D6}";

		LLRecorder recorder;

	}
}

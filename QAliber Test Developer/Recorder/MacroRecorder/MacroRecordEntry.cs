using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using QAliber.Engine.Win32;

namespace QAliber.Recorder
{
	[Serializable]
	public class MacroRecordEntry
	{
		public MacroRecordEntry(Win32Input input, long time)
		{
			Input = input;
			Time = time;
		}

		public Win32Input Input;
		public long Time;
		
	}

	
}

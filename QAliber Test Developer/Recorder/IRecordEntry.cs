using System;
using System.Collections.Generic;
using System.Text;

namespace QAliber.Recorder
{
	public interface IRecordEntry
	{
		int Index { get; }
		string Name { get; set; }
		string CodePath { get; set; }
		DateTime Time { get; set; }
		string Action { get; set; }
		string Type { get; set; }
		string[] ActionParams { get; set; }
	}

	public class IndexSorter : IComparer<IRecordEntry>
	{
		#region IComparer<IRecordEntry> Members

		public int Compare(IRecordEntry x, IRecordEntry y)
		{
			return x.Index - y.Index;
		}

		#endregion
	}
}

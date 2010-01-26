using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QAliber.Recorder
{
	public class RecordsDisplayer 
	{
		public RecordsDisplayer(List<IRecordEntry> entries)
		{
			this.entries = entries;
		}
		
		public void ReplaceVars()
		{
			varsTable = new Dictionary<string, string>();
			for (int i = 0; i < entries.Count; i++)
			{
				IRecordEntry entry = entries[i];
				entry.CodePath = ConvertNewLineNamesToRegexNames(entry.CodePath);
				string varName = GetVarNameFromEntry(entry);
				if (!varsTable.ContainsKey(entry.CodePath))
				{
					varsTable.Add(entry.CodePath, varName);
				}
			}
			

		}
	   
		public string PrintCSharpCode()
		{
			if (entries.Count == 0)
				return "";
			StringBuilder builder = new StringBuilder();
			DateTime lastTime = entries[0].Time;
			for (int i = 0; i < entries.Count; i++)
			{
				if (RecorderConfig.Default.KeepRecordedTimings)
				{
					if (i > 0)
					{
						builder.AppendFormat("System.Threading.Thread.Sleep({0});\n", (int)((TimeSpan)(entries[i].Time - lastTime)).TotalMilliseconds);
					}
					lastTime = entries[i].Time;

				}
				if (varsTable.ContainsKey(entries[i].CodePath))
				{
					string varName = varsTable[entries[i].CodePath];
					if (!varName.EndsWith("**Declared")) 
					{
						if (entries[i].Type != null)
						{
							builder.AppendFormat("{0} {1} = {2} as {0};\n", entries[i].Type, varName, entries[i].CodePath);
							varsTable[entries[i].CodePath] = varName + "**Declared";
						}
						builder.AppendFormat("{0}.{1}({2});\n", varName, entries[i].Action, CombineActionParams(entries[i].ActionParams));
					}
					else
						builder.AppendFormat("{0}.{1}({2});\n", varName.Replace("**Declared", ""), entries[i].Action, CombineActionParams(entries[i].ActionParams));
				}
				//else
				//{
				//	  string newVarName = GetNameFromCodePath(entries[i].CodePath);
				//	  builder.AppendFormat("{0} {1} = {2} as {0};\n", entries[i].Type, newVarName, entries[i].CodePath);
				//	  builder.AppendFormat("{0}.{1}({2});\n", newVarName, entries[i].Action, CombineActionParams(entries[i].ActionParams));
				//}
				
			   
			}
			return builder.ToString();
		}

		public string PrintVBCode()
		{
			if (entries.Count == 0)
				return "";
			StringBuilder builder = new StringBuilder();
			DateTime lastTime = entries[0].Time;
			for (int i = 0; i < entries.Count; i++)
			{
				
				string vbCodePath = ConvertCodePathToVB(entries[i].CodePath);
				if (RecorderConfig.Default.KeepRecordedTimings)
				{
					if (i > 0)
					{
						builder.AppendFormat("System.Threading.Thread.Sleep({0})\n", (int)((TimeSpan)(entries[i].Time - lastTime)).TotalMilliseconds);
					}
					lastTime = entries[i].Time;

				}
				if (varsTable.ContainsKey(entries[i].CodePath))
				{
					string varName = varsTable[entries[i].CodePath];
					if (!varName.EndsWith("**Declared"))
					{
						if (entries[i].Type != null)
						{
							builder.AppendFormat("Dim {1} As {0} = {2}\n", entries[i].Type, varName, ConvertCodePathToVB(entries[i].CodePath));
						}
						builder.AppendFormat("{0}.{1}({2})\n", varName, entries[i].Action, CombineActionParams(entries[i].ActionParams));
						varsTable[entries[i].CodePath] = varsTable[entries[i].CodePath] + "**Declared";
					}
					else
						builder.AppendFormat("{0}.{1}({2})\n", varName.Replace("**Declared", ""), entries[i].Action, CombineActionParams(entries[i].ActionParams));
				}
				//else
				//{
				//	  string newVarName = GetNameFromCodePath(entries[i].CodePath);
				//	  builder.AppendFormat("Dim {1} As {0} = {2}\n", entries[i].Type, ConvertVarNameToVB(newVarName), vbCodePath);
				//	  builder.AppendFormat("{0}.{1}({2})\n", ConvertVarNameToVB(newVarName), entries[i].Action, CombineActionParams(entries[i].ActionParams));
				//}


			}
			return builder.ToString();
		}

		public static string ConvertCodePathToVB(string codepath)
		{
			return codepath.Replace("[", "(").Replace("]", ")").Replace("@", "");
		}

		private string GetVarNameFromEntry(IRecordEntry entry)
		{
			if (entry.Type == null)
				return entry.CodePath;
			string naiveVar = entry.Name + entry.Index;
			Match match = Regex.Match(naiveVar, "[A-Za-z][A-Za-z0-9]+");
			if (match.Success)
				return match.Value.ToLower();
			else
				return "unknown" + entry.Index;
		}

		private string CombineActionParams(string[] actionParams)
		{
			if (actionParams.Length > 0)
				return string.Join(", ", actionParams);
			else
				return "";
		}

		private string ConvertNewLineNamesToRegexNames(string codePath)
		{
			string res = codePath;
			int startIndex = codePath.IndexOf('\n');
			if (startIndex > 0)
			{
				int endIndex = codePath.IndexOf(']', startIndex);
				res = res.Remove(startIndex, endIndex - startIndex);
				res = res.Insert(startIndex, "\", true");
			}
			return res;
		}

		private Dictionary<string, string> varsTable = new Dictionary<string, string>();
		private List<IRecordEntry> entries;
	}

	class TimeSorter : IComparer<IRecordEntry>
	{
		#region IComparer<IRecordEntry> Members

		public int Compare(IRecordEntry x, IRecordEntry y)
		{
			return (int)(x.Time.Ticks - y.Time.Ticks);
		}

		#endregion
	}

	class CodePathSizeSorter : IComparer<IRecordEntry>
	{
		#region IComparer<IRecordEntry> Members

		public int Compare(IRecordEntry x, IRecordEntry y)
		{
			if (x.CodePath.Length == y.CodePath.Length)
				return string.Compare(x.CodePath, y.CodePath);
			else
				return x.CodePath.Length - y.CodePath.Length;
		}

		#endregion
	}
}

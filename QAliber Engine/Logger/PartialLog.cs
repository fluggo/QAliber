using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace QAliber.Logger
{
	public class PartialLog
	{
		public PartialLog(string origFile)
		{
			this.origFile = origFile;
		}

		public string FixedPath
		{
			get { return fullPath; }
		}

		public bool TryToFix()
		{
			CopyPartialLog();
			int openEntries = CountOpenChildEntries();

			using (StreamWriter writer = new StreamWriter(fullPath, true))
			{
				for (int i = 0; i < openEntries; i++)
				{
					writer.WriteLine("</ChildEntries>");

				}
				writer.WriteLine("</LogEntries>");
			}
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(fullPath);
			}
			catch
			{
				return false;
			}
			return true;

		}

		private void CopyPartialLog()
		{
			string dir = Path.GetDirectoryName(origFile);
			string file = "Run.Partial.qlog";
			fullPath = Path.Combine(dir, file);
			File.Copy(origFile, fullPath, true);
		}

		private int CountOpenChildEntries()
		{
			int count = 0;
			using (StreamReader reader = new StreamReader(fullPath))
			{
				string line = reader.ReadLine();
				while (line != null)
				{
					if (line.Contains("<ChildEntries>"))
						count++;
					else if (line.Contains("</ChildEntries>"))
						count--;
					line = reader.ReadLine();
				}
			}
			return count;
		}


		private string origFile;
		private string fullPath;
	}
}

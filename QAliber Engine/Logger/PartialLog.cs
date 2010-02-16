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

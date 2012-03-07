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
using System.Windows.Forms;
using System.ComponentModel;
using QAliber.Logger;
using System.Text.RegularExpressions;
using System.Xml.Serialization;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Search text file by a given pattern
	/// <workflow>
	/// <action>Read file line by line</action>
	/// <verification>If we use regular expression looks whether the regular expression matches the line</verification>
	/// <verification>Else looks whether the line contains the given pattern</verification>
	/// </workflow>
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	[XmlType(TypeName="FileSearch", Namespace=Util.XmlNamespace)]
	public class FileSearch : global::QAliber.TestModel.TestCase
	{
		public FileSearch() : base( "Search File" )
		{
			Icon = Properties.Resources.FileSearch;
		}

		public override void Body()
		{
			using (StreamReader reader = new StreamReader(sourceFile))
			{
				string line = reader.ReadLine();
				int lineNumber = 1;
				while (line != null)
				{
					if (useRegex)
					{
						if (Regex.Match(line, pattern).Success)
						{
							textOutput = line;
							Log.Default.Info("Pattern '" + pattern + "' was found in line " + lineNumber.ToString(), line);
							ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
							break;
						}
					}
					else
					{
						if (line.Contains(pattern))
						{
							textOutput = line;
							Log.Default.Info("Pattern '" + pattern + "' was found in line " + lineNumber.ToString(), line);
							ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
							break;
						}
					}
					line = reader.ReadLine();
					lineNumber++;
				}
				if (ActualResult != QAliber.RemotingModel.TestCaseResult.Passed)
				{
					Log.Default.Error("Pattern '" + pattern + "' was not found");
					ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				}
			}
		}

		private string sourceFile = "";

		/// <summary>
		/// The file to search
		/// </summary>
		[DisplayName("File To Search")]
		[Category("Files")]
		[Description("The file to be searched")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SourceFile
		{
			get { return sourceFile; }
			set { sourceFile = value; }
		}

		private string pattern = "";

		/// <summary>
		/// The pattern to look for
		/// </summary>
		[Category("Files")]
		[Description("The pattern to search in the file")]
		public string Pattern
		{
			get { return pattern; }
			set { pattern = value; }
		}

		private bool useRegex;

		/// <summary>
		/// Is the pattern treated as regular expression
		/// </summary>
		[DisplayName("Use Regular Expression ?")]
		[Category("Files")]
		public bool UseRegex
		{
			get { return useRegex; }
			set { useRegex = value; }
		}

		private string textOutput;
		/// <summary>
		/// Will save the line containing the search text.
		/// </summary>
		[DisplayName("Get line of found text")]
		[Category("Files")]
		[XmlIgnore]
		public string TextOutput
		{
			get { return textOutput; }
		}
	
	

		public override string Description
		{
			get
			{
				return "Looking in file '" + sourceFile + "' for the pattern '" + pattern + "'";
			}
		}

		public override object Clone() {
			FileSearch result = (FileSearch) base.Clone();

			result.textOutput = null;

			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using QAliber.Logger;
using System.Text.RegularExpressions;



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
	public class FileSearch : global::QAliber.TestModel.TestCase
	{
		public FileSearch()
		{
			name = "Search File";
			icon = Properties.Resources.FileSearch;
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
							Log.Default.Info("Pattern '" + pattern + "' was found in line " + lineNumber.ToString(), line);
							actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
							break;
						}
					}
					else
					{
						if (line.Contains(pattern))
						{
							Log.Default.Info("Pattern '" + pattern + "' was found in line " + lineNumber.ToString(), line);
							actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
							break;
						}
					}
					line = reader.ReadLine();
					lineNumber++;
				}
				if (actualResult != QAliber.RemotingModel.TestCaseResult.Passed)
				{
					Log.Default.Error("Pattern '" + pattern + "' was not found");
					actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
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
	
	

		public override string Description
		{
			get
			{
				return "Looking in file '" + sourceFile + "' for the pattern '" + pattern + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}
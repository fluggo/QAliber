using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Retrieve files from a specified directory
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	public class FilesRetrieve : global::QAliber.TestModel.TestCase
	{
		public FilesRetrieve()
		{
			name = "Retrieve Files";
			icon = Properties.Resources.FileRetrieve;
		}

		public override void Body()
		{
			retrievedFiles = Directory.GetFiles(sourceDir, pattern, searchOption);
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string sourceDir = "";

		[DisplayName("Directory")]
		[Category("Files")]
		[Description("The directory to get the files from")]
		[Editor(typeof(UITypeEditors.DirectoryBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SourceDir
		{
			get { return sourceDir; }
			set { sourceDir = value; }
		}

		private string pattern = "*.*";

		/// <summary>
		/// The pattern to look for use wildcard(*) to match any character
		/// </summary>
		[DisplayName("Search Pattern")]
		[Category("Files")]
		public string Pattern
		{
			get { return pattern; }
			set { pattern = value; }
		}

		private SearchOption searchOption;

		[DisplayName("Search Option")]
		[Category("Files")]
		public SearchOption SearchOption
		{
			get { return searchOption; }
			set { searchOption = value; }
		}

		private string[] retrievedFiles = new string[] { };

		/// <summary>
		/// Read-only, can be used as a list to iterate on, in a different test case
		/// </summary>
		[DisplayName("Retrieved Files")]
		[Category("Files")]
		public string[] RetrievedFiles
		{
			get { return retrievedFiles; }
		}
	
		public override string Description
		{
			get
			{
				return "Retrieving files '" + pattern + "' from " + sourceDir;
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}

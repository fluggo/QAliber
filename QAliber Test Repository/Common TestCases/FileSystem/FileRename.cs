using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Renames a file
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	public class FileRename : global::QAliber.TestModel.TestCase
	{
		public FileRename()
		{
			name = "Rename File";
			icon = Properties.Resources.File;
		}

		public override void Body()
		{
			File.Move(sourceFile, destFile);
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string sourceFile = "";

		[DisplayName("1) Source File")]
		[Category("Files")]
		[Description("The file to be renamed")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SourceFile
		{
			get { return sourceFile; }
			set { sourceFile = value; }
		}

		private string destFile = "";

		[DisplayName("2) Renamed File")]
		[Category("Files")]
		[Description("The new file's name")]
		[Editor(typeof(UITypeEditors.FileSaveTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string DestFile
		{
			get { return destFile; }
			set { destFile = value; }
		}

		public override string Description
		{
			get
			{
				return "Renaming file '" + sourceFile + "' to '" + destFile + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}

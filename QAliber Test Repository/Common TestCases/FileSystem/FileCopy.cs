using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Copies a file
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	public class FileCopy : global::QAliber.TestModel.TestCase
	{
		public FileCopy()
		{
			name = "Copy File";
			icon = Properties.Resources.FileCopy;
		}

		public override void Body()
		{
			File.Copy(sourceFile, destFile, overwrite);
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string sourceFile = "";

		[DisplayName("1) Source File")]
		[Category("Files")]
		[Description("The file to be copied")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SourceFile
		{
			get { return sourceFile; }
			set { sourceFile = value; }
		}

		private string destFile = "";

		[DisplayName("2) Destination File")]
		[Category("Files")]
		[Description("The new file to be created / overwrited")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string DestFile
		{
			get { return destFile; }
			set { destFile = value; }
		}

		private bool overwrite;

		[DisplayName("3) Should Overwrite ?")]
		[Category("Files")]
		[Description("Should the destination file be overwritten if exists ?")]
		public bool ShouldOverwrite
		{
			get { return overwrite; }
			set { overwrite = value; }
		}

		public override string Description
		{
			get
			{
				return "Copying '" + sourceFile + "' to '" + destFile + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}

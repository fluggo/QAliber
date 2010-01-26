using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Deletes a file
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	public class FileDelete : global::QAliber.TestModel.TestCase
	{
		public FileDelete()
		{
			name = "Delete File";
			icon = Properties.Resources.FileDelete;
		}

		public override void Body()
		{
			File.Delete(sourceFile);
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string sourceFile = "";

		[DisplayName("File To Delete")]
		[Category("Files")]
		[Description("The file to be deleted")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SourceFile
		{
			get { return sourceFile; }
			set { sourceFile = value; }
		}

		public override string Description
		{
			get
			{
				return "Deleting file '" + sourceFile + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}

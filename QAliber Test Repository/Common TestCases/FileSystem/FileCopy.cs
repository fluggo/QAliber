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
using System.Xml.Serialization;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Copies a file
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	[XmlType(TypeName="FileCopy", Namespace=Util.XmlNamespace)]
	public class FileCopy : global::QAliber.TestModel.TestCase
	{
		public FileCopy() : base( "Copy File" )
		{
			Icon = Properties.Resources.FileCopy;
		}

		public override void Body()
		{
			File.Copy(sourceFile, destFile, overwrite);
			ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
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
		[Editor(typeof(UITypeEditors.FileSaveTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
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
		}
	
	
	
	}
}

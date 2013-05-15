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
using QAliber.TestModel;
using QAliber.Logger;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Renames a file
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	[XmlType(TypeName="FileRename", Namespace=Util.XmlNamespace)]
	public class FileRename : global::QAliber.TestModel.TestCase
	{
		public FileRename() : base( "Rename File" )
		{
			Icon = Properties.Resources.File;
		}

		public override void Body( TestRun run )
		{
			File.Move(sourceFile, destFile);
			ActualResult = TestCaseResult.Passed;
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
		}
	
	
	
	}
}

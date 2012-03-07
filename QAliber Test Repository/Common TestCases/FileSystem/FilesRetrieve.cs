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
	/// Retrieve files from a specified directory
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	[XmlType(TypeName="FilesRetrieve", Namespace=Util.XmlNamespace)]
	public class FilesRetrieve : global::QAliber.TestModel.TestCase
	{
		public FilesRetrieve() : base( "Retrieve Files" )
		{
			Icon = Properties.Resources.FileRetrieve;
		}

		public override void Body()
		{
			retrievedFiles = Directory.GetFiles(sourceDir, pattern, searchOption);
			ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;
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
		[XmlIgnore]
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
		}

		public override object Clone() {
			FilesRetrieve result = (FilesRetrieve) base.Clone();

			result.retrievedFiles = new string[0];

			return result;
		}

	}
}

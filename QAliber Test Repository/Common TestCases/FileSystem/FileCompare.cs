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
using System.Xml.Serialization;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Compare 2 files
	/// <preconditions>Files should exist</preconditions>
	/// <workflow>
	/// <verification>File sizes are equal</verification>
	/// <action>Reads files line by line</action>
	/// <verification>For each line - line should be equal</verification>
	/// </workflow>
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	[XmlType(TypeName="FileCompare", Namespace=Util.XmlNamespace)]
	public class FileCompare : global::QAliber.TestModel.TestCase
	{
		public FileCompare() : base( "Compare Files" )
		{
			icon = Properties.Resources.FileCompare;
		}

		public override void Body()
		{
			using (StreamReader reader1 = new StreamReader(sourceFile))
			{
				using (StreamReader reader2 = new StreamReader(destFile))
				{
					string line1 = reader1.ReadLine();
					string line2 = reader2.ReadLine();
					if (reader1.BaseStream.Length != reader2.BaseStream.Length)
					{
						Log.Default.Error("Sizes are different", reader1.BaseStream.Length + "\n" + reader2.BaseStream.Length);
						actualResult = global::QAliber.RemotingModel.TestCaseResult.Failed;
					}
					int lineNumber = 1;
					while (line1 != null && line2 != null)
					{
						if (string.Compare(line1, line2, true) != 0)
						{
							Log.Default.Error("Difference at line " + lineNumber, line1 + "\n" + line2);
							actualResult = global::QAliber.RemotingModel.TestCaseResult.Failed;
						}
						line1 = reader1.ReadLine();
						line2 = reader2.ReadLine();
						lineNumber++;
					}
				}
			}
			if (actualResult != global::QAliber.RemotingModel.TestCaseResult.Failed)
			{
				Log.Default.Info("Files are identical");
				actualResult = global::QAliber.RemotingModel.TestCaseResult.Passed;
			}
		}

		private string sourceFile = "";

		/// <summary>
		/// The 1st file to compare
		/// </summary>
		[DisplayName("1) Source File")]
		[Category("Files")]
		[Description("The 1st file to be compared")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SourceFile
		{
			get { return sourceFile; }
			set { sourceFile = value; }
		}

		private string destFile = "";

		/// <summary>
		/// The 2nd file to compare
		/// </summary>
		[DisplayName("2) Destination File")]
		[Category("Files")]
		[Description("The 2nd file to be compared")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string DestFile
		{
			get { return destFile; }
			set { destFile = value; }
		}

		public override string Description
		{
			get
			{
				return "Comparing '" + sourceFile + "' to '" + destFile + "'";
			}
		}
	
	
	
	}
}

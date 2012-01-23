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



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Copies a file
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\File System")]
	public class DirectoryDelete : global::QAliber.TestModel.TestCase
	{
		public DirectoryDelete() : base( "Delete Directory" )
		{
			icon = Properties.Resources.FileDelete;
		}

		public override void Body()
		{
			Directory.Delete(dirName);
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string dirName = "";

		[DisplayName("Directory Name")]
		[Category("Directory")]
		[Description("The directory to delete")]
		[Editor(typeof(UITypeEditors.DirectoryBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string DirName
		{
			get { return dirName; }
			set { dirName = value; }
		}

		

		
		public override string Description
		{
			get
			{
				return "Deleting Directory '" + dirName + "'";
			}
		}
	
	
	
	}
}

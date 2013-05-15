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
using QAliber.TestModel;
using System.Windows.Forms;
using System.Windows;
using System.ComponentModel;
using QAliber.Logger;
using System.Xml.Serialization;
using QAliber.Engine.Controls;

namespace QAliber.Repository.CommonTestCases.UI.Images
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Images")]
	[XmlType("SaveImageFromControl", Namespace=Util.XmlNamespace)]
	public class SaveImageFromControl : TestCase
	{
		public SaveImageFromControl() : base( "Save Image from Control" )
		{
			Icon = Properties.Resources.Bitmap;
		}

		private string control = "";


		[Category("Image")]
		[DisplayName("1) Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private string file = "";

		[Category("Image")]
		[DisplayName("2) File To Save")]
		[Editor(typeof(UITypeEditors.FileSaveTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Description("The file to save the image to (jpg format)")]
		public string File
		{
			get { return file; }
			set { file = value; }
		}

	
		public override void Body( TestRun run )
		{
			ActualResult = TestCaseResult.Passed;

			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( !c.Exists ) {
				ActualResult = TestCaseResult.Failed;
				throw new InvalidOperationException("Control not found");
			}

			c.GetImage().Save( file );
		}

		public override string Description
		{
			get
			{
				return "Saving image for path '" + control + "' to file '" + file + "'";
			}
		}

	}


}

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
using QAliber.TestModel.TypeEditors;
using System.Xml.Serialization;
using QAliber.Engine.Controls;

namespace QAliber.Repository.CommonTestCases.UI.Controls
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("GetPropertyFromControl", Namespace=Util.XmlNamespace)]
	public class GetPropertyFromControl : TestCase
	{
		public GetPropertyFromControl() : base( "Get Property from Control" )
		{
			Icon = Properties.Resources.Window;
			list = new MultipleSelectionList();
			list.Items.AddRange(
				new string[] { "Layout.X", "Layout.Y", "Layout.Width", "Layout.Height",
				"Name",
				"ClassName", "ID",
				"Enabled", "Visible"});
		}

		private string control = "";

		
		[Category("Control")]
		[DisplayName("1) Control")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private MultipleSelectionList list;

		[Category("Control")]
		[DisplayName("2) Properties")]
		[Description("The properties to retrieve")]
		[Editor(typeof(MultipleSelectionTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public MultipleSelectionList List
		{
			get { return list; }
			set { list = value; }
		}


		private string vals;

		[Category("Control")]
		[DisplayName("3) Values")]
		[Description("The values to retrieve")]
		public string Values
		{
			get { return vals; }
		}
		
	
	
		public override void Body()
		{
			ActualResult = QAliber.RemotingModel.TestCaseResult.Passed;

			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( !c.Exists ) {
				ActualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				throw new InvalidOperationException("Control not found");
			}

			List<string> values = new List<string>();

			foreach( string item in list.SelectedItems ) {
				switch( item ) {
					case "Layout.X":
						values.Add( c.Layout.X.ToString() );
						break;

					case "Layout.Y":
						values.Add( c.Layout.Y.ToString() );
						break;

					case "Layout.Width":
						values.Add( c.Layout.Width.ToString() );
						break;

					case "Layout.Height":
						values.Add( c.Layout.Height.ToString() );
						break;

					case "Name":
						values.Add( c.Name );
						break;

					case "ClassName":
						values.Add( c.ClassName );
						break;

					case "ID":
						values.Add( c.ID );
						break;

					case "Enabled":
						values.Add( c.Enabled.ToString() );
						break;

					case "Visible":
						values.Add( c.Visible.ToString() );
						break;

					default:
						values.Add( "(unknown property)" );
						break;
				}
			}

			vals = string.Join( ",", values );
		}

		public override string Description
		{
			get
			{
				return "Getting properties for control " + control;
			}
		}

	}


}


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
using QAliber.Engine.Controls;
using QAliber.Engine.Controls.UIA;
using QAliber.Engine.Controls.Web;
using QAliber.Engine.Patterns;
using System.Diagnostics;
using System.Xml.Serialization;
using QAliber.RemotingModel;

namespace QAliber.Repository.CommonTestCases.UI.Controls
{
   
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"GUI\Controls")]
	[XmlType("SelectListItemByString", Namespace=Util.XmlNamespace)]
	public class SelectListItemByString : TestCase
	{
		public SelectListItemByString() : base( "Select List Item by String" )
		{
			Icon = Properties.Resources.Combobox;
		}

		private string control = "";

		
		[Category("Behavior")]
		[DisplayName("Control")]
		[Description("Control to select from.")]
		[Editor(typeof(UITypeEditors.UIControlTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Control
		{
			get { return control; }
			set { control = value; }
		}

		private string _item;

		[Category("Behavior")]
		[DisplayName("Item")]
		[Description("Name of the item to select.")]
		public string Item
		{
			get {return _item;}
			set { _item = value; }
		}

		
	
	
		public override void Body()
		{
			ActualResult = TestCaseResult.Failed;
			UIControlBase c = UIControlBase.FindControlByPath( control );

			if( c == null || !c.Exists ) {
				Log.Default.Error( "Control not found" );
				return;
			}

			IListPattern listPattern = c.GetControlInterface<IListPattern>();

			if( listPattern == null ) {
				// Try one control up; we could be on the combo box's edit or button controls
				c = c.Parent;

				if( c == null || (listPattern = c.GetControlInterface<IListPattern>()) == null ) {
					Log.Default.Error( "Control doesn't look like a list",
						"Couldn't find an appropriate way to find and select items in the list." );
					return;
				}
			}

			UIAControl item = listPattern.GetItem( _item );

			if( item == null || !item.Exists ) {
				Log.Default.Error( "Item \"" + _item + "\" not found" );
				return;
			}

			ISelectionItemPattern selectionPattern = item.GetControlInterface<ISelectionItemPattern>();

			if( selectionPattern == null ) {
				// There are probably other ways to select an item, but we'll
				// leave it at this for now
				Log.Default.Error( "Item not selectable", "Couldn't find an appropriate way to select the item." );
				return;
			}

			selectionPattern.Select();
			ActualResult = TestCaseResult.Passed;
		}

		public override string Description
		{
			get
			{
				return "Selecting '" + _item + "' item from control '" + control + "'";
			}
		}

	}


}

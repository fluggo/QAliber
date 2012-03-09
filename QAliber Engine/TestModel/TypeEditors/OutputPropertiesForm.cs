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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Linq;

namespace QAliber.TestModel.TypeEditors
{
	public partial class OutputPropertiesForm : Form
	{
		public OutputPropertiesForm( TestCase testcase, OutputPropertiesMap output )
		{
			InitializeComponent();
			DialogResult = DialogResult.Cancel;
			this.testcase = testcase;

			this.output = new OutputPropertiesMap();

			foreach( var kv in output ) {
				this.output.Add( kv.Key, kv.Value );
			}

			FillList();
		}

		public OutputPropertiesMap Output
		{
			get { return output; }
			set { output = value; }
		}

		class Property {
			public ListViewItem Item;
			public PropertyDescriptor Descriptor;
			public string AssignedVariable;
		}

		private void FillList()
		{
			foreach( PropertyDescriptor propDesc in TypeDescriptor.GetProperties( testcase ) ) {
				if( !propDesc.IsBrowsable )
					continue;

				Property prop = new Property {
					Descriptor = propDesc,
					AssignedVariable = testcase.GetType().Name + "." + propDesc.Name
				};

				ListViewItem item = new ListViewItem(
					new string[] { propDesc.DisplayName, prop.AssignedVariable } ) {
					Tag = prop
				};

				item.SubItems[1].Tag = "TextBox";
				prop.Item = item;

				object curVal = propDesc.GetValue( testcase );

				if( curVal != null ) {
					ICollection collectionVal = curVal as ICollection;

					if( collectionVal != null )
						item.SubItems.Add( "[" + string.Join( ", ", collectionVal ) + "]" );
					else
						item.SubItems.Add( curVal.ToString() );
				}

				string alreadyAssignedVariable;

				if( output.TryGetValue( propDesc.Name, out alreadyAssignedVariable ) ) {
					item.Checked = true;
					prop.AssignedVariable = alreadyAssignedVariable;
				}

				propsListView.Items.Add( item );
			}
		}

		private void FillOutput() {
			testcase.Scenario.Variables.RemoveAllByTestCase( testcase );
			testcase.Scenario.Lists.RemoveAllByTestCase( testcase );
			testcase.Scenario.Tables.RemoveAllByTestCase( testcase );

			output.Clear();

			foreach( ListViewItem item in propsListView.Items ) {
				Property prop = (Property) item.Tag;

				if( item.Checked )
					output.Add( prop.Descriptor.Name, prop.AssignedVariable );
			}
		}

		#region Events
		private void ListViewClicked(object sender, EventArgs e)
		{
			Point p = propsListView.PointToClient(Cursor.Position);
			ListViewItem item = propsListView.GetItemAt(p.X, p.Y);
			if (item != null)
			{
				foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
				{
					if (subItem.Bounds.Contains(p))
					{
						if (subItem.Tag != null && subItem.Tag.ToString() == "TextBox")
						{
							TextBox box = new TextBox();
							box.Bounds = subItem.Bounds;
							box.Text = subItem.Text;
							box.Tag = item.Tag;
							box.Leave += new EventHandler(BoxLostFocus);
							propsListView.Controls.Add(box);
							box.Focus();
							box.SelectAll();
							break;
						}
					}
				}
			}
		}

		private void BoxLostFocus(object sender, EventArgs e)
		{
			TextBox box = sender as TextBox;
			Property prop = (Property) box.Tag as Property;
			prop.AssignedVariable = box.Text;

			propsListView.Controls.Remove(box);
		}

		private bool IsVarExists(string name)
		{
			if (testcase.Scenario.Variables[name] == null && testcase.Scenario.Lists[name] == null)
				return false;
			return true;

		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			FillOutput();
			DialogResult = DialogResult.OK;
		}
		#endregion

		private OutputPropertiesMap output = new OutputPropertiesMap();
		private TestCase testcase;

	}
}

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

namespace QAliber.TestModel.TypeEditors
{
	public partial class OutputPropertiesForm : Form
	{
		public OutputPropertiesForm(TestCase testcase, Dictionary<string, string> output)
		{
			InitializeComponent();
			DialogResult = DialogResult.Cancel;
			this.testcase = testcase;
			this.output = output;
			FillList();
		}

		

		public Dictionary<string, string> Output
		{
			get { return output; }
			set { output = value; }
		}
	

		private void FillList()
		{
			foreach (PropertyDescriptor propDesc in TypeDescriptor.GetProperties(testcase))
			{
				if (propDesc.IsBrowsable)
				{
					ListViewItem item = new ListViewItem(
						new string[] { propDesc.DisplayName, testcase.GetType().Name + "." + propDesc.Name });
					item.SubItems[1].Tag = "TextBox";
					object curVal = propDesc.GetValue(testcase);
					if (curVal != null)
					{
						ICollection collectionVal = curVal as ICollection;
						if (collectionVal != null)
						{
							string listVal = "{ ";
							foreach (object itemVal in collectionVal)
							{
								listVal += itemVal.ToString() + ", ";
							}
							listVal += "}";
							item.SubItems.Add(listVal);
							item.BackColor = Color.LightYellow;
						}
						else
							item.SubItems.Add(curVal.ToString());
					}
					if (output.ContainsValue(propDesc.DisplayName))
					{
						item.Checked = true;
						item.SubItems[1].Text = GetVarNameByPropDesc(propDesc.DisplayName);
					}
					propsListView.Items.Add(item);
				}
			}
		}

		private void FillOutput()
		{
			testcase.Scenario.Lists.RemoveAllByTestCase(testcase);
			testcase.Scenario.Variables.RemoveAllByTestCase(testcase);
			output.Clear();
			foreach (ListViewItem item in propsListView.Items)
			{
				if (item.Checked)
				{
					try
					{
						
						string varName = string.Empty;
						if (item.BackColor == Color.LightYellow)
						{
							QAliber.TestModel.Variables.ScenarioList sl = new QAliber.TestModel.Variables.ScenarioList(item.SubItems[1].Text, new string[] { }, testcase);
							testcase.Scenario.Lists.AddOrReplace(sl);
							output.Add(sl.Name, item.SubItems[0].Text);
						}
						else
						{
							QAliber.TestModel.Variables.ScenarioVariable sv = new QAliber.TestModel.Variables.ScenarioVariable(item.SubItems[1].Text, "", testcase);
							testcase.Scenario.Variables.AddOrReplace(sv);
							output.Add(sv.Name, item.SubItems[0].Text);
						}
					   
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Error Storing Properties");
					}

				}
				else
				{
					if (item.BackColor == Color.LightYellow)
					{
						testcase.Scenario.Lists.RemoveIfFound(
							new QAliber.TestModel.Variables.ScenarioList(item.SubItems[1].Text, new string[] { }, testcase));
					}
					else
					{
						testcase.Scenario.Variables.RemoveIfFound(
							new QAliber.TestModel.Variables.ScenarioVariable(item.SubItems[1].Text, "", testcase));
					}
				}
			}
		}

		private string GetVarNameByPropDesc(string propDesc)
		{
			foreach (string key in output.Keys)
			{
				if (output[key] == propDesc)
					return key;
			}
			return "";
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
							box.Tag = subItem;
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
			ListViewItem.ListViewSubItem subItem = box.Tag as ListViewItem.ListViewSubItem;
			subItem.Text = box.Text;
			
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
			Close();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			FillOutput();
			DialogResult = DialogResult.OK;
			Close();
		}
		#endregion

		private Dictionary<string, string> output = new Dictionary<string,string>();
		private TestCase testcase;

		

		

		

		
	}
}
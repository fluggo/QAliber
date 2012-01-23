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
using QAliber.TestModel.Variables;
using System.Collections;
using System.Text.RegularExpressions;

namespace QAliber.TestModel.TypeEditors
{
	public partial class VariablesWizardForm : Form
	{
		public VariablesWizardForm(TestScenario scenario, string val)
		{
			
			InitializeComponent();
			varsListView.Columns[0].Width = varsListView.Width - 10;
			this.scenario = scenario;
			
			DialogResult = DialogResult.OK;
			
			this.varTextBox.Text = val;
			this.varTextBox.SelectAll();

			
			
		}

		public string OutputString
		{
			get { return "$" + this.varTextBox.Text + "$"; }
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void varsListView_ItemActivate(object sender, EventArgs e)
		{
			TryToUpdateTextBox();
		}

		private void varsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			TryToUpdateTextBox();
		}

		private void varTextBox_TextChanged(object sender, EventArgs e)
		{
			string[] fields = varTextBox.Text.Split('.');
			Graphics g = Graphics.FromHwnd(varTextBox.Handle);
			g.PageUnit = GraphicsUnit.Pixel;
			varsListView.Location = new Point(
				varTextBox.Location.X + (int)g.MeasureString(varTextBox.Text, varTextBox.Font).Width,
				varTextBox.Bottom);
			if (fields.Length == 1)
			{
				InitVariablesList();
				varsListView.Visible = true;
				FindClosestMatch(varTextBox.Text);
			}

			if (fields.Length == 2)
			{
				InitPropertiesList(fields[0]);
				if (varsListView.Items.Count > 0)
				{
					varsListView.Visible = true;
					FindClosestMatch(fields[1]);
				}
			}
			if (fields.Length == 3 && fields[1] == "CurrentRow")
			{
				InitColumnsList(fields[0]);
				if (varsListView.Items.Count > 0)
				{
					varsListView.Visible = true;
					FindClosestMatch(fields[2]);
				}
			}
			TryToUpdateValueLabel();


		}
		
		private void varTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == 13)
			{
				e.Handled = true;
				TryToUpdateTextBox();
			}
			else if (e.KeyValue == 27)
			{
				varsListView.Visible = false;
			}
			else if (e.KeyValue == 40) //down
			{
				e.Handled = true;
				if (varsListView.SelectedIndices.Count > 0)
				{
					this.varsListView.SelectedIndexChanged -= new System.EventHandler(varsListView_SelectedIndexChanged);
					int index = varsListView.SelectedIndices[0] + 1;
					if (index < varsListView.Items.Count)
					{
						varsListView.Items[index].Selected = true;
						varsListView.Items[index].EnsureVisible();
					}
					this.varsListView.SelectedIndexChanged += new System.EventHandler(varsListView_SelectedIndexChanged);
				}
			}
			else if (e.KeyValue == 38) //up
			{
				e.Handled = true;
				if (varsListView.SelectedIndices.Count > 0)
				{
					this.varsListView.SelectedIndexChanged -= new System.EventHandler(varsListView_SelectedIndexChanged);
					int index = varsListView.SelectedIndices[0] - 1;
					if (index >= 0)
					{
						varsListView.Items[index].Selected = true;
						varsListView.Items[index].EnsureVisible();
					}
					this.varsListView.SelectedIndexChanged += new System.EventHandler(varsListView_SelectedIndexChanged);
				}
			}
		}

		private void FindClosestMatch(string str)
		{
			varsListView.SelectedIndexChanged -= new System.EventHandler(this.varsListView_SelectedIndexChanged);
			foreach (ListViewItem item in varsListView.Items)
			{
				if (item.Text.StartsWith(str))
				{
					item.Selected = true;
					break;
				}
			}
			varsListView.SelectedIndexChanged += new System.EventHandler(this.varsListView_SelectedIndexChanged);
		}

		private void InitVariablesList()
		{
			varsListView.Items.Clear();
			if (scenario.Variables != null)
			{
				foreach (var var in scenario.Variables)
				{
					varsListView.Items.Add(var.Name, var.Name, "Variable");
				}
			}
			if (scenario.Lists != null)
			{
				foreach (var var in scenario.Lists)
				{
					varsListView.Items.Add(var.Name, var.Name, "List");
				}
			}
			if (scenario.Tables != null)
			{
				foreach (var var in scenario.Tables)
				{
					varsListView.Items.Add(var.Name, var.Name, "Table");
				}
			}

		}

		private void InitPropertiesList(string var)
		{
			varsListView.Items.Clear();
			if (scenario.Lists[var] != null)
			{
				varsListView.Items.Add("Current", "Current", "Variable");
				varsListView.Items.Add("Length", "Length", "Variable");
			}
			else if (scenario.Tables[var] != null)
			{
				varsListView.Items.Add("CurrentRow", "CurrentRow", "Variable");
				varsListView.Items.Add("NumberOfRows", "NumberOfRows", "Variable");
				varsListView.Items.Add("NumberOfColumns", "NumberOfColumns", "Variable");
			}

		}

		private void InitColumnsList(string var)
		{
			varsListView.Items.Clear();
			if (scenario.Tables[var] != null)
			{
				foreach (DataColumn col in ((DataTable)scenario.Tables[var].Value).Columns)
				{
					varsListView.Items.Add(col.Caption, col.Caption, "Variable");
				}
			}
		}

		private void TryToUpdateValueLabel()
		{
			try
			{

				if (scenario.Variables[varTextBox.Text] != null)
				{
					curValDynLabel.Text = scenario.Variables[varTextBox.Text].Value.ToString();
					return;
				}
				Regex listRegex = new Regex(@"([^\[]+)\[([0-9]+)\]");
				Regex tableRegex = new Regex(@"([^\[]+)\[([0-9]+),([0-9]+)\]");
				Match match = listRegex.Match(varTextBox.Text);
				if (match.Success)
				{
					ScenarioVariable<string[]> list = scenario.Lists[match.Groups[1].Value];
					if (list != null)
					{
						int index = int.Parse(match.Groups[2].Value);
						string[] col = list.Value as string[];
						curValDynLabel.Text = col[index];
						return;
					}
				}
				match = tableRegex.Match(varTextBox.Text);
				if (match.Success)
				{
					ScenarioVariable<DataTable> table = scenario.Tables[match.Groups[1].Value];
					if (table != null)
					{
						int rowIndex = int.Parse(match.Groups[2].Value);
						int colIndex = int.Parse(match.Groups[3].Value);
						DataTable dataTable = table.Value as DataTable;
						curValDynLabel.Text = dataTable.Rows[rowIndex][colIndex].ToString();
						return;
					}
				}
				curValDynLabel.Text = "";
			}
			catch (Exception ex)
			{
				curValDynLabel.Text = ex.Message;
			}
		}

		private void TryToUpdateTextBox()
		{
			if (varsListView.Visible && varsListView.SelectedItems.Count > 0)
			{
				string text = varTextBox.Text;
				this.varTextBox.TextChanged -= new System.EventHandler(this.varTextBox_TextChanged);
				int endIndex = varTextBox.SelectionStart;
				int startIndex = varTextBox.Text.LastIndexOf('.');
				if (startIndex > endIndex)
					startIndex = endIndex;
				try
				{
					text = varTextBox.Text.Remove(startIndex + 1, endIndex - startIndex - 1);
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				text = text.Insert(startIndex + 1, varsListView.SelectedItems[0].Text);
				varTextBox.Text = text;
				
				varsListView.Visible = false;
				
				this.varTextBox.TextChanged += new System.EventHandler(this.varTextBox_TextChanged);
			}
		}


		private TestScenario scenario;

		private void varsListView_VisibleChanged(object sender, EventArgs e)
		{
			if (!varsListView.Visible)
			{
				varTextBox.Select(varTextBox.Text.Length, 0);
				//varTextBox.Select();
				varTextBox.Focus();
				
				
			}
		}

		

	   

		

		

		

		
		
		
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using QAliber.TestModel.Variables;

namespace QAliber.Builder.Presentation
{
	public partial class VariablesPanel : UserControl
	{
		public VariablesPanel(TabbedScenarioControl tabbedScenarioControl)
		{
			Visible = false;
			this.tabbedScenarioControl = tabbedScenarioControl;
			this.tabbedScenarioControl.tabbedDocumentControl.SelectedControlChanged += new EventHandler(tabbedDocumentControl_SelectedControlChanged);
			InitializeComponent();
			InitColumns();
		}

		private void InitColumns()
		{
			varsDataGridView.AutoGenerateColumns = false;
			listsDataGridView.AutoGenerateColumns = false;
			tablesDataGridView.AutoGenerateColumns = false;
			
			DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
			nameColumn.DataPropertyName = "Name";
			nameColumn.HeaderText = "Variable Name";

			DataGridViewTextBoxColumn valColumn = new DataGridViewTextBoxColumn();
			valColumn.DataPropertyName = "Value";
			valColumn.HeaderText = "Value";

			DataGridViewLinkColumn listValColumn = new DataGridViewLinkColumn();
			listValColumn.DataPropertyName = "Value";
			listValColumn.HeaderText = "List Values";
			
			DataGridViewLinkColumn tableValColumn = new DataGridViewLinkColumn();
			tableValColumn.DataPropertyName = "Value";
			tableValColumn.HeaderText = "Table Data";
			tableValColumn.DefaultCellStyle.NullValue = "Create New Table";

			DataGridViewLinkColumn definedByColumn = new DataGridViewLinkColumn();
			definedByColumn.DataPropertyName = "DefinedBy";
			definedByColumn.HeaderText = "Defined By";

			varsDataGridView.Columns.Add(nameColumn);
			varsDataGridView.Columns.Add(valColumn);
			varsDataGridView.Columns.Add(definedByColumn);

			listsDataGridView.Columns.Add((DataGridViewTextBoxColumn)nameColumn.Clone());
			listsDataGridView.Columns.Add(listValColumn);
			listsDataGridView.Columns.Add((DataGridViewLinkColumn)definedByColumn.Clone());

			tablesDataGridView.Columns.Add((DataGridViewTextBoxColumn)nameColumn.Clone());
			tablesDataGridView.Columns.Add(tableValColumn);
			tablesDataGridView.Columns.Add((DataGridViewLinkColumn)definedByColumn.Clone());

		}

		private void tabbedDocumentControl_SelectedControlChanged(object sender, EventArgs e)
		{
			sc = tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (sc != null && sc.TestScenario != null)
			{
				Visible = true;
				varsDataGridView.DataSource = sc.TestScenario.Variables;
				listsDataGridView.DataSource = sc.TestScenario.Lists;
				tablesDataGridView.DataSource = sc.TestScenario.Tables;
			}
			else
				Visible = false;
		}

		private void listsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1 && e.RowIndex >= 0)
			{
				ICollection list = listsDataGridView[e.ColumnIndex, e.RowIndex].Value as ICollection;
				ListItemsForm form = new ListItemsForm(list);
				if (DialogResult.OK == form.ShowDialog())
				{
					listsDataGridView[e.ColumnIndex, e.RowIndex].Value = form.Strings;
					if (listsDataGridView[0, e.RowIndex].Value.ToString() == defaultVar)
					{
						listsDataGridView.CurrentCell = listsDataGridView[0, e.RowIndex];
						listsDataGridView.BeginEdit(false);
						SendKeys.SendWait(index.ToString());
						listsDataGridView.EndEdit();
						listsDataGridView.BeginEdit(true);
						index++;
					}
					
				}
			}
			else if (e.ColumnIndex == 2 && e.RowIndex >= 0)
			{
				string val = listsDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
				System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(val, "[0-9]+");
				if (match.Success)
				{
					if (sc != null && sc.TestScenario != null)
					{
						sc.SelectNodeByID(int.Parse(match.Value));
					}
				}
			}
		}

		private void tablesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1 && e.RowIndex >= 0)
			{
				DataTable table = tablesDataGridView[e.ColumnIndex, e.RowIndex].Value as DataTable;
				EditTableForm form = new EditTableForm(table);
				if (DialogResult.OK == form.ShowDialog())
				{
					tablesDataGridView[e.ColumnIndex, e.RowIndex].Value = form.Table;
					if (tablesDataGridView[0, e.RowIndex].Value.ToString() == defaultVar)
					{
						tablesDataGridView.CurrentCell = tablesDataGridView[0, e.RowIndex];
						tablesDataGridView.BeginEdit(false);
						SendKeys.SendWait(index.ToString());
						tablesDataGridView.EndEdit();
						tablesDataGridView.BeginEdit(true);
						index++;
					}
				}
			}
			else if (e.ColumnIndex == 2 && e.RowIndex >= 0)
			{
				string val = tablesDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
				System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(val, "[0-9]+");
				if (match.Success)
				{
					if (sc != null && sc.TestScenario != null)
					{
						sc.SelectNodeByID(int.Parse(match.Value));
					}
				}
			}
		}

		private void varsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 2 && e.RowIndex >= 0)
			{
				string val = varsDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
				System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(val, "[0-9]+");
				if (match.Success)
				{
					if (sc != null && sc.TestScenario != null)
					{
						sc.SelectNodeByID(int.Parse(match.Value));
					}
				}
			}
		}

		private void varsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1 && varsDataGridView[1, e.RowIndex].Value != null)
			{
				if (varsDataGridView[0, e.RowIndex].Value == null)
				{
					varsDataGridView[0, e.RowIndex].Value = defaultVar + index.ToString();
					index++;

				}
			}
		}

		private void listsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				if (e.Value == null)
					e.Value = "Create New List";
				else
					e.Value = "List Items";

			}
		}

		
		private ScenarioControl sc;
		private TabbedScenarioControl tabbedScenarioControl;
		private string defaultVar = "ChangeThisName";
		private int index = 1;

	   

		

	   
		
	}
}

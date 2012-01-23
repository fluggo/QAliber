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
using System.Drawing;
using System.Data;
using System.Linq;
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

		private void HandleVariablesFormatting( object sender, DataGridViewCellFormattingEventArgs e ) {
			if( e.RowIndex < 0 || e.ColumnIndex == _variablesNameColumn.Index ||
					e.ColumnIndex == _variablesValueColumn.Index )
				return;

			var variable = (ScenarioVariable<string>) varsDataGridView.Rows[e.RowIndex].DataBoundItem;

			if( variable == null )
				return;

			if( e.ColumnIndex == _variablesTestStepColumn.Index ) {
				if( variable.TestStep == null )
					e.Value = string.Empty;
				else
					e.Value = variable.TestStep.Name;

				e.FormattingApplied = true;
			}
		}

		private void HandleVariablesDefaultValues( object sender, DataGridViewRowEventArgs e ) {
			e.Row.Cells[_variablesNameColumn.Index].Value = defaultVar;
			e.Row.Cells[_variablesValueColumn.Index].Value = string.Empty;
		}

		private void listsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == _listsTestStepColumn.Index && e.RowIndex >= 0)
			{
				var variable = (ScenarioVariable<string[]>) listsDataGridView.Rows[e.RowIndex].DataBoundItem;

				if( variable != null && variable.TestStep != null ) {
					if (sc != null && sc.TestScenario != null)
					{
						sc.SelectNodeByID( variable.TestStep.ID );
					}
				}
			}
		}

		private void HandleListsFormatting( object sender, DataGridViewCellFormattingEventArgs e ) {
			if( e.RowIndex < 0 || e.ColumnIndex == _listsNameColumn.Index )
				return;

			var variable = (ScenarioVariable<string[]>) listsDataGridView.Rows[e.RowIndex].DataBoundItem;

			if( variable == null )
				return;

			if( e.ColumnIndex == _listsValueColumn.Index ) {
				e.Value = "[" + string.Join( ", ", variable.Value ) + "]";
				e.FormattingApplied = true;
			}
			else if( e.ColumnIndex == _listsTestStepColumn.Index ) {
				if( variable.TestStep == null )
					e.Value = string.Empty;
				else
					e.Value = variable.TestStep.Name;

				e.FormattingApplied = true;
			}
		}

		private void HandleListsDefaultValues( object sender, DataGridViewRowEventArgs e ) {
			e.Row.Cells[_listsNameColumn.Index].Value = defaultVar;
			e.Row.Cells[_listsValueColumn.Index].Value = new string[0];
		}

		private void tablesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == _listsValueColumn.Index && e.RowIndex >= 0)
			{
				DataTable table = tablesDataGridView[e.ColumnIndex, e.RowIndex].Value as DataTable;

				if( table == null )
					table = new DataTable( "Table" );

				EditTableForm form = new EditTableForm(table);
				if (DialogResult.OK == form.ShowDialog())
				{
					tablesDataGridView[e.ColumnIndex, e.RowIndex].Value = form.Table;
				}
			}
			else if (e.ColumnIndex == _listsTestStepColumn.Index && e.RowIndex >= 0)
			{
				var variable = (ScenarioVariable<string[]>) tablesDataGridView.Rows[e.RowIndex].DataBoundItem;

				if( variable != null && variable.TestStep != null ) {
					if (sc != null && sc.TestScenario != null)
					{
						sc.SelectNodeByID( variable.TestStep.ID );
					}
				}
			}
		}

		private void HandleTablesFormatting( object sender, DataGridViewCellFormattingEventArgs e ) {
			if( e.RowIndex < 0 || e.ColumnIndex == _tablesNameColumn.Index )
				return;

			var variable = (ScenarioVariable<DataTable>) tablesDataGridView.Rows[e.RowIndex].DataBoundItem;

			if( variable == null )
				return;

			if( e.ColumnIndex == _tablesValueColumn.Index ) {
				e.Value = "[(" +
					string.Join( ",", variable.Value.Columns.Cast<DataColumn>().Select( col => col.Caption ) ) + ")" +
					string.Concat( variable.Value.Rows.Cast<DataRow>().Select( row =>
						", (" + string.Join( ",", variable.Value.Columns.Cast<DataColumn>().Select(
							col => row.Field<string>( col ) ) ) + ")" ) ) + "]";
				e.FormattingApplied = true;
			}
			else if( e.ColumnIndex == _tablesTestStepColumn.Index ) {
				if( variable.TestStep == null )
					e.Value = string.Empty;
				else
					e.Value = variable.TestStep.Name;

				e.FormattingApplied = true;
			}
		}

		private void HandleTablesDefaultValues( object sender, DataGridViewRowEventArgs e ) {
			e.Row.Cells[_tablesNameColumn.Index].Value = defaultVar;
			e.Row.Cells[_tablesValueColumn.Index].Value = new DataTable( "Data Table" );
		}

		private void varsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == _variablesTestStepColumn.Index && e.RowIndex >= 0)
			{
				var variable = (ScenarioVariable<string[]>) varsDataGridView.Rows[e.RowIndex].DataBoundItem;

				if( variable != null && variable.TestStep != null ) {
					if (sc != null && sc.TestScenario != null)
					{
						sc.SelectNodeByID( variable.TestStep.ID );
					}
				}
			}
		}

		private ScenarioControl sc;
		private TabbedScenarioControl tabbedScenarioControl;
		private const string defaultVar = "ChangeThisName";
	}
}

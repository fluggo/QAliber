using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace QAliber.Builder.Presentation
{
	public partial class EditTableForm : Form
	{
		public EditTableForm(DataTable table)
		{
			this.table = table;
			DialogResult = DialogResult.Cancel;
			InitializeComponent();
			tableDataGridView.DataSource = table;
			BlockSort();
		}

		public DataTable Table
		{
			get { return table; }
			set 
			{ 
				table = value;
				tableDataGridView.DataSource = table;
			}
		}

		private void BlockSort()
		{
			foreach (DataGridViewColumn column in tableDataGridView.Columns)
			{
				column.SortMode = DataGridViewColumnSortMode.NotSortable;
			}
		}
	
		
		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void insertColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string headerName = "Header 0";
			int i = 0;
			while (table.Columns.Contains(headerName))
			{
				i++;
				headerName = "Header " + i;
			}
			table.Columns.Add(headerName);

			tableDataGridView.Refresh();
			BlockSort();
		}

		private void removeColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tableDataGridView.CurrentCell != null && tableDataGridView.CurrentCell.ColumnIndex >= 0)
			{
				//tableDataGridView.Columns.RemoveAt(tableDataGridView.CurrentCell.ColumnIndex);
				table.Columns.RemoveAt(tableDataGridView.CurrentCell.ColumnIndex);
				tableDataGridView.Refresh();
			}
		}

		private void tableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 && e.ColumnIndex >= 0)
			{
				TextBox box = new TextBox();
				box.Leave += new EventHandler(box_Leave);
				box.Bounds = tableDataGridView.GetCellDisplayRectangle(e.ColumnIndex, 0, false);
				box.Bounds = new Rectangle(box.Bounds.X, 0, box.Bounds.Width, box.Bounds.Height);
				tableDataGridView.Controls.Add(box);
				box.Text = tableDataGridView.Columns[e.ColumnIndex].HeaderText;
				box.Focus();

				columnHeaderChanging = tableDataGridView.Columns[e.ColumnIndex].HeaderText;
			}
		}

		private void box_Leave(object sender, EventArgs e)
		{
			TextBox box = (TextBox)sender;
			if (!string.IsNullOrEmpty(box.Text))
			{
				try
				{
					table.Columns[columnHeaderChanging].ColumnName = table.Columns[columnHeaderChanging].Caption = box.Text;
					tableDataGridView.Controls.Remove(box);
					tableDataGridView.Refresh();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			else
			{
				tableDataGridView.Controls.Remove(box);
				tableDataGridView.Refresh();
			}
		}


		private DataTable table;
		private string columnHeaderChanging;

	   
		

		
	}
}
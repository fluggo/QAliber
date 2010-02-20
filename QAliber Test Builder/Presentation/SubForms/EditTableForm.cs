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
			if (columnIndexRightClicked >= 0)
			{
				table.Columns.RemoveAt(columnIndexRightClicked);
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

		private void tableDataGridView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				columnIndexRightClicked = tableDataGridView.HitTest(e.X, e.Y).ColumnIndex;
				if (columnIndexRightClicked >= 0)
				{
					removeColumnToolStripMenuItem.Visible = true;
					removeColumnToolStripMenuItem.Text = "Remove Column '" + tableDataGridView.Columns[columnIndexRightClicked].HeaderText + "'";

				}
				else
				{
					removeColumnToolStripMenuItem.Visible = false;
				}

			}
		}

		private void tableDataGridView_Paint(object sender, PaintEventArgs e)
		{
			if (tableDataGridView.Columns.Count == 0)
			{
				string str = "Right click this grid to add / remove columns to the table";
				SizeF size = e.Graphics.MeasureString(str, this.Font);
				e.Graphics.DrawString(str, this.Font, Brushes.White, new PointF(
					(tableDataGridView.Width - size.Width) / 2, (tableDataGridView.Height - size.Height) / 2));
			}
			else if (tableDataGridView.Rows.Count < 3)
			{
				string str = "To change the column names simply click on the column headers";
				SizeF size = e.Graphics.MeasureString(str, this.Font);
				e.Graphics.DrawString(str, this.Font, Brushes.White, new PointF(
					(tableDataGridView.Width - size.Width) / 2, (tableDataGridView.Height - size.Height) / 2));
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
		private int columnIndexRightClicked = -1;

	   
		

		

	   
		

		
	}
}
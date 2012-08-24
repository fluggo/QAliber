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
		DataTable _originalTable, _table;

		private string columnHeaderChanging;
		private int columnIndexRightClicked = -1;

		public EditTableForm(DataTable table)
		{
			_originalTable = table;

			// Clone the original table
			_table = table.Clone();
			_table.TableName = "TableVarName";
			_table.Load( table.CreateDataReader() );

			DialogResult = DialogResult.Cancel;
			InitializeComponent();

			tableDataGridView.DataSource = _table;
			BlockSort();
		}

		public DataTable Table
		{
			get { return (DialogResult == DialogResult.OK) ? _table : _originalTable; }
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
			_table.AcceptChanges();
			DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void insertColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string headerName = "Header 0";
			int i = 0;
			while( _table.Columns.Contains( headerName ) )
			{
				i++;
				headerName = "Header " + i;
			}
			_table.Columns.Add(headerName);

			BlockSort();
		}

		private void removeColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (columnIndexRightClicked >= 0)
			{
				_table.Columns.RemoveAt( columnIndexRightClicked );
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
				this.Controls.Add(box);
				box.BringToFront();
				box.Text = tableDataGridView.Columns[e.ColumnIndex].HeaderText;
				box.Focus();

				// Hand focus back to data grid view on Enter or Escape
				box.PreviewKeyDown += (senderBox, ev) => {
					if( ev.KeyCode == Keys.Enter || ev.KeyCode == Keys.Escape )
						ev.IsInputKey = true;
				};

				box.KeyDown += (senderBox, ev) => {
					if( ev.KeyCode == Keys.Enter ) {
						ev.Handled = true;
						tableDataGridView.Focus();
					}

					if( ev.KeyCode == Keys.Escape ) {
						ev.Handled = true;
						box.Text = string.Empty;
						tableDataGridView.Focus();
					}
				};

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
					_table.Columns[columnHeaderChanging].Caption = box.Text;
					_table.Columns[columnHeaderChanging].ColumnName = box.Text;

					this.Controls.Remove( box );
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			else
			{
				this.Controls.Remove( box );
			}

			BlockSort();
		}

	}
}

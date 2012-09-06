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
 
namespace QAliber.Builder.Presentation
{
	partial class EditTableForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tableDataGridView = new System.Windows.Forms.DataGridView();
			this.gridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.insertColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.tableDataGridView)).BeginInit();
			this.gridContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableDataGridView
			// 
			this.tableDataGridView.AllowUserToOrderColumns = true;
			this.tableDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tableDataGridView.ContextMenuStrip = this.gridContextMenuStrip;
			this.tableDataGridView.Location = new System.Drawing.Point(0, 1);
			this.tableDataGridView.Name = "tableDataGridView";
			this.tableDataGridView.Size = new System.Drawing.Size(424, 245);
			this.tableDataGridView.TabIndex = 0;
			this.tableDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableDataGridView_CellContentClick);
			this.tableDataGridView.Paint += new System.Windows.Forms.PaintEventHandler(this.tableDataGridView_Paint);
			this.tableDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tableDataGridView_MouseDown);
			// 
			// gridContextMenuStrip
			// 
			this.gridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertColumnToolStripMenuItem,
            this.removeColumnToolStripMenuItem});
			this.gridContextMenuStrip.Name = "gridContextMenuStrip";
			this.gridContextMenuStrip.Size = new System.Drawing.Size(152, 48);
			// 
			// insertColumnToolStripMenuItem
			// 
			this.insertColumnToolStripMenuItem.Name = "insertColumnToolStripMenuItem";
			this.insertColumnToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.insertColumnToolStripMenuItem.Text = "Insert Column";
			this.insertColumnToolStripMenuItem.Click += new System.EventHandler(this.insertColumnToolStripMenuItem_Click);
			// 
			// removeColumnToolStripMenuItem
			// 
			this.removeColumnToolStripMenuItem.Name = "removeColumnToolStripMenuItem";
			this.removeColumnToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.removeColumnToolStripMenuItem.Text = "Remove Column";
			this.removeColumnToolStripMenuItem.Click += new System.EventHandler(this.removeColumnToolStripMenuItem_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(337, 252);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(255, 252);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// EditTableForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(424, 287);
			this.Controls.Add(this.tableDataGridView);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "EditTableForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Table";
			((System.ComponentModel.ISupportInitialize)(this.tableDataGridView)).EndInit();
			this.gridContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DataGridView tableDataGridView;
		private System.Windows.Forms.ContextMenuStrip gridContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem insertColumnToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeColumnToolStripMenuItem;
	}
}
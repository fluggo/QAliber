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
	partial class VariablesPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.varsTabControl = new System.Windows.Forms.TabControl();
			this.varsTabPage = new System.Windows.Forms.TabPage();
			this.varsDataGridView = new System.Windows.Forms.DataGridView();
			this.listsTabPage = new System.Windows.Forms.TabPage();
			this.listsDataGridView = new System.Windows.Forms.DataGridView();
			this._listsNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._listsValueColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this._listsTestStepColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this.tablesTabPage = new System.Windows.Forms.TabPage();
			this.tablesDataGridView = new System.Windows.Forms.DataGridView();
			this._tablesNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._tablesValueColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this._tablesTestStepColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this._variablesNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._variablesValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._variablesTestStepColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this.varsTabControl.SuspendLayout();
			this.varsTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.varsDataGridView)).BeginInit();
			this.listsTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.listsDataGridView)).BeginInit();
			this.tablesTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tablesDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// varsTabControl
			// 
			this.varsTabControl.Controls.Add(this.varsTabPage);
			this.varsTabControl.Controls.Add(this.listsTabPage);
			this.varsTabControl.Controls.Add(this.tablesTabPage);
			this.varsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.varsTabControl.Location = new System.Drawing.Point(0, 0);
			this.varsTabControl.Name = "varsTabControl";
			this.varsTabControl.SelectedIndex = 0;
			this.varsTabControl.Size = new System.Drawing.Size(551, 214);
			this.varsTabControl.TabIndex = 0;
			// 
			// varsTabPage
			// 
			this.varsTabPage.Controls.Add(this.varsDataGridView);
			this.varsTabPage.Location = new System.Drawing.Point(4, 22);
			this.varsTabPage.Name = "varsTabPage";
			this.varsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.varsTabPage.Size = new System.Drawing.Size(543, 188);
			this.varsTabPage.TabIndex = 0;
			this.varsTabPage.Text = "Variables";
			this.varsTabPage.UseVisualStyleBackColor = true;
			// 
			// varsDataGridView
			// 
			this.varsDataGridView.AllowUserToOrderColumns = true;
			this.varsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.varsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.varsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this._variablesNameColumn,
			this._variablesValueColumn,
			this._variablesTestStepColumn});
			this.varsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.varsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.varsDataGridView.Location = new System.Drawing.Point(3, 3);
			this.varsDataGridView.Name = "varsDataGridView";
			this.varsDataGridView.Size = new System.Drawing.Size(537, 182);
			this.varsDataGridView.TabIndex = 0;
			this.varsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.varsDataGridView_CellContentClick);
			this.varsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.HandleVariablesFormatting);
			this.varsDataGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.HandleVariablesDefaultValues);
			// 
			// listsTabPage
			// 
			this.listsTabPage.Controls.Add(this.listsDataGridView);
			this.listsTabPage.Location = new System.Drawing.Point(4, 22);
			this.listsTabPage.Name = "listsTabPage";
			this.listsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.listsTabPage.Size = new System.Drawing.Size(543, 188);
			this.listsTabPage.TabIndex = 1;
			this.listsTabPage.Text = "Lists";
			this.listsTabPage.UseVisualStyleBackColor = true;
			// 
			// listsDataGridView
			// 
			this.listsDataGridView.AllowUserToOrderColumns = true;
			this.listsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.listsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.listsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this._listsNameColumn,
			this._listsValueColumn,
			this._listsTestStepColumn});
			this.listsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.listsDataGridView.Location = new System.Drawing.Point(3, 3);
			this.listsDataGridView.Name = "listsDataGridView";
			this.listsDataGridView.Size = new System.Drawing.Size(537, 182);
			this.listsDataGridView.TabIndex = 0;
			this.listsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listsDataGridView_CellContentClick);
			this.listsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.HandleListsFormatting);
			this.listsDataGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.HandleListsDefaultValues);
			// 
			// _listsNameColumn
			// 
			this._listsNameColumn.DataPropertyName = "Name";
			this._listsNameColumn.HeaderText = "Name";
			this._listsNameColumn.Name = "_listsNameColumn";
			// 
			// _listsValueColumn
			// 
			this._listsValueColumn.DataPropertyName = "Value";
			this._listsValueColumn.HeaderText = "Value";
			this._listsValueColumn.Name = "_listsValueColumn";
			this._listsValueColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this._listsValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this._listsValueColumn.TrackVisitedState = false;
			// 
			// _listsTestStepColumn
			// 
			this._listsTestStepColumn.DataPropertyName = "TestStep";
			this._listsTestStepColumn.HeaderText = "Defined By";
			this._listsTestStepColumn.Name = "_listsTestStepColumn";
			this._listsTestStepColumn.ReadOnly = true;
			this._listsTestStepColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this._listsTestStepColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this._listsTestStepColumn.TrackVisitedState = false;
			// 
			// tablesTabPage
			// 
			this.tablesTabPage.Controls.Add(this.tablesDataGridView);
			this.tablesTabPage.Location = new System.Drawing.Point(4, 22);
			this.tablesTabPage.Name = "tablesTabPage";
			this.tablesTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.tablesTabPage.Size = new System.Drawing.Size(543, 188);
			this.tablesTabPage.TabIndex = 2;
			this.tablesTabPage.Text = "Tables";
			this.tablesTabPage.UseVisualStyleBackColor = true;
			// 
			// tablesDataGridView
			// 
			this.tablesDataGridView.AllowUserToOrderColumns = true;
			this.tablesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.tablesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tablesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this._tablesNameColumn,
			this._tablesValueColumn,
			this._tablesTestStepColumn});
			this.tablesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tablesDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.tablesDataGridView.Location = new System.Drawing.Point(3, 3);
			this.tablesDataGridView.Name = "tablesDataGridView";
			this.tablesDataGridView.Size = new System.Drawing.Size(537, 182);
			this.tablesDataGridView.TabIndex = 1;
			this.tablesDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablesDataGridView_CellContentClick);
			this.tablesDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.HandleTablesFormatting);
			this.tablesDataGridView.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.HandleTablesDefaultValues);
			// 
			// _tablesNameColumn
			// 
			this._tablesNameColumn.DataPropertyName = "Name";
			this._tablesNameColumn.HeaderText = "Name";
			this._tablesNameColumn.Name = "_tablesNameColumn";
			// 
			// _tablesValueColumn
			// 
			this._tablesValueColumn.DataPropertyName = "Value";
			this._tablesValueColumn.HeaderText = "Value";
			this._tablesValueColumn.Name = "_tablesValueColumn";
			this._tablesValueColumn.TrackVisitedState = false;
			// 
			// _tablesTestStepColumn
			// 
			this._tablesTestStepColumn.DataPropertyName = "TestStep";
			this._tablesTestStepColumn.HeaderText = "Defined By";
			this._tablesTestStepColumn.Name = "_tablesTestStepColumn";
			this._tablesTestStepColumn.ReadOnly = true;
			this._tablesTestStepColumn.TrackVisitedState = false;
			// 
			// _variablesNameColumn
			// 
			this._variablesNameColumn.DataPropertyName = "Name";
			this._variablesNameColumn.HeaderText = "Name";
			this._variablesNameColumn.Name = "_variablesNameColumn";
			// 
			// _variablesValueColumn
			// 
			this._variablesValueColumn.DataPropertyName = "Value";
			this._variablesValueColumn.HeaderText = "Value";
			this._variablesValueColumn.Name = "_variablesValueColumn";
			// 
			// _variablesTestStepColumn
			// 
			this._variablesTestStepColumn.DataPropertyName = "TestStep";
			this._variablesTestStepColumn.HeaderText = "Defined By";
			this._variablesTestStepColumn.Name = "_variablesTestStepColumn";
			this._variablesTestStepColumn.ReadOnly = true;
			this._variablesTestStepColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this._variablesTestStepColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this._variablesTestStepColumn.TrackVisitedState = false;
			// 
			// VariablesPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.varsTabControl);
			this.Name = "VariablesPanel";
			this.Size = new System.Drawing.Size(551, 214);
			this.varsTabControl.ResumeLayout(false);
			this.varsTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.varsDataGridView)).EndInit();
			this.listsTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.listsDataGridView)).EndInit();
			this.tablesTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tablesDataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl varsTabControl;
		private System.Windows.Forms.TabPage varsTabPage;
		private System.Windows.Forms.DataGridView varsDataGridView;
		private System.Windows.Forms.TabPage listsTabPage;
		private System.Windows.Forms.DataGridView listsDataGridView;
		private System.Windows.Forms.TabPage tablesTabPage;
		private System.Windows.Forms.DataGridView tablesDataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn _listsNameColumn;
		private System.Windows.Forms.DataGridViewLinkColumn _listsValueColumn;
		private System.Windows.Forms.DataGridViewLinkColumn _listsTestStepColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _tablesNameColumn;
		private System.Windows.Forms.DataGridViewLinkColumn _tablesValueColumn;
		private System.Windows.Forms.DataGridViewLinkColumn _tablesTestStepColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _variablesNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _variablesValueColumn;
		private System.Windows.Forms.DataGridViewLinkColumn _variablesTestStepColumn;
	}
}

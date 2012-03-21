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
 
namespace QAliber.Repository.CommonTestCases.UITypeEditors
{
	partial class UIControlLocatorForm
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
			this.btnCursor = new System.Windows.Forms.Button();
			this.textBox = new System.Windows.Forms.TextBox();
			this.labelHelp = new System.Windows.Forms.Label();
			this.labelPath = new System.Windows.Forms.Label();
			this.labelCoord = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.textBoxXY = new System.Windows.Forms.TextBox();
			this.radioButtonUIA = new System.Windows.Forms.RadioButton();
			this.radioButtonWeb = new System.Windows.Forms.RadioButton();
			this._timer = new System.Windows.Forms.Timer(this.components);
			this._cancelButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this._hierarchyList = new System.Windows.Forms.ListView();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this._controlPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// btnCursor
			// 
			this.btnCursor.BackgroundImage = global::QAliber.Repository.CommonTestCases.Properties.Resources.Crosshair;
			this.btnCursor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnCursor.Location = new System.Drawing.Point(485, 26);
			this.btnCursor.Name = "btnCursor";
			this.btnCursor.Size = new System.Drawing.Size(42, 42);
			this.btnCursor.TabIndex = 2;
			this.btnCursor.UseVisualStyleBackColor = true;
			this.btnCursor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCursor_MouseDown);
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(94, 48);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(385, 20);
			this.textBox.TabIndex = 4;
			// 
			// labelHelp
			// 
			this.labelHelp.AutoSize = true;
			this.labelHelp.Location = new System.Drawing.Point(91, 9);
			this.labelHelp.Name = "labelHelp";
			this.labelHelp.Size = new System.Drawing.Size(249, 13);
			this.labelHelp.TabIndex = 0;
			this.labelHelp.Text = "Drag the crosshair to the control you want to target.";
			// 
			// labelPath
			// 
			this.labelPath.AutoSize = true;
			this.labelPath.Location = new System.Drawing.Point(12, 51);
			this.labelPath.Name = "labelPath";
			this.labelPath.Size = new System.Drawing.Size(67, 13);
			this.labelPath.TabIndex = 3;
			this.labelPath.Text = "&Control path:";
			// 
			// labelCoord
			// 
			this.labelCoord.AutoSize = true;
			this.labelCoord.Location = new System.Drawing.Point(12, 84);
			this.labelCoord.Name = "labelCoord";
			this.labelCoord.Size = new System.Drawing.Size(75, 13);
			this.labelCoord.TabIndex = 5;
			this.labelCoord.Text = "&Relative point:";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(370, 283);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 13;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// textBoxXY
			// 
			this.textBoxXY.Location = new System.Drawing.Point(94, 81);
			this.textBoxXY.Name = "textBoxXY";
			this.textBoxXY.ReadOnly = true;
			this.textBoxXY.Size = new System.Drawing.Size(100, 20);
			this.textBoxXY.TabIndex = 6;
			this.textBoxXY.Text = "X,Y";
			// 
			// radioButtonUIA
			// 
			this.radioButtonUIA.AutoSize = true;
			this.radioButtonUIA.Checked = true;
			this.radioButtonUIA.Location = new System.Drawing.Point(319, 84);
			this.radioButtonUIA.Name = "radioButtonUIA";
			this.radioButtonUIA.Size = new System.Drawing.Size(89, 17);
			this.radioButtonUIA.TabIndex = 7;
			this.radioButtonUIA.TabStop = true;
			this.radioButtonUIA.Text = "&UIAutomation";
			this.radioButtonUIA.UseVisualStyleBackColor = true;
			// 
			// radioButtonWeb
			// 
			this.radioButtonWeb.AutoSize = true;
			this.radioButtonWeb.Location = new System.Drawing.Point(424, 84);
			this.radioButtonWeb.Name = "radioButtonWeb";
			this.radioButtonWeb.Size = new System.Drawing.Size(102, 17);
			this.radioButtonWeb.TabIndex = 8;
			this.radioButtonWeb.Text = "&Internet Explorer";
			this.radioButtonWeb.UseVisualStyleBackColor = true;
			// 
			// _timer
			// 
			this._timer.Interval = 1000;
			this._timer.Tick += new System.EventHandler(this.HandleTimerTick);
			// 
			// _cancelButton
			// 
			this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Location = new System.Drawing.Point(451, 283);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new System.Drawing.Size(75, 23);
			this._cancelButton.TabIndex = 14;
			this._cancelButton.Text = "Cancel";
			this._cancelButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(91, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(340, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "You can also press Scroll Lock to capture the control under the cursor.";
			// 
			// _hierarchyList
			// 
			this._hierarchyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1});
			this._hierarchyList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this._hierarchyList.Location = new System.Drawing.Point(15, 132);
			this._hierarchyList.Name = "_hierarchyList";
			this._hierarchyList.Size = new System.Drawing.Size(189, 135);
			this._hierarchyList.TabIndex = 10;
			this._hierarchyList.UseCompatibleStateImageBehavior = false;
			this._hierarchyList.View = System.Windows.Forms.View.Details;
			this._hierarchyList.SelectedIndexChanged += new System.EventHandler(this.HandleHierarchyControlSelected);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Control &hierarchy:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(227, 116);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "&Properties:";
			// 
			// _controlPropertyGrid
			// 
			this._controlPropertyGrid.CausesValidation = false;
			this._controlPropertyGrid.HelpVisible = false;
			this._controlPropertyGrid.Location = new System.Drawing.Point(230, 132);
			this._controlPropertyGrid.Name = "_controlPropertyGrid";
			this._controlPropertyGrid.Size = new System.Drawing.Size(229, 135);
			this._controlPropertyGrid.TabIndex = 12;
			this._controlPropertyGrid.ToolbarVisible = false;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 163;
			// 
			// UIControlLocatorForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._cancelButton;
			this.ClientSize = new System.Drawing.Size(538, 318);
			this.Controls.Add(this._controlPropertyGrid);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._hierarchyList);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._cancelButton);
			this.Controls.Add(this.radioButtonWeb);
			this.Controls.Add(this.radioButtonUIA);
			this.Controls.Add(this.textBoxXY);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.labelCoord);
			this.Controls.Add(this.labelPath);
			this.Controls.Add(this.labelHelp);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.btnCursor);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UIControlLocatorForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Control Locator";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCursor;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.Label labelHelp;
		private System.Windows.Forms.Label labelPath;
		private System.Windows.Forms.Label labelCoord;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox textBoxXY;
		private System.Windows.Forms.RadioButton radioButtonUIA;
		private System.Windows.Forms.RadioButton radioButtonWeb;
		private System.Windows.Forms.Timer _timer;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView _hierarchyList;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PropertyGrid _controlPropertyGrid;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}
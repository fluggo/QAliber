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
 
namespace QAliber.VS2005.Plugin.Aliases
{
	partial class ManageAliasesForm
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
			this.btnOk = new System.Windows.Forms.Button();
			this.txtAlias = new System.Windows.Forms.TextBox();
			this.lblAliasName = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.chkSupressLog = new System.Windows.Forms.CheckBox();
			this.radioReturnNull = new System.Windows.Forms.RadioButton();
			this.radioThrowException = new System.Windows.Forms.RadioButton();
			this.cmbAliasClass = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(311, 197);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// txtAlias
			// 
			this.txtAlias.Location = new System.Drawing.Point(84, 37);
			this.txtAlias.Name = "txtAlias";
			this.txtAlias.Size = new System.Drawing.Size(155, 20);
			this.txtAlias.TabIndex = 2;
			// 
			// lblAliasName
			// 
			this.lblAliasName.AutoSize = true;
			this.lblAliasName.Location = new System.Drawing.Point(12, 40);
			this.lblAliasName.Name = "lblAliasName";
			this.lblAliasName.Size = new System.Drawing.Size(66, 13);
			this.lblAliasName.TabIndex = 0;
			this.lblAliasName.Text = "Alias Name :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Class Name :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(121, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "On Element Not Found :";
			// 
			// chkSupressLog
			// 
			this.chkSupressLog.AutoSize = true;
			this.chkSupressLog.Location = new System.Drawing.Point(46, 98);
			this.chkSupressLog.Name = "chkSupressLog";
			this.chkSupressLog.Size = new System.Drawing.Size(94, 17);
			this.chkSupressLog.TabIndex = 3;
			this.chkSupressLog.Text = "Supress Log ?";
			this.chkSupressLog.UseVisualStyleBackColor = true;
			// 
			// radioReturnNull
			// 
			this.radioReturnNull.AutoSize = true;
			this.radioReturnNull.Checked = true;
			this.radioReturnNull.Location = new System.Drawing.Point(46, 122);
			this.radioReturnNull.Name = "radioReturnNull";
			this.radioReturnNull.Size = new System.Drawing.Size(78, 17);
			this.radioReturnNull.TabIndex = 4;
			this.radioReturnNull.TabStop = true;
			this.radioReturnNull.Text = "Return Null";
			this.radioReturnNull.UseVisualStyleBackColor = true;
			// 
			// radioThrowException
			// 
			this.radioThrowException.AutoSize = true;
			this.radioThrowException.Location = new System.Drawing.Point(46, 145);
			this.radioThrowException.Name = "radioThrowException";
			this.radioThrowException.Size = new System.Drawing.Size(105, 17);
			this.radioThrowException.TabIndex = 5;
			this.radioThrowException.TabStop = true;
			this.radioThrowException.Text = "Throw Exception";
			this.radioThrowException.UseVisualStyleBackColor = true;
			// 
			// cmbAliasClass
			// 
			this.cmbAliasClass.FormattingEnabled = true;
			this.cmbAliasClass.Location = new System.Drawing.Point(84, 9);
			this.cmbAliasClass.Name = "cmbAliasClass";
			this.cmbAliasClass.Size = new System.Drawing.Size(155, 21);
			this.cmbAliasClass.TabIndex = 7;
			this.cmbAliasClass.Text = "Aliases";
			// 
			// ManageAliasesForm
			// 
			this.ClientSize = new System.Drawing.Size(398, 232);
			this.Controls.Add(this.cmbAliasClass);
			this.Controls.Add(this.radioThrowException);
			this.Controls.Add(this.radioReturnNull);
			this.Controls.Add(this.chkSupressLog);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblAliasName);
			this.Controls.Add(this.txtAlias);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ManageAliasesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Enter Alias Name";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtAlias;
		private System.Windows.Forms.Label lblAliasName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkSupressLog;
		private System.Windows.Forms.RadioButton radioReturnNull;
		private System.Windows.Forms.RadioButton radioThrowException;
		private System.Windows.Forms.ComboBox cmbAliasClass;


	}
}
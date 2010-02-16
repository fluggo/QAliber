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
 
namespace QAliber.Builder.Presentation.SubForms
{
	partial class BuilderOptionsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuilderOptionsForm));
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.lblCustomLocation = new System.Windows.Forms.Label();
			this.textBoxFolder = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.btnOk = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageLog = new System.Windows.Forms.TabPage();
			this.checkBoxShowLog = new System.Windows.Forms.CheckBox();
			this.groupBoxLogLocation = new System.Windows.Forms.GroupBox();
			this.labelLogLocation = new System.Windows.Forms.Label();
			this.textLogLocation = new System.Windows.Forms.TextBox();
			this.btnBrowseLog = new System.Windows.Forms.Button();
			this.lblLogFormat = new System.Windows.Forms.Label();
			this.txtLogFormat = new System.Windows.Forms.TextBox();
			this.tabPageEngine = new System.Windows.Forms.TabPage();
			this.checkBoxMinimizeOnRun = new System.Windows.Forms.CheckBox();
			this.engineOptionsPane = new QAliber.Engine.EngineOptionsPane();
			this.btnCancel = new System.Windows.Forms.Button();
			this.copyLocalCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabPageLog.SuspendLayout();
			this.groupBoxLogLocation.SuspendLayout();
			this.tabPageEngine.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.copyLocalCheckBox);
			this.groupBox.Controls.Add(this.lblCustomLocation);
			this.groupBox.Controls.Add(this.textBoxFolder);
			this.groupBox.Controls.Add(this.btnBrowse);
			this.groupBox.Location = new System.Drawing.Point(8, 6);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(297, 121);
			this.groupBox.TabIndex = 1;
			this.groupBox.TabStop = false;
			this.groupBox.Text = "Test Repository Options";
			// 
			// lblCustomLocation
			// 
			this.lblCustomLocation.AutoSize = true;
			this.lblCustomLocation.Location = new System.Drawing.Point(6, 28);
			this.lblCustomLocation.Name = "lblCustomLocation";
			this.lblCustomLocation.Size = new System.Drawing.Size(140, 13);
			this.lblCustomLocation.TabIndex = 2;
			this.lblCustomLocation.Text = "Central Test Cases Location";
			// 
			// textBoxFolder
			// 
			this.textBoxFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::QAliber.Builder.Presentation.Properties.Settings.Default, "TestCasesAssemblyDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textBoxFolder.Location = new System.Drawing.Point(9, 44);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.Size = new System.Drawing.Size(242, 20);
			this.textBoxFolder.TabIndex = 1;
			this.textBoxFolder.Text = global::QAliber.Builder.Presentation.Properties.Settings.Default.TestCasesAssemblyDir;
			// 
			// btnBrowse
			// 
			this.btnBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowse.BackgroundImage")));
			this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnBrowse.Location = new System.Drawing.Point(257, 42);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(34, 23);
			this.btnBrowse.TabIndex = 0;
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(85, 409);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageLog);
			this.tabControl.Controls.Add(this.tabPageEngine);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(322, 403);
			this.tabControl.TabIndex = 3;
			// 
			// tabPageLog
			// 
			this.tabPageLog.Controls.Add(this.checkBoxShowLog);
			this.tabPageLog.Controls.Add(this.groupBoxLogLocation);
			this.tabPageLog.Location = new System.Drawing.Point(4, 22);
			this.tabPageLog.Name = "tabPageLog";
			this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageLog.Size = new System.Drawing.Size(314, 377);
			this.tabPageLog.TabIndex = 1;
			this.tabPageLog.Text = "Log";
			this.tabPageLog.UseVisualStyleBackColor = true;
			// 
			// checkBoxShowLog
			// 
			this.checkBoxShowLog.AutoSize = true;
			this.checkBoxShowLog.Checked = global::QAliber.Builder.Presentation.Properties.Settings.Default.ShowLogAfter;
			this.checkBoxShowLog.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::QAliber.Builder.Presentation.Properties.Settings.Default, "ShowLogAfter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBoxShowLog.Location = new System.Drawing.Point(17, 165);
			this.checkBoxShowLog.Name = "checkBoxShowLog";
			this.checkBoxShowLog.Size = new System.Drawing.Size(122, 17);
			this.checkBoxShowLog.TabIndex = 3;
			this.checkBoxShowLog.Text = "Show Log After Run";
			this.checkBoxShowLog.UseVisualStyleBackColor = true;
			// 
			// groupBoxLogLocation
			// 
			this.groupBoxLogLocation.Controls.Add(this.labelLogLocation);
			this.groupBoxLogLocation.Controls.Add(this.textLogLocation);
			this.groupBoxLogLocation.Controls.Add(this.btnBrowseLog);
			this.groupBoxLogLocation.Controls.Add(this.lblLogFormat);
			this.groupBoxLogLocation.Controls.Add(this.txtLogFormat);
			this.groupBoxLogLocation.Location = new System.Drawing.Point(8, 16);
			this.groupBoxLogLocation.Name = "groupBoxLogLocation";
			this.groupBoxLogLocation.Size = new System.Drawing.Size(297, 132);
			this.groupBoxLogLocation.TabIndex = 2;
			this.groupBoxLogLocation.TabStop = false;
			this.groupBoxLogLocation.Text = "Location";
			// 
			// labelLogLocation
			// 
			this.labelLogLocation.AutoSize = true;
			this.labelLogLocation.Location = new System.Drawing.Point(6, 25);
			this.labelLogLocation.Name = "labelLogLocation";
			this.labelLogLocation.Size = new System.Drawing.Size(69, 13);
			this.labelLogLocation.TabIndex = 5;
			this.labelLogLocation.Text = "Log Location";
			// 
			// textLogLocation
			// 
			this.textLogLocation.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::QAliber.Builder.Presentation.Properties.Settings.Default, "LogLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textLogLocation.Location = new System.Drawing.Point(9, 41);
			this.textLogLocation.Name = "textLogLocation";
			this.textLogLocation.Size = new System.Drawing.Size(242, 20);
			this.textLogLocation.TabIndex = 4;
			this.textLogLocation.Text = global::QAliber.Builder.Presentation.Properties.Settings.Default.LogLocation;
			// 
			// btnBrowseLog
			// 
			this.btnBrowseLog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowseLog.BackgroundImage")));
			this.btnBrowseLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnBrowseLog.Location = new System.Drawing.Point(257, 39);
			this.btnBrowseLog.Name = "btnBrowseLog";
			this.btnBrowseLog.Size = new System.Drawing.Size(34, 23);
			this.btnBrowseLog.TabIndex = 3;
			this.btnBrowseLog.UseVisualStyleBackColor = true;
			this.btnBrowseLog.Click += new System.EventHandler(this.btnBrowseLog_Click);
			// 
			// lblLogFormat
			// 
			this.lblLogFormat.AutoSize = true;
			this.lblLogFormat.Location = new System.Drawing.Point(6, 78);
			this.lblLogFormat.Name = "lblLogFormat";
			this.lblLogFormat.Size = new System.Drawing.Size(92, 13);
			this.lblLogFormat.TabIndex = 2;
			this.lblLogFormat.Text = "Log Folder Fromat";
			// 
			// txtLogFormat
			// 
			this.txtLogFormat.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::QAliber.Builder.Presentation.Properties.Settings.Default, "LogStructure", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.txtLogFormat.Location = new System.Drawing.Point(9, 94);
			this.txtLogFormat.Name = "txtLogFormat";
			this.txtLogFormat.Size = new System.Drawing.Size(282, 20);
			this.txtLogFormat.TabIndex = 1;
			this.txtLogFormat.Text = global::QAliber.Builder.Presentation.Properties.Settings.Default.LogStructure;
			// 
			// tabPageEngine
			// 
			this.tabPageEngine.Controls.Add(this.checkBoxMinimizeOnRun);
			this.tabPageEngine.Controls.Add(this.groupBox);
			this.tabPageEngine.Controls.Add(this.engineOptionsPane);
			this.tabPageEngine.Location = new System.Drawing.Point(4, 22);
			this.tabPageEngine.Name = "tabPageEngine";
			this.tabPageEngine.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageEngine.Size = new System.Drawing.Size(314, 377);
			this.tabPageEngine.TabIndex = 0;
			this.tabPageEngine.Text = "Engine";
			this.tabPageEngine.UseVisualStyleBackColor = true;
			// 
			// checkBoxMinimizeOnRun
			// 
			this.checkBoxMinimizeOnRun.AutoSize = true;
			this.checkBoxMinimizeOnRun.Checked = global::QAliber.Builder.Presentation.Properties.Settings.Default.MinimizeOnRun;
			this.checkBoxMinimizeOnRun.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxMinimizeOnRun.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::QAliber.Builder.Presentation.Properties.Settings.Default, "MinimizeOnRun", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBoxMinimizeOnRun.Location = new System.Drawing.Point(8, 354);
			this.checkBoxMinimizeOnRun.Name = "checkBoxMinimizeOnRun";
			this.checkBoxMinimizeOnRun.Size = new System.Drawing.Size(106, 17);
			this.checkBoxMinimizeOnRun.TabIndex = 2;
			this.checkBoxMinimizeOnRun.Text = "Minimize On Run";
			this.checkBoxMinimizeOnRun.UseVisualStyleBackColor = true;
			// 
			// engineOptionsPane
			// 
			this.engineOptionsPane.Location = new System.Drawing.Point(7, 133);
			this.engineOptionsPane.Name = "engineOptionsPane";
			this.engineOptionsPane.Size = new System.Drawing.Size(298, 206);
			this.engineOptionsPane.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(166, 409);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// copyLocalCheckBox
			// 
			this.copyLocalCheckBox.AutoSize = true;
			this.copyLocalCheckBox.Checked = global::QAliber.Builder.Presentation.Properties.Settings.Default.CopyIfNewerTestCasesAssemblies;
			this.copyLocalCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.copyLocalCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::QAliber.Builder.Presentation.Properties.Settings.Default, "CopyIfNewerTestCasesAssemblies", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.copyLocalCheckBox.Location = new System.Drawing.Point(9, 80);
			this.copyLocalCheckBox.Name = "copyLocalCheckBox";
			this.copyLocalCheckBox.Size = new System.Drawing.Size(186, 17);
			this.copyLocalCheckBox.TabIndex = 3;
			this.copyLocalCheckBox.Text = "Copy locally only if newer versions";
			this.copyLocalCheckBox.UseVisualStyleBackColor = true;
			// 
			// BuilderOptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(322, 444);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "BuilderOptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.tabControl.ResumeLayout(false);
			this.tabPageLog.ResumeLayout(false);
			this.tabPageLog.PerformLayout();
			this.groupBoxLogLocation.ResumeLayout(false);
			this.groupBoxLogLocation.PerformLayout();
			this.tabPageEngine.ResumeLayout(false);
			this.tabPageEngine.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private QAliber.Engine.EngineOptionsPane engineOptionsPane;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.TextBox textBoxFolder;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label lblCustomLocation;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageLog;
		private System.Windows.Forms.TabPage tabPageEngine;
		private System.Windows.Forms.GroupBox groupBoxLogLocation;
		private System.Windows.Forms.Label lblLogFormat;
		private System.Windows.Forms.TextBox txtLogFormat;
		private System.Windows.Forms.Label labelLogLocation;
		private System.Windows.Forms.TextBox textLogLocation;
		private System.Windows.Forms.Button btnBrowseLog;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox checkBoxShowLog;
		private System.Windows.Forms.CheckBox checkBoxMinimizeOnRun;
		private System.Windows.Forms.CheckBox copyLocalCheckBox;
	}
}
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
 
namespace QAliber.VS2005.Plugin
{
	partial class SpyControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpyControl));
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageControls = new System.Windows.Forms.ImageList(this.components);
			this.nodeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.highlightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addAliasMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.winFormsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripRecord = new System.Windows.Forms.ToolStripButton();
			this.toolStripStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripCapture = new System.Windows.Forms.ToolStripButton();
			this.toolStripComboBoxSpyAs = new System.Windows.Forms.ToolStripComboBox();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.splitContainerMain = new System.Windows.Forms.SplitContainer();
			this.splitContainerRight = new System.Windows.Forms.SplitContainer();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.richTextBox = new System.Windows.Forms.RichTextBox();
			this.groupBoxLang = new System.Windows.Forms.GroupBox();
			this.radioButtonCSharp = new System.Windows.Forms.RadioButton();
			this.radioButtonVB = new System.Windows.Forms.RadioButton();
			this.groupBoxCode = new System.Windows.Forms.GroupBox();
			this.nodeContextMenu.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.splitContainerMain.Panel1.SuspendLayout();
			this.splitContainerMain.Panel2.SuspendLayout();
			this.splitContainerMain.SuspendLayout();
			this.splitContainerRight.Panel1.SuspendLayout();
			this.splitContainerRight.Panel2.SuspendLayout();
			this.splitContainerRight.SuspendLayout();
			this.groupBoxLang.SuspendLayout();
			this.groupBoxCode.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.HideSelection = false;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.imageControls;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = 0;
			this.treeView.Size = new System.Drawing.Size(255, 709);
			this.treeView.TabIndex = 0;
			this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeExpand);
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
			// 
			// imageControls
			// 
			this.imageControls.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageControls.ImageStream")));
			this.imageControls.TransparentColor = System.Drawing.Color.Magenta;
			this.imageControls.Images.SetKeyName(0, "browser.bmp");
			this.imageControls.Images.SetKeyName(1, "button.bmp");
			this.imageControls.Images.SetKeyName(2, "checkbox.bmp");
			this.imageControls.Images.SetKeyName(3, "combobox.bmp");
			this.imageControls.Images.SetKeyName(4, "control.bmp");
			this.imageControls.Images.SetKeyName(5, "editbox.bmp");
			this.imageControls.Images.SetKeyName(6, "image.bmp");
			this.imageControls.Images.SetKeyName(7, "label.bmp");
			this.imageControls.Images.SetKeyName(8, "link.bmp");
			this.imageControls.Images.SetKeyName(9, "list.bmp");
			this.imageControls.Images.SetKeyName(10, "menu.bmp");
			this.imageControls.Images.SetKeyName(11, "panel.bmp");
			this.imageControls.Images.SetKeyName(12, "progressbar.bmp");
			this.imageControls.Images.SetKeyName(13, "radiobutton.bmp");
			this.imageControls.Images.SetKeyName(14, "tab.bmp");
			this.imageControls.Images.SetKeyName(15, "treeview.bmp");
			this.imageControls.Images.SetKeyName(16, "table.bmp");
			this.imageControls.Images.SetKeyName(17, "window.bmp");
			this.imageControls.Images.SetKeyName(18, "notexists.bmp");
			// 
			// nodeContextMenu
			// 
			this.nodeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.highlightMenuItem,
			this.viewImageMenuItem,
			this.addAliasMenuItem,
			this.refreshToolStripMenuItem,
			this.toolStripSeparator2,
			this.winFormsToolStripMenuItem});
			this.nodeContextMenu.Name = "nodeContextMenu";
			this.nodeContextMenu.Size = new System.Drawing.Size(211, 120);
			this.nodeContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.nodeContextMenu_Opening);
			// 
			// highlightMenuItem
			// 
			this.highlightMenuItem.Name = "highlightMenuItem";
			this.highlightMenuItem.Size = new System.Drawing.Size(210, 22);
			this.highlightMenuItem.Text = "Highlight Control";
			this.highlightMenuItem.Click += new System.EventHandler(this.highlightMenuItem_Click);
			// 
			// viewImageMenuItem
			// 
			this.viewImageMenuItem.Name = "viewImageMenuItem";
			this.viewImageMenuItem.Size = new System.Drawing.Size(210, 22);
			this.viewImageMenuItem.Text = "Image Viewer ...";
			this.viewImageMenuItem.Click += new System.EventHandler(this.viewImageMenuItem_Click);
			// 
			// addAliasMenuItem
			// 
			this.addAliasMenuItem.Name = "addAliasMenuItem";
			this.addAliasMenuItem.Size = new System.Drawing.Size(210, 22);
			this.addAliasMenuItem.Text = "Add Alias ...";
			this.addAliasMenuItem.Visible = false;
			this.addAliasMenuItem.Click += new System.EventHandler(this.addAliasMenuItem_Click);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(207, 6);
			// 
			// winFormsToolStripMenuItem
			// 
			this.winFormsToolStripMenuItem.Enabled = false;
			this.winFormsToolStripMenuItem.Name = "winFormsToolStripMenuItem";
			this.winFormsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
			this.winFormsToolStripMenuItem.Text = "Display WinForms Properties";
			this.winFormsToolStripMenuItem.Click += new System.EventHandler(this.winFormsToolStripMenuItem_Click);
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripRecord,
			this.toolStripStop,
			this.toolStripSeparator1,
			this.toolStripCapture,
			this.toolStripComboBoxSpyAs});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(630, 39);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip";
			// 
			// toolStripRecord
			// 
			this.toolStripRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripRecord.Image = global::QAliber.VS2005.Plugin.Resources.Record;
			this.toolStripRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripRecord.Name = "toolStripRecord";
			this.toolStripRecord.Size = new System.Drawing.Size(36, 36);
			this.toolStripRecord.Text = "UI Recorder";
			this.toolStripRecord.ToolTipText = "Record User Actions (CTRL+SHIFT+4)";
			this.toolStripRecord.Click += new System.EventHandler(this.toolStripRecord_Click);
			// 
			// toolStripStop
			// 
			this.toolStripStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripStop.Enabled = false;
			this.toolStripStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStop.Image")));
			this.toolStripStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripStop.Name = "toolStripStop";
			this.toolStripStop.Size = new System.Drawing.Size(36, 36);
			this.toolStripStop.ToolTipText = "Stop Recording User Actions (CTRL+SHIFT+6)";
			this.toolStripStop.Click += new System.EventHandler(this.toolStripStop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
			// 
			// toolStripCapture
			// 
			this.toolStripCapture.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripCapture.Image = global::QAliber.VS2005.Plugin.Resources.Crosshair;
			this.toolStripCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripCapture.Name = "toolStripCapture";
			this.toolStripCapture.Size = new System.Drawing.Size(36, 36);
			this.toolStripCapture.ToolTipText = "Control Finder";
			this.toolStripCapture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripCapture_MouseDown);
			// 
			// toolStripComboBoxSpyAs
			// 
			this.toolStripComboBoxSpyAs.Items.AddRange(new object[] {
			"UI Automation",
			"Web (DOM)"});
			this.toolStripComboBoxSpyAs.Name = "toolStripComboBoxSpyAs";
			this.toolStripComboBoxSpyAs.Size = new System.Drawing.Size(121, 39);
			this.toolStripComboBoxSpyAs.ToolTipText = "Spy & Record Controls Method";
			this.toolStripComboBoxSpyAs.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSpyAs_SelectedIndexChanged);
			// 
			// notifyIcon
			// 
			this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon.BalloonTipText = "Your actions are now being recorded, to stop recording please press CTRL+SHIFT+6";
			this.notifyIcon.BalloonTipTitle = "Recording...";
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Record In Progress";
			// 
			// splitContainerMain
			// 
			this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMain.Location = new System.Drawing.Point(0, 39);
			this.splitContainerMain.Name = "splitContainerMain";
			// 
			// splitContainerMain.Panel1
			// 
			this.splitContainerMain.Panel1.Controls.Add(this.treeView);
			// 
			// splitContainerMain.Panel2
			// 
			this.splitContainerMain.Panel2.Controls.Add(this.splitContainerRight);
			this.splitContainerMain.Size = new System.Drawing.Size(630, 709);
			this.splitContainerMain.SplitterDistance = 255;
			this.splitContainerMain.TabIndex = 2;
			// 
			// splitContainerRight
			// 
			this.splitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerRight.Location = new System.Drawing.Point(0, 0);
			this.splitContainerRight.Name = "splitContainerRight";
			this.splitContainerRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerRight.Panel1
			// 
			this.splitContainerRight.Panel1.Controls.Add(this.propertyGrid);
			// 
			// splitContainerRight.Panel2
			// 
			this.splitContainerRight.Panel2.Controls.Add(this.groupBoxCode);
			this.splitContainerRight.Panel2.Controls.Add(this.groupBoxLang);
			
			this.splitContainerRight.Size = new System.Drawing.Size(371, 709);
			this.splitContainerRight.SplitterDistance = 387;
			this.splitContainerRight.TabIndex = 0;
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(371, 387);
			this.propertyGrid.TabIndex = 0;
			// 
			// richTextBox
			// 
			this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox.Location = new System.Drawing.Point(3, 16);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.Size = new System.Drawing.Size(365, 299);
			this.richTextBox.TabIndex = 0;
			this.richTextBox.Text = "";
			// 
			// groupBoxLang
			// 
			this.groupBoxLang.Controls.Add(this.radioButtonVB);
			this.groupBoxLang.Controls.Add(this.radioButtonCSharp);
			this.groupBoxLang.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxLang.Location = new System.Drawing.Point(0, 0);
			this.groupBoxLang.Name = "groupBoxLang";
			this.groupBoxLang.Size = new System.Drawing.Size(371, 45);
			this.groupBoxLang.TabIndex = 1;
			this.groupBoxLang.TabStop = false;
			this.groupBoxLang.Text = "Language";
			// 
			// radioButtonCSharp
			// 
			this.radioButtonCSharp.AutoSize = true;
			this.radioButtonCSharp.Checked = true;
			this.radioButtonCSharp.Location = new System.Drawing.Point(32, 19);
			this.radioButtonCSharp.Name = "radioButtonCSharp";
			this.radioButtonCSharp.Size = new System.Drawing.Size(39, 17);
			this.radioButtonCSharp.TabIndex = 0;
			this.radioButtonCSharp.TabStop = true;
			this.radioButtonCSharp.Text = "C#";
			this.radioButtonCSharp.UseVisualStyleBackColor = true;
			this.radioButtonCSharp.CheckedChanged += new System.EventHandler(this.radioButtonCSharp_CheckedChanged);
			// 
			// radioButtonVB
			// 
			this.radioButtonVB.AutoSize = true;
			this.radioButtonVB.Location = new System.Drawing.Point(99, 19);
			this.radioButtonVB.Name = "radioButtonVB";
			this.radioButtonVB.Size = new System.Drawing.Size(64, 17);
			this.radioButtonVB.TabIndex = 2;
			this.radioButtonVB.Text = "VB.NET";
			this.radioButtonVB.UseVisualStyleBackColor = true;
			this.radioButtonVB.CheckedChanged += new System.EventHandler(this.radioButtonVB_CheckedChanged);
			// 
			// groupBoxCode
			// 
			this.groupBoxCode.Controls.Add(this.richTextBox);
			this.groupBoxCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxCode.Location = new System.Drawing.Point(0, 0);
			this.groupBoxCode.Name = "groupBoxCode";
			this.groupBoxCode.Size = new System.Drawing.Size(371, 318);
			this.groupBoxCode.TabIndex = 2;
			this.groupBoxCode.TabStop = false;
			this.groupBoxCode.Text = "Generated Code";
			// 
			// SpyControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainerMain);
			this.Controls.Add(this.toolStrip);
			this.Name = "SpyControl";
			this.Size = new System.Drawing.Size(630, 748);
			this.nodeContextMenu.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.splitContainerMain.Panel1.ResumeLayout(false);
			this.splitContainerMain.Panel2.ResumeLayout(false);
			this.splitContainerMain.ResumeLayout(false);
			this.splitContainerRight.Panel1.ResumeLayout(false);
			this.splitContainerRight.Panel2.ResumeLayout(false);
			this.splitContainerRight.ResumeLayout(false);
			this.groupBoxLang.ResumeLayout(false);
			this.groupBoxLang.PerformLayout();
			this.groupBoxCode.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		
		#endregion

		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.ContextMenuStrip nodeContextMenu;
		private System.Windows.Forms.ToolStripMenuItem highlightMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripRecord;
		private System.Windows.Forms.ToolStripButton toolStripStop;
		private System.Windows.Forms.ImageList imageControls;
		private System.Windows.Forms.ToolStripButton toolStripCapture;
		internal System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ToolStripMenuItem viewImageMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addAliasMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSpyAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem winFormsToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainerMain;
		private System.Windows.Forms.SplitContainer splitContainerRight;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		internal System.Windows.Forms.RichTextBox richTextBox;
		private System.Windows.Forms.GroupBox groupBoxLang;
		private System.Windows.Forms.RadioButton radioButtonVB;
		private System.Windows.Forms.RadioButton radioButtonCSharp;
		private System.Windows.Forms.GroupBox groupBoxCode;
	}
}


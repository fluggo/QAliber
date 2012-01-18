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
	partial class ScenarioControl
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScenarioControl));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.testcaseIconsList = new System.Windows.Forms.ImageList(this.components);
			this.testCasesPG = new System.Windows.Forms.PropertyGrid();
			this.pgMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.variablesWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testCasesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.playCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SetBPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addTestCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuCategorize = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripBlue = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripPurple = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripGreen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripOrange = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripRed = new System.Windows.Forms.ToolStripMenuItem();
			this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scenarioTreeView = new QAliber.Builder.Presentation.QAliberTreeView();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.pgMenuStrip.SuspendLayout();
			this.testCasesMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.scenarioTreeView);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.testCasesPG);
			this.splitContainer.Size = new System.Drawing.Size(582, 487);
			this.splitContainer.SplitterDistance = 194;
			this.splitContainer.TabIndex = 0;
			// 
			// testcaseIconsList
			// 
			this.testcaseIconsList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("testcaseIconsList.ImageStream")));
			this.testcaseIconsList.TransparentColor = System.Drawing.Color.Magenta;
			this.testcaseIconsList.Images.SetKeyName(0, "Generic");
			this.testcaseIconsList.Images.SetKeyName(1, "InProgress");
			this.testcaseIconsList.Images.SetKeyName(2, "InBreakpoint");
			// 
			// testCasesPG
			// 
			this.testCasesPG.BackColor = System.Drawing.Color.White;
			this.testCasesPG.CategoryForeColor = System.Drawing.Color.SteelBlue;
			this.testCasesPG.ContextMenuStrip = this.pgMenuStrip;
			this.testCasesPG.Dock = System.Windows.Forms.DockStyle.Fill;
			this.testCasesPG.HelpBackColor = System.Drawing.Color.GhostWhite;
			this.testCasesPG.HelpForeColor = System.Drawing.Color.Navy;
			this.testCasesPG.LineColor = System.Drawing.Color.White;
			this.testCasesPG.Location = new System.Drawing.Point(0, 0);
			this.testCasesPG.Name = "testCasesPG";
			this.testCasesPG.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.testCasesPG.Size = new System.Drawing.Size(384, 487);
			this.testCasesPG.TabIndex = 0;
			this.testCasesPG.ViewBackColor = System.Drawing.Color.AliceBlue;
			this.testCasesPG.ViewForeColor = System.Drawing.Color.DarkBlue;
			this.testCasesPG.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.AfterValueChanged);
			// 
			// pgMenuStrip
			// 
			this.pgMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.variablesWizardToolStripMenuItem});
			this.pgMenuStrip.Name = "pgMenuStrip";
			this.pgMenuStrip.Size = new System.Drawing.Size(169, 26);
			// 
			// variablesWizardToolStripMenuItem
			// 
			this.variablesWizardToolStripMenuItem.Name = "variablesWizardToolStripMenuItem";
			this.variablesWizardToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.variablesWizardToolStripMenuItem.Text = "Variables Wizard ...";
			this.variablesWizardToolStripMenuItem.Click += new System.EventHandler(this.variablesWizardToolStripMenuItem_Click);
			// 
			// testCasesMenu
			// 
			this.testCasesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.playCurrentToolStripMenuItem,
			this.SetBPToolStripMenuItem,
			this.addTestCaseToolStripMenuItem,
			this.addFolderToolStripMenuItem,
			this.toolStripSeparator3,
			this.toolStripMenuCategorize,
			this.toolStripSeparator2,
			this.moveUpToolStripMenuItem,
			this.moveDownToolStripMenuItem,
			this.toolStripSeparator1,
			this.cutToolStripMenuItem,
			this.copyToolStripMenuItem,
			this.pasteToolStripMenuItem,
			this.deleteToolStripMenuItem,
			this.toolStripSeparator5,
			this.helpToolStripMenuItem});
			this.testCasesMenu.Name = "testCasesMenu";
			this.testCasesMenu.Size = new System.Drawing.Size(186, 314);
			// 
			// playCurrentToolStripMenuItem
			// 
			this.playCurrentToolStripMenuItem.Image = global::QAliber.Builder.Presentation.Properties.Resources.DebugPlay;
			this.playCurrentToolStripMenuItem.Name = "playCurrentToolStripMenuItem";
			this.playCurrentToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.playCurrentToolStripMenuItem.Text = "Play Current Test Case";
			this.playCurrentToolStripMenuItem.Click += new System.EventHandler(this.playCurrentToolStripMenuItem_Click);
			// 
			// SetBPToolStripMenuItem
			// 
			this.SetBPToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SetBPToolStripMenuItem.Image")));
			this.SetBPToolStripMenuItem.Name = "SetBPToolStripMenuItem";
			this.SetBPToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.SetBPToolStripMenuItem.Text = "Toggle Breakpoint";
			this.SetBPToolStripMenuItem.Click += new System.EventHandler(this.SetBPToolStripMenuItem_Click);
			// 
			// addTestCaseToolStripMenuItem
			// 
			this.addTestCaseToolStripMenuItem.Name = "addTestCaseToolStripMenuItem";
			this.addTestCaseToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.addTestCaseToolStripMenuItem.Text = "Add Test Case";
			this.addTestCaseToolStripMenuItem.Click += new System.EventHandler(this.MenuAddTestCaseClicked);
			// 
			// addFolderToolStripMenuItem
			// 
			this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
			this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.addFolderToolStripMenuItem.Text = "Add Folder";
			this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.MenuAddFolderClicked);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(182, 6);
			// 
			// toolStripMenuCategorize
			// 
			this.toolStripMenuCategorize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripBlue,
			this.toolStripPurple,
			this.toolStripGreen,
			this.toolStripOrange,
			this.toolStripRed,
			this.noneToolStripMenuItem});
			this.toolStripMenuCategorize.Name = "toolStripMenuCategorize";
			this.toolStripMenuCategorize.Size = new System.Drawing.Size(185, 22);
			this.toolStripMenuCategorize.Text = "Categorize";
			// 
			// toolStripBlue
			// 
			this.toolStripBlue.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBlue.Image")));
			this.toolStripBlue.Name = "toolStripBlue";
			this.toolStripBlue.Size = new System.Drawing.Size(158, 22);
			this.toolStripBlue.Text = "Blue Category";
			this.toolStripBlue.Click += new System.EventHandler(this.toolStripBlue_Click);
			// 
			// toolStripPurple
			// 
			this.toolStripPurple.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPurple.Image")));
			this.toolStripPurple.Name = "toolStripPurple";
			this.toolStripPurple.Size = new System.Drawing.Size(158, 22);
			this.toolStripPurple.Text = "Purple Category";
			this.toolStripPurple.Click += new System.EventHandler(this.toolStripPurple_Click);
			// 
			// toolStripGreen
			// 
			this.toolStripGreen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripGreen.Image")));
			this.toolStripGreen.Name = "toolStripGreen";
			this.toolStripGreen.Size = new System.Drawing.Size(158, 22);
			this.toolStripGreen.Text = "Green Category";
			this.toolStripGreen.Click += new System.EventHandler(this.toolStripGreen_Click);
			// 
			// toolStripOrange
			// 
			this.toolStripOrange.Image = ((System.Drawing.Image)(resources.GetObject("toolStripOrange.Image")));
			this.toolStripOrange.Name = "toolStripOrange";
			this.toolStripOrange.Size = new System.Drawing.Size(158, 22);
			this.toolStripOrange.Text = "Orange Category";
			this.toolStripOrange.Click += new System.EventHandler(this.toolStripOrange_Click);
			// 
			// toolStripRed
			// 
			this.toolStripRed.Image = ((System.Drawing.Image)(resources.GetObject("toolStripRed.Image")));
			this.toolStripRed.Name = "toolStripRed";
			this.toolStripRed.Size = new System.Drawing.Size(158, 22);
			this.toolStripRed.Text = "Red Category";
			this.toolStripRed.Click += new System.EventHandler(this.toolStripRed_Click);
			// 
			// noneToolStripMenuItem
			// 
			this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
			this.noneToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.noneToolStripMenuItem.Text = "None";
			this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(182, 6);
			// 
			// moveUpToolStripMenuItem
			// 
			this.moveUpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("moveUpToolStripMenuItem.Image")));
			this.moveUpToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
			this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.moveUpToolStripMenuItem.Text = "Move Up";
			this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.MenuMoveUpClicked);
			// 
			// moveDownToolStripMenuItem
			// 
			this.moveDownToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("moveDownToolStripMenuItem.Image")));
			this.moveDownToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
			this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.moveDownToolStripMenuItem.Text = "Move Down";
			this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.MenuMoveDownClicked);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.MenuCutClicked);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.MenuCopyClicked);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.MenuPasteClicked);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
			this.deleteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.MenuDeleteClicked);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(182, 6);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripMenuItem.Image")));
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.helpToolStripMenuItem.Text = "Help...";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
			// 
			// scenarioTreeView
			// 
			this.scenarioTreeView.AllowDrop = true;
			this.scenarioTreeView.CheckBoxes = true;
			this.scenarioTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scenarioTreeView.EnableComplexCheck = true;
			this.scenarioTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.scenarioTreeView.HideSelection = false;
			this.scenarioTreeView.ImageIndex = 0;
			this.scenarioTreeView.ImageList = this.testcaseIconsList;
			this.scenarioTreeView.Location = new System.Drawing.Point(0, 0);
			this.scenarioTreeView.Name = "scenarioTreeView";
			this.scenarioTreeView.SelectedImageIndex = 0;
			this.scenarioTreeView.SelectedNodes = new QAliber.Builder.Presentation.QAliberTreeNode[0];
			this.scenarioTreeView.Size = new System.Drawing.Size(194, 487);
			this.scenarioTreeView.TabIndex = 0;
			this.scenarioTreeView.NodeDragged += new System.EventHandler<QAliber.Builder.Presentation.NodeDraggedEventArgs>(this.AfterDragEnded);
			this.scenarioTreeView.TestCaseDragged += new System.EventHandler<QAliber.Builder.Presentation.NodeDraggedEventArgs>(this.AfterTypeDragEnded);
			this.scenarioTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.AfterCheckChanged);
			this.scenarioTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AfterTreeNodeSelected);
			// 
			// ScenarioControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Name = "ScenarioControl";
			this.Size = new System.Drawing.Size(582, 487);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.pgMenuStrip.ResumeLayout(false);
			this.testCasesMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private QAliberTreeView scenarioTreeView;
		private System.Windows.Forms.PropertyGrid testCasesPG;
		private System.Windows.Forms.ContextMenuStrip testCasesMenu;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ImageList testcaseIconsList;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem playCurrentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SetBPToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip pgMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem variablesWizardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuCategorize;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem toolStripBlue;
		private System.Windows.Forms.ToolStripMenuItem toolStripPurple;
		private System.Windows.Forms.ToolStripMenuItem toolStripGreen;
		private System.Windows.Forms.ToolStripMenuItem toolStripOrange;
		private System.Windows.Forms.ToolStripMenuItem toolStripRed;
		private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addTestCaseToolStripMenuItem;
	}
}

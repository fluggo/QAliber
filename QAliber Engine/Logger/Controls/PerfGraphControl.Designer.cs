namespace QAliber.Logger.Controls
{
	partial class PerfGraphControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerfGraphControl));
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.listViewCounters = new System.Windows.Forms.ListView();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabelCounters = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonPlot = new System.Windows.Forms.ToolStripButton();
			this.zedGraphControl = new ZedGraph.ZedGraphControl();
			this.contextMenuStripList = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.contextMenuStripList.SuspendLayout();
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
			this.splitContainer.Panel1.Controls.Add(this.listViewCounters);
			this.splitContainer.Panel1.Controls.Add(this.toolStrip);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.zedGraphControl);
			this.splitContainer.Size = new System.Drawing.Size(548, 539);
			this.splitContainer.SplitterDistance = 191;
			this.splitContainer.TabIndex = 0;
			// 
			// listViewCounters
			// 
			this.listViewCounters.CheckBoxes = true;
			this.listViewCounters.ContextMenuStrip = this.contextMenuStripList;
			this.listViewCounters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewCounters.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.listViewCounters.GridLines = true;
			this.listViewCounters.Location = new System.Drawing.Point(0, 25);
			this.listViewCounters.MultiSelect = false;
			this.listViewCounters.Name = "listViewCounters";
			this.listViewCounters.ShowItemToolTips = true;
			this.listViewCounters.Size = new System.Drawing.Size(191, 514);
			this.listViewCounters.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewCounters.TabIndex = 0;
			this.listViewCounters.UseCompatibleStateImageBehavior = false;
			this.listViewCounters.View = System.Windows.Forms.View.List;
			this.listViewCounters.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewCounters_ItemChecked);
			this.listViewCounters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewCounters_ItemCheck);
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripLabelCounters,
			this.toolStripSeparator1,
			this.toolStripButtonPlot});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(191, 25);
			this.toolStrip.TabIndex = 2;
			// 
			// toolStripLabelCounters
			// 
			this.toolStripLabelCounters.Name = "toolStripLabelCounters";
			this.toolStripLabelCounters.Size = new System.Drawing.Size(51, 22);
			this.toolStripLabelCounters.Text = "Counters";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButtonPlot
			// 
			this.toolStripButtonPlot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonPlot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPlot.Image")));
			this.toolStripButtonPlot.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonPlot.Name = "toolStripButtonPlot";
			this.toolStripButtonPlot.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonPlot.ToolTipText = "Plot Graphs";
			this.toolStripButtonPlot.Click += new System.EventHandler(this.toolStripButtonPlot_Click);
			// 
			// zedGraphControl
			// 
			this.zedGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.zedGraphControl.Location = new System.Drawing.Point(0, 0);
			this.zedGraphControl.Name = "zedGraphControl";
			this.zedGraphControl.ScrollGrace = 0;
			this.zedGraphControl.ScrollMaxX = 0;
			this.zedGraphControl.ScrollMaxY = 0;
			this.zedGraphControl.ScrollMaxY2 = 0;
			this.zedGraphControl.ScrollMinX = 0;
			this.zedGraphControl.ScrollMinY = 0;
			this.zedGraphControl.ScrollMinY2 = 0;
			this.zedGraphControl.Size = new System.Drawing.Size(353, 539);
			this.zedGraphControl.TabIndex = 0;
			this.zedGraphControl.Click += new System.EventHandler(this.zedGraphControl_Click);
			this.zedGraphControl.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zedGraphControl_ContextMenuBuilder);
			// 
			// contextMenuStripList
			// 
			this.contextMenuStripList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.selectAllToolStripMenuItem,
			this.clearAllToolStripMenuItem});
			this.contextMenuStripList.Name = "contextMenuStripList";
			this.contextMenuStripList.Size = new System.Drawing.Size(229, 70);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.selectAllToolStripMenuItem.Text = "Select All (Maximum 8 Counters)";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// clearAllToolStripMenuItem
			// 
			this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
			this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.clearAllToolStripMenuItem.Text = "Clear All";
			this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
			// 
			// PerfGraphControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Name = "PerfGraphControl";
			this.Size = new System.Drawing.Size(548, 539);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.contextMenuStripList.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ListView listViewCounters;
		private ZedGraph.ZedGraphControl zedGraphControl;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabelCounters;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButtonPlot;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripList;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
	}
}

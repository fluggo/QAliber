namespace QAliber.Logger.Controls
{
	partial class LogViewerControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewerControl));
			this.logTree = new System.Windows.Forms.TreeView();
			this.imageTree = new System.Windows.Forms.ImageList(this.components);
			this.splitContainerMain = new System.Windows.Forms.SplitContainer();
			this.logTreeFlattened = new System.Windows.Forms.TreeView();
			this.logTreeFiltered = new System.Windows.Forms.TreeView();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripSpan = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainerRight = new System.Windows.Forms.SplitContainer();
			this.groupBoxRemarks = new System.Windows.Forms.GroupBox();
			this.richTextBoxRemarks = new System.Windows.Forms.RichTextBox();
			this.groupBoxPicture = new System.Windows.Forms.GroupBox();
			this.pictureBox = new QAliber.Logger.Controls.ZoomPanControl();
			this.toolStripPicture = new System.Windows.Forms.ToolStrip();
			this.zoomPicture = new System.Windows.Forms.ToolStripButton();
			this.panPicture = new System.Windows.Forms.ToolStripButton();
			this.slideShowControl = new QAliber.Logger.Controls.SlideshowControl();
			this.perfGraphControl = new QAliber.Logger.Controls.PerfGraphControl();
			this.toolStripMain = new System.Windows.Forms.ToolStrip();
			this.infoFilter = new System.Windows.Forms.ToolStripButton();
			this.warningFilter = new System.Windows.Forms.ToolStripButton();
			this.errorFilter = new System.Windows.Forms.ToolStripButton();
			this.pictureFilter = new System.Windows.Forms.ToolStripButton();
			this.linkFilter = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.normalFilter = new System.Windows.Forms.ToolStripButton();
			this.debugFilter = new System.Windows.Forms.ToolStripButton();
			this.internalFilter = new System.Windows.Forms.ToolStripButton();
			this.criticalFilter = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.stringFilter = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.processFilter = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.videoPanelToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.resourcesGraphToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.nodeMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyThisMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jumpNextErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.jumpPrevErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.countThisMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.countItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.countAllTheChildrenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.showLeavesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainerMain.Panel1.SuspendLayout();
			this.splitContainerMain.Panel2.SuspendLayout();
			this.splitContainerMain.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.splitContainerRight.Panel1.SuspendLayout();
			this.splitContainerRight.Panel2.SuspendLayout();
			this.splitContainerRight.SuspendLayout();
			this.groupBoxRemarks.SuspendLayout();
			this.groupBoxPicture.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.toolStripPicture.SuspendLayout();
			this.toolStripMain.SuspendLayout();
			this.nodeMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// logTree
			// 
			this.logTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.logTree.HideSelection = false;
			this.logTree.ImageIndex = 0;
			this.logTree.ImageList = this.imageTree;
			this.logTree.Location = new System.Drawing.Point(0, 0);
			this.logTree.Name = "logTree";
			this.logTree.SelectedImageIndex = 0;
			this.logTree.Size = new System.Drawing.Size(312, 628);
			this.logTree.TabIndex = 0;
			this.logTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.logTree_NodeMouseDoubleClick);
			this.logTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.logTree_AfterSelect);
			this.logTree.MouseMove += new System.Windows.Forms.MouseEventHandler(this.logTree_MouseMove);
			// 
			// imageTree
			// 
			this.imageTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageTree.ImageStream")));
			this.imageTree.TransparentColor = System.Drawing.Color.White;
			this.imageTree.Images.SetKeyName(0, "Info");
			this.imageTree.Images.SetKeyName(1, "Warning");
			this.imageTree.Images.SetKeyName(2, "Picture");
			this.imageTree.Images.SetKeyName(3, "Link");
			this.imageTree.Images.SetKeyName(4, "Passed");
			this.imageTree.Images.SetKeyName(5, "PassedWarnings");
			this.imageTree.Images.SetKeyName(6, "PassedErrors");
			this.imageTree.Images.SetKeyName(7, "Error");
			this.imageTree.Images.SetKeyName(8, "Normal");
			this.imageTree.Images.SetKeyName(9, "Debug");
			this.imageTree.Images.SetKeyName(10, "Internal");
			this.imageTree.Images.SetKeyName(11, "Critical");
			// 
			// splitContainerMain
			// 
			this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMain.Location = new System.Drawing.Point(0, 25);
			this.splitContainerMain.Name = "splitContainerMain";
			// 
			// splitContainerMain.Panel1
			// 
			this.splitContainerMain.Panel1.Controls.Add(this.logTreeFlattened);
			this.splitContainerMain.Panel1.Controls.Add(this.logTreeFiltered);
			this.splitContainerMain.Panel1.Controls.Add(this.logTree);
			this.splitContainerMain.Panel1.Controls.Add(this.statusStrip);
			// 
			// splitContainerMain.Panel2
			// 
			this.splitContainerMain.Panel2.Controls.Add(this.splitContainerRight);
			this.splitContainerMain.Size = new System.Drawing.Size(800, 650);
			this.splitContainerMain.SplitterDistance = 312;
			this.splitContainerMain.TabIndex = 1;
			// 
			// logTreeFlattened
			// 
			this.logTreeFlattened.BackColor = System.Drawing.Color.Azure;
			this.logTreeFlattened.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logTreeFlattened.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.logTreeFlattened.ImageIndex = 0;
			this.logTreeFlattened.ImageList = this.imageTree;
			this.logTreeFlattened.Location = new System.Drawing.Point(0, 0);
			this.logTreeFlattened.Name = "logTreeFlattened";
			this.logTreeFlattened.SelectedImageIndex = 0;
			this.logTreeFlattened.Size = new System.Drawing.Size(312, 628);
			this.logTreeFlattened.TabIndex = 3;
			this.logTreeFlattened.Visible = false;
			this.logTreeFlattened.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.logTree_NodeMouseDoubleClick);
			this.logTreeFlattened.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.logTree_AfterSelect);
			this.logTreeFlattened.MouseMove += new System.Windows.Forms.MouseEventHandler(this.logTree_MouseMove);
			// 
			// logTreeFiltered
			// 
			this.logTreeFiltered.BackColor = System.Drawing.Color.LightYellow;
			this.logTreeFiltered.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logTreeFiltered.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.logTreeFiltered.ImageIndex = 0;
			this.logTreeFiltered.ImageList = this.imageTree;
			this.logTreeFiltered.Location = new System.Drawing.Point(0, 0);
			this.logTreeFiltered.Name = "logTreeFiltered";
			this.logTreeFiltered.SelectedImageIndex = 0;
			this.logTreeFiltered.Size = new System.Drawing.Size(312, 628);
			this.logTreeFiltered.TabIndex = 2;
			this.logTreeFiltered.Visible = false;
			this.logTreeFiltered.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.logTree_NodeMouseDoubleClick);
			this.logTreeFiltered.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.logTree_AfterSelect);
			this.logTreeFiltered.MouseMove += new System.Windows.Forms.MouseEventHandler(this.logTree_MouseMove);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripTime,
			this.toolStripSpan,
			this.toolStripCount});
			this.statusStrip.Location = new System.Drawing.Point(0, 628);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.statusStrip.Size = new System.Drawing.Size(312, 22);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 1;
			// 
			// toolStripTime
			// 
			this.toolStripTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripTime.Name = "toolStripTime";
			this.toolStripTime.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStripSpan
			// 
			this.toolStripSpan.Name = "toolStripSpan";
			this.toolStripSpan.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStripCount
			// 
			this.toolStripCount.Name = "toolStripCount";
			this.toolStripCount.Size = new System.Drawing.Size(0, 17);
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
			this.splitContainerRight.Panel1.Controls.Add(this.groupBoxRemarks);
			// 
			// splitContainerRight.Panel2
			// 
			this.splitContainerRight.Panel2.Controls.Add(this.groupBoxPicture);
			this.splitContainerRight.Panel2.Controls.Add(this.slideShowControl);
			this.splitContainerRight.Panel2.Controls.Add(this.perfGraphControl);
			this.splitContainerRight.Size = new System.Drawing.Size(484, 650);
			this.splitContainerRight.SplitterDistance = 97;
			this.splitContainerRight.TabIndex = 0;
			// 
			// groupBoxRemarks
			// 
			this.groupBoxRemarks.Controls.Add(this.richTextBoxRemarks);
			this.groupBoxRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxRemarks.Location = new System.Drawing.Point(0, 0);
			this.groupBoxRemarks.Name = "groupBoxRemarks";
			this.groupBoxRemarks.Size = new System.Drawing.Size(484, 97);
			this.groupBoxRemarks.TabIndex = 0;
			this.groupBoxRemarks.TabStop = false;
			this.groupBoxRemarks.Text = "Remarks";
			// 
			// richTextBoxRemarks
			// 
			this.richTextBoxRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxRemarks.Location = new System.Drawing.Point(3, 16);
			this.richTextBoxRemarks.Name = "richTextBoxRemarks";
			this.richTextBoxRemarks.ReadOnly = true;
			this.richTextBoxRemarks.Size = new System.Drawing.Size(478, 78);
			this.richTextBoxRemarks.TabIndex = 0;
			this.richTextBoxRemarks.Text = "";
			// 
			// groupBoxPicture
			// 
			this.groupBoxPicture.Controls.Add(this.pictureBox);
			this.groupBoxPicture.Controls.Add(this.toolStripPicture);
			this.groupBoxPicture.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxPicture.Location = new System.Drawing.Point(0, 0);
			this.groupBoxPicture.Name = "groupBoxPicture";
			this.groupBoxPicture.Size = new System.Drawing.Size(484, 549);
			this.groupBoxPicture.TabIndex = 1;
			this.groupBoxPicture.TabStop = false;
			this.groupBoxPicture.Text = "Picture";
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.SystemColors.Window;
			this.pictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.ImageFile = null;
			this.pictureBox.Location = new System.Drawing.Point(3, 41);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.PanLocation = new System.Drawing.Point(0, 0);
			this.pictureBox.Size = new System.Drawing.Size(478, 505);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.Zoom = 1F;
			// 
			// toolStripPicture
			// 
			this.toolStripPicture.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripPicture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.zoomPicture,
			this.panPicture});
			this.toolStripPicture.Location = new System.Drawing.Point(3, 16);
			this.toolStripPicture.Name = "toolStripPicture";
			this.toolStripPicture.Size = new System.Drawing.Size(478, 25);
			this.toolStripPicture.TabIndex = 1;
			// 
			// zoomPicture
			// 
			this.zoomPicture.CheckOnClick = true;
			this.zoomPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.zoomPicture.Image = ((System.Drawing.Image)(resources.GetObject("zoomPicture.Image")));
			this.zoomPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.zoomPicture.Name = "zoomPicture";
			this.zoomPicture.Size = new System.Drawing.Size(23, 22);
			this.zoomPicture.Text = "Zoom";
			this.zoomPicture.ToolTipText = "Zoom";
			this.zoomPicture.Click += new System.EventHandler(this.zoomPicture_Click);
			// 
			// panPicture
			// 
			this.panPicture.Checked = true;
			this.panPicture.CheckOnClick = true;
			this.panPicture.CheckState = System.Windows.Forms.CheckState.Checked;
			this.panPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.panPicture.Image = ((System.Drawing.Image)(resources.GetObject("panPicture.Image")));
			this.panPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.panPicture.Name = "panPicture";
			this.panPicture.Size = new System.Drawing.Size(23, 22);
			this.panPicture.Text = "Pan";
			this.panPicture.Click += new System.EventHandler(this.panPicture_Click);
			// 
			// slideShowControl
			// 
			this.slideShowControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slideShowControl.Location = new System.Drawing.Point(0, 0);
			this.slideShowControl.Name = "slideShowControl";
			this.slideShowControl.Size = new System.Drawing.Size(484, 549);
			this.slideShowControl.TabIndex = 2;
			this.slideShowControl.Visible = false;
			// 
			// perfGraphControl
			// 
			this.perfGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.perfGraphControl.Location = new System.Drawing.Point(0, 0);
			this.perfGraphControl.Name = "perfGraphControl";
			this.perfGraphControl.Size = new System.Drawing.Size(484, 549);
			this.perfGraphControl.TabIndex = 2;
			this.perfGraphControl.Visible = false;
			// 
			// toolStripMain
			// 
			this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.infoFilter,
			this.warningFilter,
			this.errorFilter,
			this.pictureFilter,
			this.linkFilter,
			this.toolStripSeparator3,
			this.normalFilter,
			this.debugFilter,
			this.internalFilter,
			this.criticalFilter,
			this.toolStripSeparator6,
			this.stringFilter,
			this.toolStripSeparator2,
			this.processFilter,
			this.toolStripSeparator5,
			this.videoPanelToolStripButton,
			this.resourcesGraphToolStripButton,
			this.toolStripSeparator7,
			this.refreshToolStripButton,
			this.toolStripSeparator1,
			this.helpToolStripButton});
			this.toolStripMain.Location = new System.Drawing.Point(0, 0);
			this.toolStripMain.Name = "toolStripMain";
			this.toolStripMain.Size = new System.Drawing.Size(800, 25);
			this.toolStripMain.TabIndex = 2;
			// 
			// infoFilter
			// 
			this.infoFilter.CheckOnClick = true;
			this.infoFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.infoFilter.ImageTransparentColor = System.Drawing.Color.White;
			this.infoFilter.Name = "infoFilter";
			this.infoFilter.Size = new System.Drawing.Size(23, 22);
			this.infoFilter.Text = "Filter By Info";
			// 
			// warningFilter
			// 
			this.warningFilter.CheckOnClick = true;
			this.warningFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.warningFilter.ImageTransparentColor = System.Drawing.Color.White;
			this.warningFilter.Name = "warningFilter";
			this.warningFilter.Size = new System.Drawing.Size(23, 22);
			this.warningFilter.Text = "Filter By Warning";
			// 
			// errorFilter
			// 
			this.errorFilter.CheckOnClick = true;
			this.errorFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.errorFilter.ImageTransparentColor = System.Drawing.Color.White;
			this.errorFilter.Name = "errorFilter";
			this.errorFilter.Size = new System.Drawing.Size(23, 22);
			this.errorFilter.Text = "toolStripButton1";
			this.errorFilter.ToolTipText = "Filter By Error";
			// 
			// pictureFilter
			// 
			this.pictureFilter.CheckOnClick = true;
			this.pictureFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pictureFilter.ImageTransparentColor = System.Drawing.Color.White;
			this.pictureFilter.Name = "pictureFilter";
			this.pictureFilter.Size = new System.Drawing.Size(23, 22);
			this.pictureFilter.ToolTipText = "Filter By Picture";
			// 
			// linkFilter
			// 
			this.linkFilter.CheckOnClick = true;
			this.linkFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.linkFilter.ImageTransparentColor = System.Drawing.Color.White;
			this.linkFilter.Name = "linkFilter";
			this.linkFilter.Size = new System.Drawing.Size(23, 22);
			this.linkFilter.Text = "Filter By Link";
			this.linkFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// normalFilter
			// 
			this.normalFilter.CheckOnClick = true;
			this.normalFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.normalFilter.ImageTransparentColor = System.Drawing.Color.White;
			this.normalFilter.Name = "normalFilter";
			this.normalFilter.Size = new System.Drawing.Size(23, 22);
			this.normalFilter.ToolTipText = "Filter By Normal Verbosity";
			// 
			// debugFilter
			// 
			this.debugFilter.CheckOnClick = true;
			this.debugFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.debugFilter.ImageTransparentColor = System.Drawing.Color.White;
			this.debugFilter.Name = "debugFilter";
			this.debugFilter.Size = new System.Drawing.Size(23, 22);
			this.debugFilter.ToolTipText = "Filter By Debug Verbosity";
			// 
			// internalFilter
			// 
			this.internalFilter.CheckOnClick = true;
			this.internalFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.internalFilter.ImageTransparentColor = System.Drawing.Color.White;
			this.internalFilter.Name = "internalFilter";
			this.internalFilter.Size = new System.Drawing.Size(23, 22);
			this.internalFilter.ToolTipText = "Filter By Internal Verbosity";
			// 
			// criticalFilter
			// 
			this.criticalFilter.CheckOnClick = true;
			this.criticalFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.criticalFilter.ImageTransparentColor = System.Drawing.Color.White;
			this.criticalFilter.Name = "criticalFilter";
			this.criticalFilter.Size = new System.Drawing.Size(23, 22);
			this.criticalFilter.ToolTipText = "Filter By Critical Verbosity";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// stringFilter
			// 
			this.stringFilter.Name = "stringFilter";
			this.stringFilter.Size = new System.Drawing.Size(100, 25);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// processFilter
			// 
			this.processFilter.CheckOnClick = true;
			this.processFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.processFilter.Image = ((System.Drawing.Image)(resources.GetObject("processFilter.Image")));
			this.processFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.processFilter.Name = "processFilter";
			this.processFilter.Size = new System.Drawing.Size(23, 22);
			this.processFilter.Text = "toolStripButton1";
			this.processFilter.ToolTipText = "Perform Filter";
			this.processFilter.Click += new System.EventHandler(this.processFilter_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// videoPanelToolStripButton
			// 
			this.videoPanelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.videoPanelToolStripButton.Enabled = false;
			this.videoPanelToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("videoPanelToolStripButton.Image")));
			this.videoPanelToolStripButton.ImageTransparentColor = System.Drawing.Color.White;
			this.videoPanelToolStripButton.Name = "videoPanelToolStripButton";
			this.videoPanelToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.videoPanelToolStripButton.ToolTipText = "Video Panel";
			this.videoPanelToolStripButton.Click += new System.EventHandler(this.videoPanelToolStripButton_Click);
			// 
			// resourcesGraphToolStripButton
			// 
			this.resourcesGraphToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.resourcesGraphToolStripButton.Enabled = false;
			this.resourcesGraphToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("resourcesGraphToolStripButton.Image")));
			this.resourcesGraphToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.resourcesGraphToolStripButton.Name = "resourcesGraphToolStripButton";
			this.resourcesGraphToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.resourcesGraphToolStripButton.ToolTipText = "Resources Graph Panel";
			this.resourcesGraphToolStripButton.Click += new System.EventHandler(this.resourcesGraphToolStripButton_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			// 
			// refreshToolStripButton
			// 
			this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.refreshToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripButton.Image")));
			this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshToolStripButton.Name = "refreshToolStripButton";
			this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.refreshToolStripButton.Text = "toolStripButton1";
			this.refreshToolStripButton.ToolTipText = "Refresh";
			this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// helpToolStripButton
			// 
			this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
			this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.helpToolStripButton.Name = "helpToolStripButton";
			this.helpToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.helpToolStripButton.Text = "He&lp";
			// 
			// nodeMenuStrip
			// 
			this.nodeMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.copyThisMessageToolStripMenuItem,
			this.jumpNextErrorToolStripMenuItem,
			this.jumpPrevErrorToolStripMenuItem,
			this.toolStripSeparator8,
			this.countThisMessageToolStripMenuItem,
			this.countItemsToolStripMenuItem,
			this.countAllTheChildrenToolStripMenuItem,
			this.toolStripSeparator4,
			this.showLeavesToolStripMenuItem});
			this.nodeMenuStrip.Name = "nodeMenuStrip";
			this.nodeMenuStrip.Size = new System.Drawing.Size(279, 192);
			// 
			// copyThisMessageToolStripMenuItem
			// 
			this.copyThisMessageToolStripMenuItem.Name = "copyThisMessageToolStripMenuItem";
			this.copyThisMessageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyThisMessageToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
			this.copyThisMessageToolStripMenuItem.Text = "Copy this message";
			this.copyThisMessageToolStripMenuItem.Click += new System.EventHandler(this.copyThisMessageToolStripMenuItem_Click);
			// 
			// jumpNextErrorToolStripMenuItem
			// 
			this.jumpNextErrorToolStripMenuItem.Name = "jumpNextErrorToolStripMenuItem";
			this.jumpNextErrorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
			this.jumpNextErrorToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
			this.jumpNextErrorToolStripMenuItem.Text = "Jump to Next Warning / Error";
			this.jumpNextErrorToolStripMenuItem.Click += new System.EventHandler(this.jumpNextErrorToolStripMenuItem_Click);
			// 
			// jumpPrevErrorToolStripMenuItem
			// 
			this.jumpPrevErrorToolStripMenuItem.Name = "jumpPrevErrorToolStripMenuItem";
			this.jumpPrevErrorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
			this.jumpPrevErrorToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
			this.jumpPrevErrorToolStripMenuItem.Text = "Jump to Previous Warning / Error";
			this.jumpPrevErrorToolStripMenuItem.Click += new System.EventHandler(this.jumpPrevErrorToolStripMenuItem_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(275, 6);
			// 
			// countThisMessageToolStripMenuItem
			// 
			this.countThisMessageToolStripMenuItem.Name = "countThisMessageToolStripMenuItem";
			this.countThisMessageToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
			this.countThisMessageToolStripMenuItem.Text = "Count this message";
			this.countThisMessageToolStripMenuItem.Click += new System.EventHandler(this.countThisMessageToolStripMenuItem_Click);
			// 
			// countItemsToolStripMenuItem
			// 
			this.countItemsToolStripMenuItem.Name = "countItemsToolStripMenuItem";
			this.countItemsToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
			this.countItemsToolStripMenuItem.Text = "Count items at this tree level";
			this.countItemsToolStripMenuItem.Click += new System.EventHandler(this.countItemsToolStripMenuItem_Click);
			// 
			// countAllTheChildrenToolStripMenuItem
			// 
			this.countAllTheChildrenToolStripMenuItem.Name = "countAllTheChildrenToolStripMenuItem";
			this.countAllTheChildrenToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
			this.countAllTheChildrenToolStripMenuItem.Text = "Count all the children";
			this.countAllTheChildrenToolStripMenuItem.Click += new System.EventHandler(this.countAllTheChildrenToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(275, 6);
			// 
			// showLeavesToolStripMenuItem
			// 
			this.showLeavesToolStripMenuItem.CheckOnClick = true;
			this.showLeavesToolStripMenuItem.Name = "showLeavesToolStripMenuItem";
			this.showLeavesToolStripMenuItem.Size = new System.Drawing.Size(278, 22);
			this.showLeavesToolStripMenuItem.Text = "Show Leaves Only";
			this.showLeavesToolStripMenuItem.Click += new System.EventHandler(this.showLeavesToolStripMenuItem_Click);
			// 
			// LogViewerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.Controls.Add(this.splitContainerMain);
			this.Controls.Add(this.toolStripMain);
			this.Name = "LogViewerControl";
			this.Size = new System.Drawing.Size(800, 675);
			this.splitContainerMain.Panel1.ResumeLayout(false);
			this.splitContainerMain.Panel1.PerformLayout();
			this.splitContainerMain.Panel2.ResumeLayout(false);
			this.splitContainerMain.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.splitContainerRight.Panel1.ResumeLayout(false);
			this.splitContainerRight.Panel2.ResumeLayout(false);
			this.splitContainerRight.ResumeLayout(false);
			this.groupBoxRemarks.ResumeLayout(false);
			this.groupBoxPicture.ResumeLayout(false);
			this.groupBoxPicture.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.toolStripPicture.ResumeLayout(false);
			this.toolStripPicture.PerformLayout();
			this.toolStripMain.ResumeLayout(false);
			this.toolStripMain.PerformLayout();
			this.nodeMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView logTree;
		private System.Windows.Forms.ImageList imageTree;
		private System.Windows.Forms.SplitContainer splitContainerMain;
		private System.Windows.Forms.SplitContainer splitContainerRight;
		private System.Windows.Forms.GroupBox groupBoxRemarks;
		private System.Windows.Forms.RichTextBox richTextBoxRemarks;
		private System.Windows.Forms.GroupBox groupBoxPicture;
		private ZoomPanControl pictureBox;
		private SlideshowControl slideShowControl;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripTime;
		private System.Windows.Forms.ToolStrip toolStripMain;
		private System.Windows.Forms.ToolStripButton helpToolStripButton;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripStatusLabel toolStripSpan;
		private System.Windows.Forms.ToolStripButton infoFilter;
		private System.Windows.Forms.ToolStripButton warningFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton processFilter;
		private System.Windows.Forms.ToolStripButton errorFilter;
		private System.Windows.Forms.ToolStripButton pictureFilter;
		private System.Windows.Forms.ToolStripButton linkFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStrip toolStripPicture;
		private System.Windows.Forms.ToolStripButton zoomPicture;
		private System.Windows.Forms.ToolStripButton panPicture;
		private System.Windows.Forms.TreeView logTreeFiltered;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripTextBox stringFilter;
		private System.Windows.Forms.ContextMenuStrip nodeMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem countThisMessageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem countItemsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem countAllTheChildrenToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripCount;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem showLeavesToolStripMenuItem;
		private System.Windows.Forms.TreeView logTreeFlattened;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton refreshToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem copyThisMessageToolStripMenuItem;
		private PerfGraphControl perfGraphControl;
		private System.Windows.Forms.ToolStripButton normalFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton debugFilter;
		private System.Windows.Forms.ToolStripButton internalFilter;
		private System.Windows.Forms.ToolStripButton criticalFilter;
		private System.Windows.Forms.ToolStripButton videoPanelToolStripButton;
		private System.Windows.Forms.ToolStripButton resourcesGraphToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem jumpNextErrorToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem jumpPrevErrorToolStripMenuItem;
	}
}


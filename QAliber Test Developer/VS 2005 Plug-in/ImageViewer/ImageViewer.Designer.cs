namespace QAliber.VS2005.Plugin
{
	partial class ImageViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewer));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripSave = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLocation = new System.Windows.Forms.ToolStripLabel();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.groupBoxPicture = new System.Windows.Forms.GroupBox();
			this.zoomPanControl = new QAliber.VS2005.Plugin.ZoomPanControl();
			this.toolStripPicture = new System.Windows.Forms.ToolStrip();
			this.zoomPicture = new System.Windows.Forms.ToolStripButton();
			this.panPicture = new System.Windows.Forms.ToolStripButton();
			this.selectPicture = new System.Windows.Forms.ToolStripButton();
			this.groupBoxCode = new System.Windows.Forms.GroupBox();
			this.codeRichTextBox = new System.Windows.Forms.RichTextBox();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBoxTasks = new System.Windows.Forms.GroupBox();
			this.linkGeneratePartialReadCode = new System.Windows.Forms.LinkLabel();
			this.imageViewerBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.linkCreatePartialResource = new System.Windows.Forms.LinkLabel();
			this.linkGenerateEntireReadCode = new System.Windows.Forms.LinkLabel();
			this.linkCreateResourceEntire = new System.Windows.Forms.LinkLabel();
			this.linkGenerateEntireCompareCode = new System.Windows.Forms.LinkLabel();
			this.linkCreateVirtual = new System.Windows.Forms.LinkLabel();
			this.linkGeneratePartialCompareCode = new System.Windows.Forms.LinkLabel();
			this.linkGeenrateClickCode = new System.Windows.Forms.LinkLabel();
			this.toolStrip.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.groupBoxPicture.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.zoomPanControl)).BeginInit();
			this.toolStripPicture.SuspendLayout();
			this.groupBoxCode.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.groupBoxTasks.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageViewerBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripSave,
			this.toolStripSeparator,
			this.toolStripLocation});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(753, 25);
			this.toolStrip.TabIndex = 1;
			// 
			// toolStripSave
			// 
			this.toolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSave.Image")));
			this.toolStripSave.ImageTransparentColor = System.Drawing.Color.White;
			this.toolStripSave.Name = "toolStripSave";
			this.toolStripSave.Size = new System.Drawing.Size(23, 22);
			this.toolStripSave.Text = "toolStripButton3";
			this.toolStripSave.ToolTipText = "Save";
			this.toolStripSave.Click += new System.EventHandler(this.toolStripSave_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLocation
			// 
			this.toolStripLocation.Name = "toolStripLocation";
			this.toolStripLocation.Size = new System.Drawing.Size(30, 22);
			this.toolStripLocation.Text = "X: Y:";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "Jpeg files|*.jpg|All Files|*.*";
			this.saveFileDialog.Title = "Save Image as Jpeg";
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 25);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.groupBoxPicture);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.groupBoxCode);
			this.splitContainer.Panel2.Controls.Add(this.groupBoxTasks);
			this.splitContainer.Size = new System.Drawing.Size(753, 397);
			this.splitContainer.SplitterDistance = 490;
			this.splitContainer.TabIndex = 2;
			// 
			// groupBoxPicture
			// 
			this.groupBoxPicture.Controls.Add(this.zoomPanControl);
			this.groupBoxPicture.Controls.Add(this.toolStripPicture);
			this.groupBoxPicture.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxPicture.Location = new System.Drawing.Point(0, 0);
			this.groupBoxPicture.Name = "groupBoxPicture";
			this.groupBoxPicture.Size = new System.Drawing.Size(490, 397);
			this.groupBoxPicture.TabIndex = 2;
			this.groupBoxPicture.TabStop = false;
			this.groupBoxPicture.Text = "Picture";
			// 
			// zoomPanControl
			// 
			this.zoomPanControl.BackColor = System.Drawing.SystemColors.Window;
			this.zoomPanControl.Cursor = System.Windows.Forms.Cursors.Hand;
			this.zoomPanControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.zoomPanControl.ImageFile = null;
			this.zoomPanControl.Location = new System.Drawing.Point(3, 41);
			this.zoomPanControl.Name = "zoomPanControl";
			this.zoomPanControl.PanLocation = new System.Drawing.Point(0, 0);
			this.zoomPanControl.Size = new System.Drawing.Size(484, 353);
			this.zoomPanControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.zoomPanControl.TabIndex = 0;
			this.zoomPanControl.TabStop = false;
			this.zoomPanControl.Zoom = 1F;
			this.zoomPanControl.ZoomPanImage = null;
			this.zoomPanControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zoomPanControl_MouseMove);
			this.zoomPanControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zoomPanControl_MouseClick);
			this.zoomPanControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoomPanControl_MouseUp);
			// 
			// toolStripPicture
			// 
			this.toolStripPicture.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripPicture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.zoomPicture,
			this.panPicture,
			this.selectPicture});
			this.toolStripPicture.Location = new System.Drawing.Point(3, 16);
			this.toolStripPicture.Name = "toolStripPicture";
			this.toolStripPicture.Size = new System.Drawing.Size(484, 25);
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
			// selectPicture
			// 
			this.selectPicture.CheckOnClick = true;
			this.selectPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.selectPicture.Image = ((System.Drawing.Image)(resources.GetObject("selectPicture.Image")));
			this.selectPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.selectPicture.Name = "selectPicture";
			this.selectPicture.Size = new System.Drawing.Size(23, 22);
			this.selectPicture.ToolTipText = "Selection";
			this.selectPicture.Click += new System.EventHandler(this.selectPicture_Click);
			// 
			// groupBoxCode
			// 
			this.groupBoxCode.Controls.Add(this.codeRichTextBox);
			this.groupBoxCode.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBoxCode.Location = new System.Drawing.Point(0, 229);
			this.groupBoxCode.Name = "groupBoxCode";
			this.groupBoxCode.Size = new System.Drawing.Size(259, 168);
			this.groupBoxCode.TabIndex = 9;
			this.groupBoxCode.TabStop = false;
			this.groupBoxCode.Text = "Generated Code";
			// 
			// codeRichTextBox
			// 
			this.codeRichTextBox.ContextMenuStrip = this.contextMenuStrip;
			this.codeRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.codeRichTextBox.Location = new System.Drawing.Point(3, 16);
			this.codeRichTextBox.Name = "codeRichTextBox";
			this.codeRichTextBox.Size = new System.Drawing.Size(253, 149);
			this.codeRichTextBox.TabIndex = 0;
			this.codeRichTextBox.Text = "";
			this.codeRichTextBox.WordWrap = false;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.copyToClipboardToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(163, 26);
			// 
			// copyToClipboardToolStripMenuItem
			// 
			this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
			this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.copyToClipboardToolStripMenuItem.Text = "Copy To Clipboard";
			this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
			// 
			// groupBoxTasks
			// 
			this.groupBoxTasks.Controls.Add(this.linkGeneratePartialReadCode);
			this.groupBoxTasks.Controls.Add(this.linkCreatePartialResource);
			this.groupBoxTasks.Controls.Add(this.linkGenerateEntireReadCode);
			this.groupBoxTasks.Controls.Add(this.linkCreateResourceEntire);
			this.groupBoxTasks.Controls.Add(this.linkGenerateEntireCompareCode);
			this.groupBoxTasks.Controls.Add(this.linkCreateVirtual);
			this.groupBoxTasks.Controls.Add(this.linkGeneratePartialCompareCode);
			this.groupBoxTasks.Controls.Add(this.linkGeenrateClickCode);
			this.groupBoxTasks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxTasks.Location = new System.Drawing.Point(0, 0);
			this.groupBoxTasks.Name = "groupBoxTasks";
			this.groupBoxTasks.Size = new System.Drawing.Size(259, 397);
			this.groupBoxTasks.TabIndex = 10;
			this.groupBoxTasks.TabStop = false;
			this.groupBoxTasks.Text = "Tasks";
			// 
			// linkGeneratePartialReadCode
			// 
			this.linkGeneratePartialReadCode.AutoSize = true;
			this.linkGeneratePartialReadCode.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.imageViewerBindingSource, "HasImageSelection", true));
			this.linkGeneratePartialReadCode.Location = new System.Drawing.Point(19, 177);
			this.linkGeneratePartialReadCode.Name = "linkGeneratePartialReadCode";
			this.linkGeneratePartialReadCode.Size = new System.Drawing.Size(233, 13);
			this.linkGeneratePartialReadCode.TabIndex = 8;
			this.linkGeneratePartialReadCode.TabStop = true;
			this.linkGeneratePartialReadCode.Text = "Generate Code for Partial Image Reading (OCR)";
			this.linkGeneratePartialReadCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGeneratePartialReadCode_LinkClicked);
			// 
			// imageViewerBindingSource
			// 
			this.imageViewerBindingSource.DataSource = typeof(QAliber.VS2005.Plugin.ImageViewer);
			// 
			// linkCreatePartialResource
			// 
			this.linkCreatePartialResource.AutoSize = true;
			this.linkCreatePartialResource.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.imageViewerBindingSource, "HasImageSelection", true));
			this.linkCreatePartialResource.Location = new System.Drawing.Point(18, 28);
			this.linkCreatePartialResource.Name = "linkCreatePartialResource";
			this.linkCreatePartialResource.Size = new System.Drawing.Size(189, 13);
			this.linkCreatePartialResource.TabIndex = 1;
			this.linkCreatePartialResource.TabStop = true;
			this.linkCreatePartialResource.Text = "Create Resource from Image Selection";
			this.linkCreatePartialResource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCreatePartialResource_LinkClicked);
			// 
			// linkGenerateEntireReadCode
			// 
			this.linkGenerateEntireReadCode.AutoSize = true;
			this.linkGenerateEntireReadCode.Location = new System.Drawing.Point(19, 200);
			this.linkGenerateEntireReadCode.Name = "linkGenerateEntireReadCode";
			this.linkGenerateEntireReadCode.Size = new System.Drawing.Size(231, 13);
			this.linkGenerateEntireReadCode.TabIndex = 7;
			this.linkGenerateEntireReadCode.TabStop = true;
			this.linkGenerateEntireReadCode.Text = "Generate Code for Entire Image Reading (OCR)";
			this.linkGenerateEntireReadCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGenerateEntireReadCode_LinkClicked);
			// 
			// linkCreateResourceEntire
			// 
			this.linkCreateResourceEntire.AutoSize = true;
			this.linkCreateResourceEntire.Location = new System.Drawing.Point(18, 53);
			this.linkCreateResourceEntire.Name = "linkCreateResourceEntire";
			this.linkCreateResourceEntire.Size = new System.Drawing.Size(172, 13);
			this.linkCreateResourceEntire.TabIndex = 2;
			this.linkCreateResourceEntire.TabStop = true;
			this.linkCreateResourceEntire.Text = "Create Resource from Entire Image";
			this.linkCreateResourceEntire.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkCreateResourceEntire_LinkClicked);
			// 
			// linkGenerateEntireCompareCode
			// 
			this.linkGenerateEntireCompareCode.AutoSize = true;
			this.linkGenerateEntireCompareCode.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.imageViewerBindingSource, "EntireResName", true));
			this.linkGenerateEntireCompareCode.Location = new System.Drawing.Point(19, 153);
			this.linkGenerateEntireCompareCode.Name = "linkGenerateEntireCompareCode";
			this.linkGenerateEntireCompareCode.Size = new System.Drawing.Size(201, 13);
			this.linkGenerateEntireCompareCode.TabIndex = 6;
			this.linkGenerateEntireCompareCode.TabStop = true;
			this.linkGenerateEntireCompareCode.Text = "Generate Code for Entire Image Compare";
			this.linkGenerateEntireCompareCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGenerateEntireCompareCode_LinkClicked);
			// 
			// linkCreateVirtual
			// 
			this.linkCreateVirtual.AutoSize = true;
			this.linkCreateVirtual.Enabled = false;
			this.linkCreateVirtual.Location = new System.Drawing.Point(18, 78);
			this.linkCreateVirtual.Name = "linkCreateVirtual";
			this.linkCreateVirtual.Size = new System.Drawing.Size(211, 13);
			this.linkCreateVirtual.TabIndex = 3;
			this.linkCreateVirtual.TabStop = true;
			this.linkCreateVirtual.Text = "Create Virtual Control  from Image Selection";
			this.linkCreateVirtual.Visible = false;
			// 
			// linkGeneratePartialCompareCode
			// 
			this.linkGeneratePartialCompareCode.AutoSize = true;
			this.linkGeneratePartialCompareCode.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.imageViewerBindingSource, "PartialResName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.linkGeneratePartialCompareCode.Location = new System.Drawing.Point(18, 128);
			this.linkGeneratePartialCompareCode.Name = "linkGeneratePartialCompareCode";
			this.linkGeneratePartialCompareCode.Size = new System.Drawing.Size(203, 13);
			this.linkGeneratePartialCompareCode.TabIndex = 5;
			this.linkGeneratePartialCompareCode.TabStop = true;
			this.linkGeneratePartialCompareCode.Text = "Generate Code for Partial Image Compare";
			this.linkGeneratePartialCompareCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGeneratePartialCompareCode_LinkClicked);
			// 
			// linkGeenrateClickCode
			// 
			this.linkGeenrateClickCode.AutoSize = true;
			this.linkGeenrateClickCode.Enabled = false;
			this.linkGeenrateClickCode.Location = new System.Drawing.Point(18, 103);
			this.linkGeenrateClickCode.Name = "linkGeenrateClickCode";
			this.linkGeenrateClickCode.Size = new System.Drawing.Size(154, 13);
			this.linkGeenrateClickCode.TabIndex = 4;
			this.linkGeenrateClickCode.TabStop = true;
			this.linkGeenrateClickCode.Text = "Generate Code for Simple Click";
			this.linkGeenrateClickCode.Visible = false;
			this.linkGeenrateClickCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGeenrateClickCode_LinkClicked);
			// 
			// ImageViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(753, 422);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.toolStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImageViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Image Viewer";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.groupBoxPicture.ResumeLayout(false);
			this.groupBoxPicture.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.zoomPanControl)).EndInit();
			this.toolStripPicture.ResumeLayout(false);
			this.toolStripPicture.PerformLayout();
			this.groupBoxCode.ResumeLayout(false);
			this.contextMenuStrip.ResumeLayout(false);
			this.groupBoxTasks.ResumeLayout(false);
			this.groupBoxTasks.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageViewerBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripButton toolStripSave;
		private System.Windows.Forms.ToolStripLabel toolStripLocation;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.GroupBox groupBoxPicture;
		private ZoomPanControl zoomPanControl;
		private System.Windows.Forms.ToolStrip toolStripPicture;
		private System.Windows.Forms.ToolStripButton zoomPicture;
		private System.Windows.Forms.ToolStripButton panPicture;
		private System.Windows.Forms.RichTextBox codeRichTextBox;
		private System.Windows.Forms.ToolStripButton selectPicture;
		private System.Windows.Forms.LinkLabel linkCreatePartialResource;
		private System.Windows.Forms.LinkLabel linkGeneratePartialCompareCode;
		private System.Windows.Forms.LinkLabel linkGeenrateClickCode;
		private System.Windows.Forms.LinkLabel linkCreateVirtual;
		private System.Windows.Forms.LinkLabel linkCreateResourceEntire;
		private System.Windows.Forms.GroupBox groupBoxCode;
		private System.Windows.Forms.LinkLabel linkGeneratePartialReadCode;
		private System.Windows.Forms.LinkLabel linkGenerateEntireReadCode;
		private System.Windows.Forms.LinkLabel linkGenerateEntireCompareCode;
		private System.Windows.Forms.GroupBox groupBoxTasks;
		private System.Windows.Forms.BindingSource imageViewerBindingSource;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;


	}
}
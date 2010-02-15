namespace QAliber.Builder.Presentation
{
	partial class TestCasesPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestCasesPanel));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.addDllsToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.searchToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.findNextToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.dragToAddToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.typesTreeView = new System.Windows.Forms.TreeView();
			this.typesImageList = new System.Windows.Forms.ImageList(this.components);
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.leafContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip.SuspendLayout();
			this.leafContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.addDllsToolStripButton,
			this.toolStripSeparator1,
			this.searchToolStripTextBox,
			this.findNextToolStripButton,
			this.refreshToolStripButton,
			this.dragToAddToolStripLabel});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(314, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// addDllsToolStripButton
			// 
			this.addDllsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addDllsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("addDllsToolStripButton.Image")));
			this.addDllsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addDllsToolStripButton.Name = "addDllsToolStripButton";
			this.addDllsToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.addDllsToolStripButton.Text = "Add Test cases from external Dlls";
			this.addDllsToolStripButton.Click += new System.EventHandler(this.addDllsToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// searchToolStripTextBox
			// 
			this.searchToolStripTextBox.Name = "searchToolStripTextBox";
			this.searchToolStripTextBox.Size = new System.Drawing.Size(80, 25);
			this.searchToolStripTextBox.Text = "Search...";
			this.searchToolStripTextBox.Click += new System.EventHandler(this.searchToolStripTextBox_Click);
			// 
			// findNextToolStripButton
			// 
			this.findNextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.findNextToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("findNextToolStripButton.Image")));
			this.findNextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.findNextToolStripButton.Name = "findNextToolStripButton";
			this.findNextToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.findNextToolStripButton.Text = "Find Next";
			this.findNextToolStripButton.Click += new System.EventHandler(this.AfterSearchSubmitted);
			// 
			// refreshToolStripButton
			// 
			this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.refreshToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripButton.Image")));
			this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshToolStripButton.Name = "refreshToolStripButton";
			this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.refreshToolStripButton.Text = "Check for Updates";
			this.refreshToolStripButton.ToolTipText = "Refresh";
			this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
			// 
			// dragToAddToolStripLabel
			// 
			this.dragToAddToolStripLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dragToAddToolStripLabel.ForeColor = System.Drawing.Color.DimGray;
			this.dragToAddToolStripLabel.Name = "dragToAddToolStripLabel";
			this.dragToAddToolStripLabel.Size = new System.Drawing.Size(144, 22);
			this.dragToAddToolStripLabel.Text = "To add an item simply drag it";
			// 
			// typesTreeView
			// 
			this.typesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.typesTreeView.ImageIndex = 0;
			this.typesTreeView.ImageList = this.typesImageList;
			this.typesTreeView.Location = new System.Drawing.Point(0, 25);
			this.typesTreeView.Name = "typesTreeView";
			this.typesTreeView.SelectedImageIndex = 0;
			this.typesTreeView.Size = new System.Drawing.Size(314, 465);
			this.typesTreeView.TabIndex = 1;
			
			this.typesTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.AfterDoubleClick);
			this.typesTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.typesTreeView_MouseDown);
			this.typesTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.AfterDragStarted);
			// 
			// typesImageList
			// 
			this.typesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("typesImageList.ImageStream")));
			this.typesImageList.TransparentColor = System.Drawing.Color.Magenta;
			this.typesImageList.Images.SetKeyName(0, "TypesFolder");
			this.typesImageList.Images.SetKeyName(1, "GeneralTestCase");
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Assemblies|*.dll;*.exe";
			this.openFileDialog.Multiselect = true;
			this.openFileDialog.Title = "Load External Dlls";
			// 
			// leafContextMenuStrip
			// 
			this.leafContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.helpToolStripMenuItem});
			this.leafContextMenuStrip.Name = "leafContextMenuStrip";
			this.leafContextMenuStrip.Size = new System.Drawing.Size(115, 26);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripMenuItem.Image")));
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.helpToolStripMenuItem.Text = "Help";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
			// 
			// TestCasesPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.typesTreeView);
			this.Controls.Add(this.toolStrip);
			this.Name = "TestCasesPanel";
			this.Size = new System.Drawing.Size(314, 490);
			this.Load += new System.EventHandler(this.TestCasesPanel_Load);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.leafContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton addDllsToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripTextBox searchToolStripTextBox;
		private System.Windows.Forms.ToolStripButton findNextToolStripButton;
		private System.Windows.Forms.TreeView typesTreeView;
		private System.Windows.Forms.ImageList typesImageList;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripLabel dragToAddToolStripLabel;
		private System.Windows.Forms.ContextMenuStrip leafContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton refreshToolStripButton;
	}
}

namespace QAliber.Builder.Presentation
{
	partial class TabbedScenarioControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabbedScenarioControl));
			System.Windows.Forms.ToolStripProfessionalRenderer toolStripProfessionalRenderer1 = new System.Windows.Forms.ToolStripProfessionalRenderer();
			this.editToolStrip = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveAllToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.moveUpToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.moveDownToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.undoToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.redoToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.tabbedDocumentControl = new Darwen.Windows.Forms.Controls.TabbedDocuments.TabbedDocumentControl();
			this.editToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// editToolStrip
			// 
			this.editToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.editToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.newToolStripButton,
			this.openToolStripButton,
			this.saveToolStripButton,
			this.saveAllToolStripButton,
			this.toolStripSeparator,
			this.cutToolStripButton,
			this.copyToolStripButton,
			this.pasteToolStripButton,
			this.deleteToolStripButton,
			this.toolStripSeparator1,
			this.moveUpToolStripButton,
			this.moveDownToolStripButton,
			this.undoToolStripButton,
			this.redoToolStripButton});
			this.editToolStrip.Location = new System.Drawing.Point(0, 0);
			this.editToolStrip.Name = "editToolStrip";
			this.editToolStrip.Size = new System.Drawing.Size(500, 25);
			this.editToolStrip.TabIndex = 0;
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newToolStripButton.Text = "&New";
			this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.openToolStripButton.Text = "&Open";
			this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "&Save";
			this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
			// 
			// saveAllToolStripButton
			// 
			this.saveAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveAllToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveAllToolStripButton.Image")));
			this.saveAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveAllToolStripButton.Name = "saveAllToolStripButton";
			this.saveAllToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveAllToolStripButton.Text = "Save All";
			this.saveAllToolStripButton.ToolTipText = "Save All";
			this.saveAllToolStripButton.Click += new System.EventHandler(this.saveAllToolStripButton_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// cutToolStripButton
			// 
			this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
			this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripButton.Name = "cutToolStripButton";
			this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.cutToolStripButton.Text = "C&ut";
			this.cutToolStripButton.Click += new System.EventHandler(this.cutToolStripButton_Click);
			// 
			// copyToolStripButton
			// 
			this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
			this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripButton.Name = "copyToolStripButton";
			this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.copyToolStripButton.Text = "&Copy";
			this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
			// 
			// pasteToolStripButton
			// 
			this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
			this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripButton.Name = "pasteToolStripButton";
			this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.pasteToolStripButton.Text = "&Paste";
			this.pasteToolStripButton.Click += new System.EventHandler(this.pasteToolStripButton_Click);
			// 
			// deleteToolStripButton
			// 
			this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripButton.Image")));
			this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteToolStripButton.Name = "deleteToolStripButton";
			this.deleteToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.deleteToolStripButton.Text = "Delete";
			this.deleteToolStripButton.Click += new System.EventHandler(this.deleteToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// moveUpToolStripButton
			// 
			this.moveUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.moveUpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("moveUpToolStripButton.Image")));
			this.moveUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveUpToolStripButton.Name = "moveUpToolStripButton";
			this.moveUpToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.moveUpToolStripButton.Text = "Move Up";
			this.moveUpToolStripButton.ToolTipText = "Move Up In Tree";
			this.moveUpToolStripButton.Click += new System.EventHandler(this.moveUpToolStripButton_Click);
			// 
			// moveDownToolStripButton
			// 
			this.moveDownToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.moveDownToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("moveDownToolStripButton.Image")));
			this.moveDownToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveDownToolStripButton.Name = "moveDownToolStripButton";
			this.moveDownToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.moveDownToolStripButton.Text = "Move Down";
			this.moveDownToolStripButton.Click += new System.EventHandler(this.moveDownToolStripButton_Click);
			// 
			// undoToolStripButton
			// 
			this.undoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.undoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripButton.Image")));
			this.undoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.undoToolStripButton.Name = "undoToolStripButton";
			this.undoToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.undoToolStripButton.Text = "Undo";
			this.undoToolStripButton.ToolTipText = "Undo";
			this.undoToolStripButton.Click += new System.EventHandler(this.undoToolStripButton_Click);
			// 
			// redoToolStripButton
			// 
			this.redoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.redoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripButton.Image")));
			this.redoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.redoToolStripButton.Name = "redoToolStripButton";
			this.redoToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.redoToolStripButton.Text = "Redo";
			this.redoToolStripButton.Click += new System.EventHandler(this.redoToolStripButton_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "sce";
			this.openFileDialog.Filter = "Test Scenario File|*.scn|Log Files|*.qlog|All Files|*.*";
			this.openFileDialog.Multiselect = true;
			this.openFileDialog.Title = "Load Test Scenario";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "sce";
			this.saveFileDialog.Filter = "Test Scenario File|*.scn|All Files|*.*";
			this.saveFileDialog.Title = "Save Test Scenario";
			// 
			// tabbedDocumentControl
			// 
			this.tabbedDocumentControl.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.tabbedDocumentControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabbedDocumentControl.Location = new System.Drawing.Point(0, 25);
			this.tabbedDocumentControl.Name = "tabbedDocumentControl";
			toolStripProfessionalRenderer1.RoundedEdges = true;
			this.tabbedDocumentControl.Renderer = toolStripProfessionalRenderer1;
			this.tabbedDocumentControl.SelectedControl = null;
			this.tabbedDocumentControl.Size = new System.Drawing.Size(500, 392);
			this.tabbedDocumentControl.TabIndex = 1;
			this.tabbedDocumentControl.BeforeDocumentRemovedByUser += new Darwen.Windows.Forms.Controls.TabbedDocuments.TabbedDocumentControl.DocumentHandler(this.tabbedDocumentControl_BeforeDocumentRemovedByUser);
			// 
			// TabbedScenarioControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabbedDocumentControl);
			this.Controls.Add(this.editToolStrip);
			this.Name = "TabbedScenarioControl";
			this.Size = new System.Drawing.Size(500, 417);
			this.editToolStrip.ResumeLayout(false);
			this.editToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip editToolStrip;
		private System.Windows.Forms.ToolStripButton newToolStripButton;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripButton cutToolStripButton;
		private System.Windows.Forms.ToolStripButton copyToolStripButton;
		private System.Windows.Forms.ToolStripButton pasteToolStripButton;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton moveUpToolStripButton;
		private System.Windows.Forms.ToolStripButton saveAllToolStripButton;
		private System.Windows.Forms.ToolStripButton moveDownToolStripButton;
		private System.Windows.Forms.ToolStripButton undoToolStripButton;
		private System.Windows.Forms.ToolStripButton redoToolStripButton;
		private System.Windows.Forms.ToolStripButton deleteToolStripButton;
		internal Darwen.Windows.Forms.Controls.TabbedDocuments.TabbedDocumentControl tabbedDocumentControl;
	}
}

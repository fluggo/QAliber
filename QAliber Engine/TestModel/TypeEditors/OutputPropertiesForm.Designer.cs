namespace QAliber.TestModel.TypeEditors
{
	partial class OutputPropertiesForm
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
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.propsListView = new System.Windows.Forms.ListView();
			this.nameColumn = new System.Windows.Forms.ColumnHeader();
			this.varColumn = new System.Windows.Forms.ColumnHeader();
			this.curValColumn = new System.Windows.Forms.ColumnHeader();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.propsListView);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.cancelButton);
			this.splitContainer.Panel2.Controls.Add(this.okButton);
			this.splitContainer.Size = new System.Drawing.Size(654, 403);
			this.splitContainer.SplitterDistance = 373;
			this.splitContainer.TabIndex = 0;
			// 
			// propsListView
			// 
			this.propsListView.CheckBoxes = true;
			this.propsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.nameColumn,
			this.varColumn,
			this.curValColumn});
			this.propsListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propsListView.FullRowSelect = true;
			this.propsListView.GridLines = true;
			this.propsListView.Location = new System.Drawing.Point(0, 0);
			this.propsListView.Name = "propsListView";
			this.propsListView.Size = new System.Drawing.Size(654, 373);
			this.propsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.propsListView.TabIndex = 0;
			this.propsListView.UseCompatibleStateImageBehavior = false;
			this.propsListView.View = System.Windows.Forms.View.Details;
			this.propsListView.Click += new System.EventHandler(this.ListViewClicked);
			// 
			// nameColumn
			// 
			this.nameColumn.Text = "Property Name";
			this.nameColumn.Width = 150;
			// 
			// varColumn
			// 
			this.varColumn.Text = "Variable Name";
			this.varColumn.Width = 200;
			// 
			// curValColumn
			// 
			this.curValColumn.Text = "Current Value";
			this.curValColumn.Width = 300;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(553, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(438, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// OutputPropertiesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(654, 403);
			this.Controls.Add(this.splitContainer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "OutputPropertiesForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Set Results To Variables";
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ListView propsListView;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.ColumnHeader nameColumn;
		private System.Windows.Forms.ColumnHeader varColumn;
		private System.Windows.Forms.ColumnHeader curValColumn;
	}
}
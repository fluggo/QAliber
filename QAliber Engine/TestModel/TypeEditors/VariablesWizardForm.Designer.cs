namespace QAliber.TestModel.TypeEditors
{
	partial class VariablesWizardForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariablesWizardForm));
			this.varTextBox = new System.Windows.Forms.TextBox();
			this.curValueLabel = new System.Windows.Forms.Label();
			this.curValDynLabel = new System.Windows.Forms.Label();
			this.btnSubmit = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.varsListView = new System.Windows.Forms.ListView();
			this.columnHeader = new System.Windows.Forms.ColumnHeader();
			this.varTypesImageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// varTextBox
			// 
			this.varTextBox.Location = new System.Drawing.Point(19, 21);
			this.varTextBox.Name = "varTextBox";
			this.varTextBox.Size = new System.Drawing.Size(492, 20);
			this.varTextBox.TabIndex = 0;
			this.varTextBox.TextChanged += new System.EventHandler(this.varTextBox_TextChanged);
			this.varTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.varTextBox_KeyDown);
			// 
			// curValueLabel
			// 
			this.curValueLabel.AutoSize = true;
			this.curValueLabel.Location = new System.Drawing.Point(368, 48);
			this.curValueLabel.Name = "curValueLabel";
			this.curValueLabel.Size = new System.Drawing.Size(77, 13);
			this.curValueLabel.TabIndex = 2;
			this.curValueLabel.Text = "Current Value :";
			// 
			// curValDynLabel
			// 
			this.curValDynLabel.AutoSize = true;
			this.curValDynLabel.Location = new System.Drawing.Point(446, 48);
			this.curValDynLabel.Name = "curValDynLabel";
			this.curValDynLabel.Size = new System.Drawing.Size(0, 13);
			this.curValDynLabel.TabIndex = 3;
			// 
			// btnSubmit
			// 
			this.btnSubmit.Location = new System.Drawing.Point(371, 137);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(75, 23);
			this.btnSubmit.TabIndex = 1;
			this.btnSubmit.Text = "Submit";
			this.btnSubmit.UseVisualStyleBackColor = true;
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(452, 137);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// varsListView
			// 
			this.varsListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.varsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader});
			this.varsListView.FullRowSelect = true;
			this.varsListView.GridLines = true;
			this.varsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.varsListView.HideSelection = false;
			this.varsListView.HotTracking = true;
			this.varsListView.HoverSelection = true;
			this.varsListView.Location = new System.Drawing.Point(179, 48);
			this.varsListView.MultiSelect = false;
			this.varsListView.Name = "varsListView";
			this.varsListView.ShowItemToolTips = true;
			this.varsListView.Size = new System.Drawing.Size(142, 112);
			this.varsListView.SmallImageList = this.varTypesImageList;
			this.varsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.varsListView.TabIndex = 6;
			this.varsListView.TabStop = false;
			this.varsListView.UseCompatibleStateImageBehavior = false;
			this.varsListView.View = System.Windows.Forms.View.Details;
			this.varsListView.Visible = false;
			this.varsListView.ItemActivate += new System.EventHandler(this.varsListView_ItemActivate);
			this.varsListView.VisibleChanged += new System.EventHandler(this.varsListView_VisibleChanged);
			this.varsListView.SelectedIndexChanged += new System.EventHandler(this.varsListView_SelectedIndexChanged);
			// 
			// columnHeader
			// 
			this.columnHeader.Text = "Auto Completion";
			this.columnHeader.Width = 115;
			// 
			// varTypesImageList
			// 
			this.varTypesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("varTypesImageList.ImageStream")));
			this.varTypesImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.varTypesImageList.Images.SetKeyName(0, "Variable");
			this.varTypesImageList.Images.SetKeyName(1, "List");
			this.varTypesImageList.Images.SetKeyName(2, "Table");
			// 
			// VariablesWizardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(529, 160);
			this.Controls.Add(this.varsListView);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSubmit);
			this.Controls.Add(this.curValDynLabel);
			this.Controls.Add(this.curValueLabel);
			this.Controls.Add(this.varTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "VariablesWizardForm";
			this.Text = "Variables Wizard";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox varTextBox;
		private System.Windows.Forms.Label curValueLabel;
		private System.Windows.Forms.Label curValDynLabel;
		private System.Windows.Forms.Button btnSubmit;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListView varsListView;
		private System.Windows.Forms.ImageList varTypesImageList;
		private System.Windows.Forms.ColumnHeader columnHeader;

	}
}
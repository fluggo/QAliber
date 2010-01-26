namespace QAliber.Builder.Presentation
{
	partial class VariablesPanel
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
			this.varsTabControl = new System.Windows.Forms.TabControl();
			this.varsTabPage = new System.Windows.Forms.TabPage();
			this.varsDataGridView = new System.Windows.Forms.DataGridView();
			this.listsTabPage = new System.Windows.Forms.TabPage();
			this.listsDataGridView = new System.Windows.Forms.DataGridView();
			this.tablesTabPage = new System.Windows.Forms.TabPage();
			this.tablesDataGridView = new System.Windows.Forms.DataGridView();
			this.varsTabControl.SuspendLayout();
			this.varsTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.varsDataGridView)).BeginInit();
			this.listsTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.listsDataGridView)).BeginInit();
			this.tablesTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tablesDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// varsTabControl
			// 
			this.varsTabControl.Controls.Add(this.varsTabPage);
			this.varsTabControl.Controls.Add(this.listsTabPage);
			this.varsTabControl.Controls.Add(this.tablesTabPage);
			this.varsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.varsTabControl.Location = new System.Drawing.Point(0, 0);
			this.varsTabControl.Name = "varsTabControl";
			this.varsTabControl.SelectedIndex = 0;
			this.varsTabControl.Size = new System.Drawing.Size(551, 214);
			this.varsTabControl.TabIndex = 0;
			// 
			// varsTabPage
			// 
			this.varsTabPage.Controls.Add(this.varsDataGridView);
			this.varsTabPage.Location = new System.Drawing.Point(4, 22);
			this.varsTabPage.Name = "varsTabPage";
			this.varsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.varsTabPage.Size = new System.Drawing.Size(543, 188);
			this.varsTabPage.TabIndex = 0;
			this.varsTabPage.Text = "Variables";
			this.varsTabPage.UseVisualStyleBackColor = true;
			// 
			// varsDataGridView
			// 
			this.varsDataGridView.AllowUserToOrderColumns = true;
			this.varsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.varsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.varsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.varsDataGridView.Location = new System.Drawing.Point(3, 3);
			this.varsDataGridView.Name = "varsDataGridView";
			this.varsDataGridView.Size = new System.Drawing.Size(537, 182);
			this.varsDataGridView.TabIndex = 0;
			this.varsDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.varsDataGridView_CellValueChanged);
			this.varsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.varsDataGridView_CellContentClick);
			// 
			// listsTabPage
			// 
			this.listsTabPage.Controls.Add(this.listsDataGridView);
			this.listsTabPage.Location = new System.Drawing.Point(4, 22);
			this.listsTabPage.Name = "listsTabPage";
			this.listsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.listsTabPage.Size = new System.Drawing.Size(543, 188);
			this.listsTabPage.TabIndex = 1;
			this.listsTabPage.Text = "Lists";
			this.listsTabPage.UseVisualStyleBackColor = true;
			// 
			// listsDataGridView
			// 
			this.listsDataGridView.AllowUserToOrderColumns = true;
			this.listsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.listsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.listsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listsDataGridView.Location = new System.Drawing.Point(3, 3);
			this.listsDataGridView.Name = "listsDataGridView";
			this.listsDataGridView.Size = new System.Drawing.Size(537, 182);
			this.listsDataGridView.TabIndex = 0;
			this.listsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.listsDataGridView_CellFormatting);
			this.listsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listsDataGridView_CellContentClick);
			// 
			// tablesTabPage
			// 
			this.tablesTabPage.Controls.Add(this.tablesDataGridView);
			this.tablesTabPage.Location = new System.Drawing.Point(4, 22);
			this.tablesTabPage.Name = "tablesTabPage";
			this.tablesTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.tablesTabPage.Size = new System.Drawing.Size(543, 188);
			this.tablesTabPage.TabIndex = 2;
			this.tablesTabPage.Text = "Tables";
			this.tablesTabPage.UseVisualStyleBackColor = true;
			// 
			// tablesDataGridView
			// 
			this.tablesDataGridView.AllowUserToOrderColumns = true;
			this.tablesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.tablesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tablesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tablesDataGridView.Location = new System.Drawing.Point(3, 3);
			this.tablesDataGridView.Name = "tablesDataGridView";
			this.tablesDataGridView.Size = new System.Drawing.Size(537, 182);
			this.tablesDataGridView.TabIndex = 1;
			this.tablesDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablesDataGridView_CellContentClick);
			// 
			// VariablesPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.varsTabControl);
			this.Name = "VariablesPanel";
			this.Size = new System.Drawing.Size(551, 214);
			this.varsTabControl.ResumeLayout(false);
			this.varsTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.varsDataGridView)).EndInit();
			this.listsTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.listsDataGridView)).EndInit();
			this.tablesTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tablesDataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl varsTabControl;
		private System.Windows.Forms.TabPage varsTabPage;
		private System.Windows.Forms.DataGridView varsDataGridView;
		private System.Windows.Forms.TabPage listsTabPage;
		private System.Windows.Forms.DataGridView listsDataGridView;
		private System.Windows.Forms.TabPage tablesTabPage;
		private System.Windows.Forms.DataGridView tablesDataGridView;
	}
}

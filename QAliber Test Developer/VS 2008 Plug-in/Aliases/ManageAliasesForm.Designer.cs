namespace QAliber.VS2005.Plugin.Aliases
{
	partial class ManageAliasesForm
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
			this.btnOk = new System.Windows.Forms.Button();
			this.txtAlias = new System.Windows.Forms.TextBox();
			this.lblAliasName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(311, 50);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// txtAlias
			// 
			this.txtAlias.Location = new System.Drawing.Point(85, 15);
			this.txtAlias.Name = "txtAlias";
			this.txtAlias.Size = new System.Drawing.Size(304, 20);
			this.txtAlias.TabIndex = 1;
			// 
			// lblAliasName
			// 
			this.lblAliasName.AutoSize = true;
			this.lblAliasName.Location = new System.Drawing.Point(13, 18);
			this.lblAliasName.Name = "lblAliasName";
			this.lblAliasName.Size = new System.Drawing.Size(66, 13);
			this.lblAliasName.TabIndex = 2;
			this.lblAliasName.Text = "Alias Name :";
			// 
			// ManageAliasesForm
			// 
			this.ClientSize = new System.Drawing.Size(398, 85);
			this.Controls.Add(this.lblAliasName);
			this.Controls.Add(this.txtAlias);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ManageAliasesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Enter Alias Name";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtAlias;
		private System.Windows.Forms.Label lblAliasName;

		
	}
}
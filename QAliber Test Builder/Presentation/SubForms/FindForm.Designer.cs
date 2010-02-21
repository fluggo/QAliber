namespace QAliber.Builder.Presentation.SubForms
{
	partial class FindForm
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
			this.lblFindProperty = new System.Windows.Forms.Label();
			this.txtValueFind = new System.Windows.Forms.TextBox();
			this.lblValueFind = new System.Windows.Forms.Label();
			this.btnFind = new System.Windows.Forms.Button();
			this.cmbFindProperty = new System.Windows.Forms.ComboBox();
			this.btnFindPrev = new System.Windows.Forms.Button();
			this.chkExactMatch = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lblFindProperty
			// 
			this.lblFindProperty.AutoSize = true;
			this.lblFindProperty.Location = new System.Drawing.Point(12, 9);
			this.lblFindProperty.Name = "lblFindProperty";
			this.lblFindProperty.Size = new System.Drawing.Size(175, 26);
			this.lblFindProperty.TabIndex = 0;
			this.lblFindProperty.Text = "Property to Find \r\n(as appears in the properties panel )";
			// 
			// txtValueFind
			// 
			this.txtValueFind.Location = new System.Drawing.Point(12, 95);
			this.txtValueFind.Name = "txtValueFind";
			this.txtValueFind.Size = new System.Drawing.Size(175, 20);
			this.txtValueFind.TabIndex = 2;
			// 
			// lblValueFind
			// 
			this.lblValueFind.AutoSize = true;
			this.lblValueFind.Location = new System.Drawing.Point(12, 66);
			this.lblValueFind.Name = "lblValueFind";
			this.lblValueFind.Size = new System.Drawing.Size(175, 26);
			this.lblValueFind.TabIndex = 0;
			this.lblValueFind.Text = "Value to Find\r\n(as appears in the properties panel )";
			// 
			// btnFind
			// 
			this.btnFind.Location = new System.Drawing.Point(112, 153);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(75, 23);
			this.btnFind.TabIndex = 4;
			this.btnFind.Text = "Find Next";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// cmbFindProperty
			// 
			this.cmbFindProperty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbFindProperty.FormattingEnabled = true;
			this.cmbFindProperty.Location = new System.Drawing.Point(15, 42);
			this.cmbFindProperty.Name = "cmbFindProperty";
			this.cmbFindProperty.Size = new System.Drawing.Size(172, 21);
			this.cmbFindProperty.Sorted = true;
			this.cmbFindProperty.TabIndex = 1;
			this.cmbFindProperty.DropDown += new System.EventHandler(this.cmbFindProperty_DropDown);
			// 
			// btnFindPrev
			// 
			this.btnFindPrev.Location = new System.Drawing.Point(12, 153);
			this.btnFindPrev.Name = "btnFindPrev";
			this.btnFindPrev.Size = new System.Drawing.Size(82, 23);
			this.btnFindPrev.TabIndex = 5;
			this.btnFindPrev.Text = "Find Previous";
			this.btnFindPrev.UseVisualStyleBackColor = true;
			this.btnFindPrev.Click += new System.EventHandler(this.btnFindPrev_Click);
			// 
			// chkExactMatch
			// 
			this.chkExactMatch.AutoSize = true;
			this.chkExactMatch.Checked = true;
			this.chkExactMatch.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkExactMatch.Location = new System.Drawing.Point(12, 121);
			this.chkExactMatch.Name = "chkExactMatch";
			this.chkExactMatch.Size = new System.Drawing.Size(86, 17);
			this.chkExactMatch.TabIndex = 3;
			this.chkExactMatch.Text = "Exact Match";
			this.chkExactMatch.UseVisualStyleBackColor = true;
			// 
			// FindForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(200, 188);
			this.Controls.Add(this.chkExactMatch);
			this.Controls.Add(this.btnFindPrev);
			this.Controls.Add(this.cmbFindProperty);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.txtValueFind);
			this.Controls.Add(this.lblValueFind);
			this.Controls.Add(this.lblFindProperty);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FindForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblFindProperty;
		private System.Windows.Forms.TextBox txtValueFind;
		private System.Windows.Forms.Label lblValueFind;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.ComboBox cmbFindProperty;
		private System.Windows.Forms.Button btnFindPrev;
		private System.Windows.Forms.CheckBox chkExactMatch;
	}
}
namespace QAliber.Repository.CommonTestCases.UITypeEditors
{
	partial class UIControlLocatorForm
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
			this.btnCursor = new System.Windows.Forms.Button();
			this.textBox = new System.Windows.Forms.TextBox();
			this.labelHelp = new System.Windows.Forms.Label();
			this.labelPath = new System.Windows.Forms.Label();
			this.labelCoord = new System.Windows.Forms.Label();
			this.labelType = new System.Windows.Forms.Label();
			this.labelTypeDyn = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.textBoxXY = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnCursor
			// 
			this.btnCursor.BackgroundImage = global::QAliber.Repository.CommonTestCases.Properties.Resources.Crosshair;
			this.btnCursor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnCursor.Location = new System.Drawing.Point(485, 26);
			this.btnCursor.Name = "btnCursor";
			this.btnCursor.Size = new System.Drawing.Size(42, 42);
			this.btnCursor.TabIndex = 0;
			this.btnCursor.UseVisualStyleBackColor = true;
			this.btnCursor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCursor_MouseDown);
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(94, 48);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(385, 20);
			this.textBox.TabIndex = 1;
			// 
			// labelHelp
			// 
			this.labelHelp.AutoSize = true;
			this.labelHelp.Location = new System.Drawing.Point(91, 26);
			this.labelHelp.Name = "labelHelp";
			this.labelHelp.Size = new System.Drawing.Size(388, 13);
			this.labelHelp.TabIndex = 2;
			this.labelHelp.Text = "Drag the crosshair icon to select the control you want to perform the operation o" +
				"n";
			// 
			// labelPath
			// 
			this.labelPath.AutoSize = true;
			this.labelPath.Location = new System.Drawing.Point(12, 51);
			this.labelPath.Name = "labelPath";
			this.labelPath.Size = new System.Drawing.Size(65, 13);
			this.labelPath.TabIndex = 3;
			this.labelPath.Text = "Control Path";
			// 
			// labelCoord
			// 
			this.labelCoord.AutoSize = true;
			this.labelCoord.Location = new System.Drawing.Point(12, 84);
			this.labelCoord.Name = "labelCoord";
			this.labelCoord.Size = new System.Drawing.Size(73, 13);
			this.labelCoord.TabIndex = 4;
			this.labelCoord.Text = "Relative Point";
			// 
			// labelType
			// 
			this.labelType.AutoSize = true;
			this.labelType.Location = new System.Drawing.Point(12, 116);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(67, 13);
			this.labelType.TabIndex = 6;
			this.labelType.Text = "Control Type";
			// 
			// labelTypeDyn
			// 
			this.labelTypeDyn.AutoSize = true;
			this.labelTypeDyn.Location = new System.Drawing.Point(91, 116);
			this.labelTypeDyn.Name = "labelTypeDyn";
			this.labelTypeDyn.Size = new System.Drawing.Size(0, 13);
			this.labelTypeDyn.TabIndex = 7;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(452, 111);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 8;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// textBoxXY
			// 
			this.textBoxXY.Location = new System.Drawing.Point(94, 81);
			this.textBoxXY.Name = "textBoxXY";
			this.textBoxXY.ReadOnly = true;
			this.textBoxXY.Size = new System.Drawing.Size(100, 20);
			this.textBoxXY.TabIndex = 9;
			this.textBoxXY.Text = "(X,Y)";
			// 
			// UIControlLocatorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(538, 150);
			this.Controls.Add(this.textBoxXY);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.labelTypeDyn);
			this.Controls.Add(this.labelType);
			this.Controls.Add(this.labelCoord);
			this.Controls.Add(this.labelPath);
			this.Controls.Add(this.labelHelp);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.btnCursor);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "UIControlLocatorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Control Locator";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCursor;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.Label labelHelp;
		private System.Windows.Forms.Label labelPath;
		private System.Windows.Forms.Label labelCoord;
		private System.Windows.Forms.Label labelType;
		private System.Windows.Forms.Label labelTypeDyn;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox textBoxXY;
	}
}
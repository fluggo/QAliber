namespace QAliber.VS2005.Plugin
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.spyControl = new QAliber.VS2005.Plugin.SpyControl();
			this.SuspendLayout();
			// 
			// spyControl
			// 
			this.spyControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spyControl.Location = new System.Drawing.Point(0, 0);
			this.spyControl.Name = "spyControl";
			this.spyControl.Size = new System.Drawing.Size(631, 503);
			this.spyControl.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(631, 503);
			this.Controls.Add(this.spyControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "QAliber Developer Standalone";
			this.ResumeLayout(false);

		}

		#endregion

		private SpyControl spyControl;
	}
}
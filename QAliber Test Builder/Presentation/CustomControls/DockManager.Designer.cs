namespace QAliber.Builder.Presentation
{
	partial class DockManager
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
			this.tabbedScenarioControl = new QAliber.Builder.Presentation.TabbedScenarioControl();
			this.SuspendLayout();
			// 
			// tabbedScenarioControl
			// 
			this.tabbedScenarioControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabbedScenarioControl.Location = new System.Drawing.Point(30, 24);
			this.tabbedScenarioControl.Name = "tabbedScenarioControl";
			this.tabbedScenarioControl.Size = new System.Drawing.Size(460, 419);
			this.tabbedScenarioControl.TabIndex = 14;
			// 
			// DockManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabbedScenarioControl);
			this.Name = "DockManager";
			this.Size = new System.Drawing.Size(520, 467);
			this.Controls.SetChildIndex(this.tabbedScenarioControl, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		internal TabbedScenarioControl tabbedScenarioControl;



	}
}

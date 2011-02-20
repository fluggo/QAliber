namespace QAliber.Runner
{
	partial class Notifier
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
			if (pauseHotKey != null)
				pauseHotKey.Dispose();
			if (stopHotKey != null)
				stopHotKey.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notifier));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.notifierContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.notifierContextMenuStrip.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.notifierContextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "QAliber Test Runner";
			this.notifyIcon.Visible = true;
			this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
			// 
			// notifierContextMenuStrip
			// 
			this.notifierContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.optionsToolStripMenuItem,
			this.playToolStripMenuItem,
			this.pauseToolStripMenuItem,
			this.stopToolStripMenuItem,
			this.toolStripSeparator1,
			this.exitToolStripMenuItem});
			this.notifierContextMenuStrip.Name = "notifierContextMenuStrip";
			this.notifierContextMenuStrip.Size = new System.Drawing.Size(124, 120);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.optionsToolStripMenuItem.Text = "Options...";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// playToolStripMenuItem
			// 
			this.playToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("playToolStripMenuItem.Image")));
			this.playToolStripMenuItem.Name = "playToolStripMenuItem";
			this.playToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.playToolStripMenuItem.Text = "Play ...";
			this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
			// 
			// pauseToolStripMenuItem
			// 
			this.pauseToolStripMenuItem.CheckOnClick = true;
			this.pauseToolStripMenuItem.Enabled = false;
			this.pauseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pauseToolStripMenuItem.Image")));
			this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			this.pauseToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.pauseToolStripMenuItem.Text = "Pause	Ctrl+Alt+F6";
			this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.Enabled = false;
			this.stopToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripMenuItem.Image")));
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.stopToolStripMenuItem.Text = "Stop   Ctrl+Alt+F7";
			this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Scenario Files|*.scn|All Files|*.*";
			this.openFileDialog.Title = "Load a Scenario to Play";
			this.notifierContextMenuStrip.ResumeLayout(false);

		}


		


		
		

		#endregion

		internal System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		internal System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip notifierContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;

	}
}

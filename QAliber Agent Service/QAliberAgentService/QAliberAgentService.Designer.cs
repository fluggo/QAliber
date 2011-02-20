namespace QAliber.AgentService
{
	partial class QAliberAgentService
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QAliberAgentService));
			this.scheduleTimer = new System.Timers.Timer();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.listenerMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.startStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.scheduleTimer)).BeginInit();
			this.listenerMenuStrip.SuspendLayout();
			// 
			// scheduleTimer
			// 
			this.scheduleTimer.Enabled = true;
			this.scheduleTimer.Interval = 2000;
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.listenerMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "QAliber Listener";
			this.notifyIcon.Visible = true;
			// 
			// listenerMenuStrip
			// 
			this.listenerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.startStripMenuItem,
			this.stopStripMenuItem,
			this.toolStripSeparator1,
			this.exitStripMenuItem});
			this.listenerMenuStrip.Name = "listenerMenuStrip";
			this.listenerMenuStrip.Size = new System.Drawing.Size(144, 76);
			// 
			// startStripMenuItem
			// 
			this.startStripMenuItem.Name = "startStripMenuItem";
			this.startStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.startStripMenuItem.Text = "Start Listening";
			this.startStripMenuItem.Click += new System.EventHandler(OnStart);
			// 
			// stopStripMenuItem
			// 
			this.stopStripMenuItem.Name = "stopStripMenuItem";
			this.stopStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.stopStripMenuItem.Text = "Stop Listening";
			this.stopStripMenuItem.Click += new System.EventHandler(OnStop);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
			// 
			// exitStripMenuItem
			// 
			this.exitStripMenuItem.Name = "exitStripMenuItem";
			this.exitStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.exitStripMenuItem.Text = "Exit";
			this.exitStripMenuItem.Click += new System.EventHandler(OnExit);
			((System.ComponentModel.ISupportInitialize)(this.scheduleTimer)).EndInit();
			this.listenerMenuStrip.ResumeLayout(false);

		}

		#endregion

		private System.Timers.Timer scheduleTimer;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip listenerMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem startStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitStripMenuItem;
	}
}

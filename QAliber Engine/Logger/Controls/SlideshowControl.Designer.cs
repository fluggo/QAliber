/*	  
 * Copyright (C) 2010 QAlibers (C) http://qaliber.net
 * This file is part of QAliber.
 * QAliber is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * QAliber is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License
 * along with QAliber.	If not, see <http://www.gnu.org/licenses/>.
 */
 
namespace QAliber.Logger.Controls
{
	partial class SlideshowControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlideshowControl));
			this.groupBoxVideo = new System.Windows.Forms.GroupBox();
			this.pictureBoxFrame = new System.Windows.Forms.PictureBox();
			this.frameContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.goToClosestLogMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showFramInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panelCommands = new System.Windows.Forms.Panel();
			this.labelDate = new System.Windows.Forms.Label();
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonPause = new System.Windows.Forms.Button();
			this.buttonPlay = new System.Windows.Forms.Button();
			this.labelPosition = new System.Windows.Forms.Label();
			this.trackBarSeek = new System.Windows.Forms.TrackBar();
			this.groupBoxVideo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrame)).BeginInit();
			this.frameContextMenuStrip.SuspendLayout();
			this.panelCommands.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSeek)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBoxVideo
			// 
			this.groupBoxVideo.Controls.Add(this.pictureBoxFrame);
			this.groupBoxVideo.Controls.Add(this.panelCommands);
			this.groupBoxVideo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxVideo.Location = new System.Drawing.Point(0, 0);
			this.groupBoxVideo.Name = "groupBoxVideo";
			this.groupBoxVideo.Size = new System.Drawing.Size(597, 374);
			this.groupBoxVideo.TabIndex = 0;
			this.groupBoxVideo.TabStop = false;
			this.groupBoxVideo.Text = "Video";
			// 
			// pictureBoxFrame
			// 
			this.pictureBoxFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBoxFrame.ContextMenuStrip = this.frameContextMenuStrip;
			this.pictureBoxFrame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxFrame.Location = new System.Drawing.Point(3, 16);
			this.pictureBoxFrame.Name = "pictureBoxFrame";
			this.pictureBoxFrame.Size = new System.Drawing.Size(591, 303);
			this.pictureBoxFrame.TabIndex = 1;
			this.pictureBoxFrame.TabStop = false;
			// 
			// frameContextMenuStrip
			// 
			this.frameContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.goToClosestLogMessageToolStripMenuItem,
			this.showFramInExplorerToolStripMenuItem,
			this.exportToToolStripMenuItem,
			this.fullScreenToolStripMenuItem});
			this.frameContextMenuStrip.Name = "frameContextMenuStrip";
			this.frameContextMenuStrip.Size = new System.Drawing.Size(190, 92);
			// 
			// goToClosestLogMessageToolStripMenuItem
			// 
			this.goToClosestLogMessageToolStripMenuItem.Name = "goToClosestLogMessageToolStripMenuItem";
			this.goToClosestLogMessageToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.goToClosestLogMessageToolStripMenuItem.Text = "Show Closest Message";
			this.goToClosestLogMessageToolStripMenuItem.Click += new System.EventHandler(this.goToClosestLogMessageToolStripMenuItem_Click);
			// 
			// showFramInExplorerToolStripMenuItem
			// 
			this.showFramInExplorerToolStripMenuItem.Name = "showFramInExplorerToolStripMenuItem";
			this.showFramInExplorerToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.showFramInExplorerToolStripMenuItem.Text = "Show Frame In Explorer";
			this.showFramInExplorerToolStripMenuItem.Click += new System.EventHandler(this.showFramInExplorerToolStripMenuItem_Click);
			// 
			// exportToToolStripMenuItem
			// 
			this.exportToToolStripMenuItem.Enabled = false;
			this.exportToToolStripMenuItem.Name = "exportToToolStripMenuItem";
			this.exportToToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.exportToToolStripMenuItem.Text = "Export to Video ...";
			// 
			// fullScreenToolStripMenuItem
			// 
			this.fullScreenToolStripMenuItem.CheckOnClick = true;
			this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
			this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.fullScreenToolStripMenuItem.Text = "Full Screen";
			this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.fullScreenToolStripMenuItem_Click);
			// 
			// panelCommands
			// 
			this.panelCommands.Controls.Add(this.labelDate);
			this.panelCommands.Controls.Add(this.buttonStop);
			this.panelCommands.Controls.Add(this.buttonPause);
			this.panelCommands.Controls.Add(this.buttonPlay);
			this.panelCommands.Controls.Add(this.labelPosition);
			this.panelCommands.Controls.Add(this.trackBarSeek);
			this.panelCommands.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelCommands.Location = new System.Drawing.Point(3, 319);
			this.panelCommands.Name = "panelCommands";
			this.panelCommands.Size = new System.Drawing.Size(591, 52);
			this.panelCommands.TabIndex = 0;
			this.panelCommands.SizeChanged += new System.EventHandler(this.panelCommands_SizeChanged);
			// 
			// labelDate
			// 
			this.labelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelDate.AutoSize = true;
			this.labelDate.Location = new System.Drawing.Point(453, 26);
			this.labelDate.Name = "labelDate";
			this.labelDate.Size = new System.Drawing.Size(0, 13);
			this.labelDate.TabIndex = 5;
			// 
			// buttonStop
			// 
			this.buttonStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonStop.Enabled = false;
			this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
			this.buttonStop.Location = new System.Drawing.Point(75, 7);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(32, 32);
			this.buttonStop.TabIndex = 4;
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// buttonPause
			// 
			this.buttonPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonPause.Enabled = false;
			this.buttonPause.Image = ((System.Drawing.Image)(resources.GetObject("buttonPause.Image")));
			this.buttonPause.Location = new System.Drawing.Point(38, 7);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(32, 32);
			this.buttonPause.TabIndex = 3;
			this.buttonPause.UseVisualStyleBackColor = true;
			this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
			// 
			// buttonPlay
			// 
			this.buttonPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.buttonPlay.Image = ((System.Drawing.Image)(resources.GetObject("buttonPlay.Image")));
			this.buttonPlay.Location = new System.Drawing.Point(3, 7);
			this.buttonPlay.Name = "buttonPlay";
			this.buttonPlay.Size = new System.Drawing.Size(32, 32);
			this.buttonPlay.TabIndex = 2;
			this.buttonPlay.UseVisualStyleBackColor = true;
			this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
			// 
			// labelPosition
			// 
			this.labelPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelPosition.AutoSize = true;
			this.labelPosition.Location = new System.Drawing.Point(453, 7);
			this.labelPosition.Name = "labelPosition";
			this.labelPosition.Size = new System.Drawing.Size(0, 13);
			this.labelPosition.TabIndex = 1;
			// 
			// trackBarSeek
			// 
			this.trackBarSeek.BackColor = System.Drawing.SystemColors.Control;
			this.trackBarSeek.Location = new System.Drawing.Point(110, 7);
			this.trackBarSeek.Name = "trackBarSeek";
			this.trackBarSeek.Size = new System.Drawing.Size(291, 42);
			this.trackBarSeek.TabIndex = 0;
			this.trackBarSeek.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarSeek.Scroll += new System.EventHandler(this.trackBarSeek_Scroll);
			// 
			// SlideshowControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBoxVideo);
			this.Name = "SlideshowControl";
			this.Size = new System.Drawing.Size(597, 374);
			this.groupBoxVideo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrame)).EndInit();
			this.frameContextMenuStrip.ResumeLayout(false);
			this.panelCommands.ResumeLayout(false);
			this.panelCommands.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSeek)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxVideo;
		private System.Windows.Forms.Panel panelCommands;
		private System.Windows.Forms.TrackBar trackBarSeek;
		private System.Windows.Forms.Button buttonPlay;
		private System.Windows.Forms.Label labelPosition;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonPause;
		private System.Windows.Forms.PictureBox pictureBoxFrame;
		private System.Windows.Forms.Label labelDate;
		private System.Windows.Forms.ContextMenuStrip frameContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem goToClosestLogMessageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showFramInExplorerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
	}
}

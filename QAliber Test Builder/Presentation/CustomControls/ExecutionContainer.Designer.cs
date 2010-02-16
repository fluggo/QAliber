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
 
namespace QAliber.Builder.Presentation
{
	partial class ExecutionContainer
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
				playHotKey.Dispose();
				pauseHotKey.Dispose();
				stopHotKey.Dispose();
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
			System.Windows.Forms.ToolStripProfessionalRenderer toolStripProfessionalRenderer1 = new System.Windows.Forms.ToolStripProfessionalRenderer();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.pauseToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.statusToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.dockManager = new QAliber.Builder.Presentation.DockManager();
			this.debugPlayToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.debugPlayToolStripButton,
			this.pauseToolStripButton,
			this.stopToolStripButton,
			this.statusToolStripLabel});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(389, 25);
			this.toolStrip.TabIndex = 0;
			// 
			// pauseToolStripButton
			// 
			this.pauseToolStripButton.CheckOnClick = true;
			this.pauseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pauseToolStripButton.Enabled = false;
			this.pauseToolStripButton.Image = global::QAliber.Builder.Presentation.Properties.Resources.Pause;
			this.pauseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pauseToolStripButton.Name = "pauseToolStripButton";
			this.pauseToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.pauseToolStripButton.Text = "Pause";
			this.pauseToolStripButton.Click += new System.EventHandler(this.pauseToolStripButton_Click);
			// 
			// stopToolStripButton
			// 
			this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.stopToolStripButton.Enabled = false;
			this.stopToolStripButton.Image = global::QAliber.Builder.Presentation.Properties.Resources.Stop;
			this.stopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.stopToolStripButton.Name = "stopToolStripButton";
			this.stopToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.stopToolStripButton.Text = "Stop";
			this.stopToolStripButton.Click += new System.EventHandler(this.stopToolStripButton_Click);
			// 
			// statusToolStripLabel
			// 
			this.statusToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.statusToolStripLabel.Name = "statusToolStripLabel";
			this.statusToolStripLabel.Size = new System.Drawing.Size(25, 22);
			this.statusToolStripLabel.Text = "Idle";
			// 
			// dockManager
			// 
			this.dockManager.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockManager.Location = new System.Drawing.Point(0, 25);
			this.dockManager.Name = "dockManager";
			toolStripProfessionalRenderer1.RoundedEdges = true;
			this.dockManager.Renderer = toolStripProfessionalRenderer1;
			this.dockManager.Size = new System.Drawing.Size(389, 361);
			this.dockManager.TabIndex = 1;
			// 
			// debugPlayToolStripButton
			// 
			this.debugPlayToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.debugPlayToolStripButton.Image = global::QAliber.Builder.Presentation.Properties.Resources.DebugPlay;
			this.debugPlayToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.debugPlayToolStripButton.Name = "debugPlayToolStripButton";
			this.debugPlayToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.debugPlayToolStripButton.Text = "Debug";
			this.debugPlayToolStripButton.ToolTipText = "Play";
			this.debugPlayToolStripButton.Click += new System.EventHandler(this.debugPlayToolStripButton_Click);
			// 
			// ExecutionContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dockManager);
			this.Controls.Add(this.toolStrip);
			this.Name = "ExecutionContainer";
			this.Size = new System.Drawing.Size(389, 386);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		internal DockManager dockManager;
		internal System.Windows.Forms.ToolStripButton pauseToolStripButton;
		internal System.Windows.Forms.ToolStripButton stopToolStripButton;
		internal System.Windows.Forms.ToolStripLabel statusToolStripLabel;
		internal System.Windows.Forms.ToolStripButton debugPlayToolStripButton;
	}
}

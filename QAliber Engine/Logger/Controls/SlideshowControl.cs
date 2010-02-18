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
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace QAliber.Logger.Controls
{
	public partial class SlideshowControl : UserControl, IComparer<FileInfo>
	{
		public SlideshowControl()
		{
			InitializeComponent();
			
			
		}

		public void Init(string logPath)
		{
			this.logPath = logPath;
			logPath += @"\Video\";
			frameTimes.Clear();
			if (Directory.Exists(logPath))
			{
				foreach (string picFile in Directory.GetFiles(logPath, "*.jpg"))
				{
					FileInfo fileInfo = new FileInfo(picFile);
					frameTimes.Add(fileInfo);
				}
				if (frameTimes.Count > 0)
				{
					Visible = true;
					BringToFront();

					frameTimes.Sort(this);
					duration = frameTimes[frameTimes.Count - 1].CreationTime - frameTimes[0].CreationTime;
					durationStr = GetSpanString(duration);
					trackBarSeek.Minimum = 0;
					trackBarSeek.Maximum = frameTimes.Count - 1;
				}
			}
			
		}

		public void SeekClosestFrameByTime(DateTime time)
		{
			int i = 1;
			bool found = false;
			for (; i < frameTimes.Count; i++)
			{
				if (frameTimes[i - 1].CreationTime < time && frameTimes[i].CreationTime >= time)
				{
					found = true;
					break;
				}

			}
			if (found)
				trackBarSeek.Value = i - 1;
			else
				trackBarSeek.Value = 0;
			trackBarSeek_Scroll(null, EventArgs.Empty);
		}

		private void trackBarSeek_Scroll(object sender, EventArgs e)
		{
			pictureBoxFrame.BackgroundImage = Bitmap.FromFile(frameTimes[trackBarSeek.Value].FullName);
			TimeSpan currentSpan = frameTimes[trackBarSeek.Value].CreationTime - frameTimes[0].CreationTime;
			string spanStr = GetSpanString(currentSpan);
			labelPosition.Text = spanStr + " / " + durationStr;
			labelDate.Text = frameTimes[trackBarSeek.Value].CreationTime.ToString();
		}

		private void buttonPlay_Click(object sender, EventArgs e)
		{
			paused = false;
			stopped = false;
			buttonPlay.Enabled = false;
			buttonPause.Enabled = true;
			buttonStop.Enabled = true;
			new Thread(new ThreadStart(PlayWorker)).Start();
			
			
		}

		private void buttonPause_Click(object sender, EventArgs e)
		{
			if (buttonPause.FlatStyle == FlatStyle.Standard)
			{
				paused = true;
				buttonPause.FlatStyle = FlatStyle.Flat;
			}
			else
			{
				paused = false;
				buttonPause.FlatStyle = FlatStyle.Standard;
				new Thread(new ThreadStart(PlayWorker)).Start();
				
			}
			buttonPlay.Enabled = false;
			buttonPause.Enabled = true;
			buttonStop.Enabled = true;
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			stopped = true;
		}

		private void PlayWorker()
		{
			if (trackBarSeek.Value == trackBarSeek.Maximum)
				trackBarSeek.Value = 0;
			while (trackBarSeek.Value < trackBarSeek.Maximum && !paused && !stopped)
			{
				trackBarSeek_Scroll(this, EventArgs.Empty);
				trackBarSeek.Value++;
				Thread.Sleep(300);
			}
			if (!paused)
			{
				buttonPlay.Enabled = true;
				buttonPause.Enabled = false;
				buttonStop.Enabled = false;
			}
			if (stopped)
				trackBarSeek.Value = 0;
			
		}

		private string GetSpanString(TimeSpan span)
		{
			string sp = span.ToString();
			if (sp.IndexOf(".") >= 0)
				sp = sp.Remove(sp.IndexOf("."));
			return sp;
		}

		private LogViewerControl GetParentControl()
		{
			Control parent = Parent;
			while (parent != null)
			{
				if (parent is LogViewerControl)
					return (LogViewerControl)parent;
				parent = parent.Parent;
			}
			return null;
		}

		#region Events
		private List<FileInfo> frameTimes = new List<FileInfo>();

		private void panelCommands_SizeChanged(object sender, EventArgs e)
		{
			trackBarSeek.Size = new Size(labelDate.Location.X - buttonStop.Location.X - 30, trackBarSeek.Size.Height);
		}

		private void showFramInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (File.Exists(frameTimes[trackBarSeek.Value].FullName))
				System.Diagnostics.Process.Start("explorer", "/select, \"" + frameTimes[trackBarSeek.Value].FullName + "\"");
		}

		private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form form = FindForm();
			if (fullScreenToolStripMenuItem.Checked)
			{
				parent = Parent;
				form.WindowState = FormWindowState.Maximized;
				Parent.Controls.Remove(this);
				form.Controls.Add(this);
				this.Bounds = new Rectangle(0, 0, form.Width, form.Height);
				this.BringToFront();
			}
			else
			{
				form.Controls.Remove(this);
				parent.Controls.Add(this);
				Dock = DockStyle.Fill;
				this.BringToFront();
			}
		}

		private void goToClosestLogMessageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LogViewerControl lvc = GetParentControl();
			if (lvc != null)
			{
				lvc.SelectNodeByDate(frameTimes[trackBarSeek.Value].CreationTime);
			}

		}

		private void goToClosestLogErrorToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		#endregion

		#region IComparer<FileInfo> Members

		public int Compare(FileInfo x, FileInfo y)
		{
			return (int)(x.CreationTime.Ticks - y.CreationTime.Ticks);
		}

		#endregion

		private volatile bool paused;
		private volatile bool stopped;
		private TimeSpan duration;
		private string durationStr;
		private string logPath;
		private Control parent;

		


	   
	}
}

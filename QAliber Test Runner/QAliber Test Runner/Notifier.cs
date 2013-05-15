using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using QAliber.TestModel;

namespace QAliber.Runner
{
	public delegate void StateDelegate(ExecutionState state);

	public partial class Notifier : Component
	{
		public Notifier()
		{
			InitializeComponent();
			InitHotKeys();
			runner = new Runner(this);
		}

		public Notifier(IContainer container)
		{
			container.Add(this);
			
			InitializeComponent();
		}

		public void ChangeEnabledState(ExecutionState state)
		{
			if (notifierContextMenuStrip.InvokeRequired)
			{
				notifierContextMenuStrip.Invoke(new StateDelegate(ChangeEnabledState), state);
			}
			else
			{
				switch (state)
				{
					case ExecutionState.InProgress:
						notifyIcon.BalloonTipTitle = "Automated Scenario is Running ...";
						notifyIcon.Icon = Properties.Resources.PlayIcon;
						if (!string.IsNullOrEmpty(notifyIcon.BalloonTipText))
							notifyIcon.ShowBalloonTip(int.MaxValue);
						playToolStripMenuItem.Enabled = false;
						pauseToolStripMenuItem.Enabled = true;
						stopToolStripMenuItem.Enabled = true;
						break;
					case ExecutionState.Paused:
						notifyIcon.BalloonTipTitle = "Automated Scenario Paused";
						notifyIcon.Icon = Properties.Resources.PauseIcon;
						if (!string.IsNullOrEmpty(notifyIcon.BalloonTipText))
							notifyIcon.ShowBalloonTip(int.MaxValue);
						playToolStripMenuItem.Enabled = false;
						pauseToolStripMenuItem.Enabled = true;
						stopToolStripMenuItem.Enabled = true;
						break;
					case ExecutionState.Executed:
						notifyIcon.BalloonTipTitle = "Automated Run Ended";
						notifyIcon.Icon = Properties.Resources.AlertIcon;
						if (!string.IsNullOrEmpty(notifyIcon.BalloonTipText))
							notifyIcon.ShowBalloonTip(int.MaxValue);
						playToolStripMenuItem.Enabled = true;
						pauseToolStripMenuItem.Enabled = false;
						stopToolStripMenuItem.Enabled = false;
						break;
				}
			}
		}

		private void InitHotKeys()
		{
			try
			{
				
				pauseHotKey = new ManagedWinapi.Hotkey();
				pauseHotKey.Ctrl = true;
				pauseHotKey.Alt = true;
				pauseHotKey.KeyCode = System.Windows.Forms.Keys.F6;
				pauseHotKey.Enabled = true;
				pauseHotKey.HotkeyPressed += new EventHandler(PauseHotKeyPressed);

				stopHotKey = new ManagedWinapi.Hotkey();
				stopHotKey.Ctrl = true;
				stopHotKey.Alt = true;
				stopHotKey.KeyCode = System.Windows.Forms.Keys.F7;
				stopHotKey.Enabled = true;
				stopHotKey.HotkeyPressed += new EventHandler(StopHotKeyPressed);
			}
			catch (ManagedWinapi.HotkeyAlreadyInUseException)
			{
			}
		}

		private void PauseHotKeyPressed(object sender, EventArgs e)
		{
			if (pauseToolStripMenuItem.Enabled)
			{
				pauseToolStripMenuItem.Checked = !pauseToolStripMenuItem.Checked;
				pauseToolStripMenuItem_Click(this, EventArgs.Empty);
			}
		}

		private void StopHotKeyPressed(object sender, EventArgs e)
		{
			if (stopToolStripMenuItem.Enabled)
			{
				stopToolStripMenuItem_Click(this, EventArgs.Empty);
			}
		}

		private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
		{
			if (!logShowFailed)
			{
				try
				{
					if (!string.IsNullOrEmpty(QAliber.Logger.Log.Default.Filename) && Process.GetProcessesByName("QAliber Log Viewer").Length == 0)
						Process.Start(QAliber.Logger.Log.Default.Filename);
				}
				catch
				{
					System.Windows.Forms.MessageBox.Show("Error while loading log viewer", "Error");
					logShowFailed = true;
				}
			}
		}

		private void optionsToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			BuilderOptionsForm form = new BuilderOptionsForm();
			form.ShowDialog();
		}

		private void playToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				runner.Run(0, 0, TestModel.TestController.Default.RemoteAssemblyDirectory, openFileDialog.FileName);
			}
		}

		private void stopToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TestModel.TestController.Default.Stop();
		}

		private void pauseToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (pauseToolStripMenuItem.Checked)
				TestModel.TestController.Default.Pause();
			else
				TestModel.TestController.Default.Resume();
		}

		private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
		{

			System.Windows.Forms.Application.Exit();
		}

		private bool logShowFailed;
		internal ManagedWinapi.Hotkey pauseHotKey;
		internal ManagedWinapi.Hotkey stopHotKey;
		internal Runner runner;
	}

	
}

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
using System.Runtime.Remoting;
using QAliber.TestModel;
using QAliber.RemotingModel;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using QAliber.Logger;
using QAliber.Logger.Controls;
using Darwen.Windows.Forms.Controls.TabbedDocuments;

namespace QAliber.Builder.Presentation
{
	public partial class ExecutionContainer : UserControl
	{
		public ExecutionContainer()
		{
			InitializeComponent();
			InitHotkeys();
			sink = new NotifySink();
			sink.Control = this;
			TestController.Default.OnExecutionStateChanged += new ExecutionStateChangedCallback(sink.FireExecutionStateChangedCallback);
			TestController.Default.OnStepStarted += new StepStartedCallback(sink.FireStepStartedCallback);
			TestController.Default.OnStepResultArrived += new StepResultArrivedCallback(sink.FireStepResultArrivedCallback);
			TestController.Default.OnLogResultArrived += new LogResultArrivedCallback(sink.FireLogResultArrivedCallback);
			TestController.Default.OnBreakPointReached += new BreakPointReachedCallback(sink.FireBreakPointReachedCallback);

		}

		internal void playToolStripButton_Click(object sender, EventArgs e)
		{
			try
			{
				
				currentPlayingScenarioControl = dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
				if (currentPlayingScenarioControl != null)
				{
					if (Properties.Settings.Default.MinimizeOnRun)
					{
						winState = FindForm().WindowState;
						FindForm().WindowState = FormWindowState.Minimized;
					}
					dockManager.tabbedScenarioControl.SaveItem(currentPlayingScenarioControl);
					//TODO : extern to cfg
					TestController.Default.Run(currentPlayingScenarioControl.TestScenario.Filename);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not save!\n" + ex.Message, "Error While Saving");
			}
		}

		internal void debugPlayToolStripButton_Click(object sender, EventArgs e)
		{
			
			currentPlayingScenarioControl = dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl as ScenarioControl;
			if (currentPlayingScenarioControl != null)
			{
				if (Properties.Settings.Default.MinimizeOnRun)
				{
					winState = FindForm().WindowState;
					FindForm().WindowState = FormWindowState.Minimized;
				}
				if (isInBP)
				{
					TestController.Default.ContinueFromBreakPoint();
					debugPlayToolStripButton.Enabled = false;
					pauseToolStripButton.Enabled = true;
					stopToolStripButton.Enabled = true;
					statusToolStripLabel.Text = "Scenario In Progress";
					isInBP = false;
				}
				else
				{
					//currentPlayingScenarioControl.SetNodeIDs();
					TestController.Default.Run(currentPlayingScenarioControl.TestScenario.RootTestCase);
				}
			}
		}

		internal void pauseToolStripButton_Click(object sender, EventArgs e)
		{
			if (!pauseToolStripButton.Checked)
				TestController.Default.Resume();
			else
				TestController.Default.Pause();
		}

		internal void stopToolStripButton_Click(object sender, EventArgs e)
		{
			if (isInBP)
			{
				TestController.Default.ContinueFromBreakPoint();
				isInBP = false;
			}
			TestController.Default.Stop();
		}

		void PlayHotKeyPressed(object sender, EventArgs e)
		{
			if (debugPlayToolStripButton.Enabled)
				debugPlayToolStripButton_Click(this, EventArgs.Empty);
		}

		void PauseHotKeyPressed(object sender, EventArgs e)
		{
			if (pauseToolStripButton.Enabled)
			{
				pauseToolStripButton.Checked = !pauseToolStripButton.Checked;
				pauseToolStripButton_Click(this, EventArgs.Empty);
			}
		}

		void StopHotKeyPressed(object sender, EventArgs e)
		{
			if (stopToolStripButton.Enabled)
				stopToolStripButton_Click(this, EventArgs.Empty);
		}

		private void InitHotkeys()
		{
			try
			{
				playHotKey = new ManagedWinapi.Hotkey();
				playHotKey.Ctrl = true;
				playHotKey.Alt = true;
				playHotKey.KeyCode = Keys.F5;
				playHotKey.Enabled = true;
				playHotKey.HotkeyPressed += new EventHandler(PlayHotKeyPressed);

				pauseHotKey = new ManagedWinapi.Hotkey();
				pauseHotKey.Ctrl = true;
				pauseHotKey.Alt = true;
				pauseHotKey.KeyCode = Keys.F6;
				pauseHotKey.Enabled = true;
				pauseHotKey.HotkeyPressed += new EventHandler(PauseHotKeyPressed);

				stopHotKey = new ManagedWinapi.Hotkey();
				stopHotKey.Ctrl = true;
				stopHotKey.Alt = true;
				stopHotKey.KeyCode = Keys.F7;
				stopHotKey.Enabled = true;
				stopHotKey.HotkeyPressed += new EventHandler(StopHotKeyPressed);
			}
			catch (ManagedWinapi.HotkeyAlreadyInUseException)
			{
			}

		}

		

		internal ScenarioControl currentPlayingScenarioControl;
		internal ManagedWinapi.Hotkey playHotKey;
		internal ManagedWinapi.Hotkey pauseHotKey;
		internal ManagedWinapi.Hotkey stopHotKey;
		internal FormWindowState winState;

		internal bool isInBP = false;

		private NotifySink sink;
		

		

		
	}

	class NotifySink : NotifyCallbackSink
	{

		private ExecutionContainer control;

		public ExecutionContainer Control
		{
			get { return control; }
			set { control = value; }
		}
	
		/// <summary>
		/// Events from the server call into here. This is not in the GUI thread.
		/// </summary>
		/// <param name="s">Pass a string for testing</param>
		protected override void OnExecutionStateChanged(ExecutionState state)
		{
			if (control != null)
			{
				//System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
				if (control.InvokeRequired)
					control.Invoke(new ExecutionStateChangedCallback(this.OnExecutionStateChanged), state);
				else
				{
					switch (state)
					{
						case ExecutionState.Paused:
							control.debugPlayToolStripButton.Enabled = false;
							control.pauseToolStripButton.Enabled = true;
							control.pauseToolStripButton.Checked = true;
							control.stopToolStripButton.Enabled = true;

							control.statusToolStripLabel.Text = "Scenario Paused";
							break;
						case ExecutionState.Executed:
							control.debugPlayToolStripButton.Enabled = true;
							control.pauseToolStripButton.Enabled = false;
							control.stopToolStripButton.Enabled = false;
							
							control.statusToolStripLabel.Text = "Scenario Ended";
							DelayedActivation();
							break;
						case ExecutionState.NotExecuted:
							control.debugPlayToolStripButton.Enabled = true;
							control.pauseToolStripButton.Enabled = false;
							control.stopToolStripButton.Enabled = false;
							
							control.statusToolStripLabel.Text = "Idle";
							break;

						case ExecutionState.InProgress:
							control.debugPlayToolStripButton.Enabled = false;
							control.pauseToolStripButton.Enabled = true;
							control.pauseToolStripButton.Checked = false;
							control.stopToolStripButton.Enabled = true;
							
							control.statusToolStripLabel.Text = "Scenario In Progress";
							break;
						default:
							break;
					}
				}
			}

		}

		protected override void OnStepResultArrived(TestCaseResult result)
		{
			if (control != null)
			{
				if (control.InvokeRequired)
					control.Invoke(new StepResultArrivedCallback(OnStepResultArrived), result);
				else 
					if (control != null && control.currentPlayingScenarioControl != null)
					control.currentPlayingScenarioControl.OnStepResultArrived(result);
			}
		}

		protected override void OnStepStarted(int id)
		{
			if (control != null)
			{
				if (control.InvokeRequired)
					control.Invoke(new StepStartedCallback(OnStepStarted), id);
				else
					if (control != null && control.currentPlayingScenarioControl != null)
						control.currentPlayingScenarioControl.OnStepStarted(id);
			}
		}

		protected override void OnLogResultArrived(string logFile)
		{
			if (control != null)
			{
				if (control.InvokeRequired)
					control.Invoke(new LogResultArrivedCallback(OnLogResultArrived), logFile);
				else if (System.IO.File.Exists(logFile))
				{
					string title = string.Empty;
					if (control.currentPlayingScenarioControl != null)
						title = "Log - " + control.currentPlayingScenarioControl.TestScenario.RootTestCase.Name;
					else
						title = "Log - " + System.IO.Path.GetFileNameWithoutExtension(logFile);

					foreach (Control c in control.dockManager.tabbedScenarioControl.tabbedDocumentControl.Items)
					{
						
						LogViewerControl lvc = c as LogViewerControl;
						if (lvc != null)
						{
							if (string.Compare(title, control.dockManager.tabbedScenarioControl.tabbedDocumentControl.ItemTitles[lvc]) == 0)
							{
								lvc.Filename = logFile;
								if (Properties.Settings.Default.ShowLogAfter)
									control.dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl = lvc;
								
								return;
							}
						}
					}
					LogViewerControl logViewerControl = new LogViewerControl();
					logViewerControl.Filename = logFile;
					control.dockManager.tabbedScenarioControl.tabbedDocumentControl.Items.Add(title, logViewerControl);
					if (Properties.Settings.Default.ShowLogAfter)
						control.dockManager.tabbedScenarioControl.tabbedDocumentControl.SelectedControl = logViewerControl;
					

				}
			}
		}

		protected override void OnBreakPointReached()
		{
			if (control != null)
			{
				if (control.InvokeRequired)
					control.Invoke(new BreakPointReachedCallback(OnBreakPointReached));
				else
					if (control != null && control.currentPlayingScenarioControl != null)
					{
						control.currentPlayingScenarioControl.OnBreakPointReached();
						control.isInBP = true;
						control.debugPlayToolStripButton.Enabled = true;
						control.pauseToolStripButton.Enabled = false;
						control.stopToolStripButton.Enabled = true;
						control.statusToolStripLabel.Text = "Breakpoint Reached";
						control.FindForm().WindowState = control.winState;
					}
			}
		}

		private void DelayedActivation()
		{
			new System.Threading.Thread(new System.Threading.ThreadStart(DelayedActivationWorker)).Start();
		}

		private void DelayedActivationWorker()
		{
			System.Threading.Thread.Sleep(500);
			if (control.InvokeRequired)
				control.Invoke(new System.Threading.ThreadStart(DelayedActivationWorker));
			else
			{
				control.FindForm().WindowState = control.winState;
				control.FindForm().Activate();
			}
		}
	}
}

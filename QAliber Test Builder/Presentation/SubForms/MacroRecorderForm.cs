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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QAliber.Recorder.MacroRecorder;
using QAliber.TestModel;

namespace QAliber.Builder.Presentation.SubForms
{
	public partial class MacroRecorderForm : Form
	{
		public MacroRecorderForm()
		{
			InitializeComponent();
			InitDatagrid();
			InitHotkeys();
			openFileDialog.InitialDirectory = TestController.Default.RemoteAssemblyDirectory;
			saveFileDialog.InitialDirectory = TestController.Default.RemoteAssemblyDirectory;
			converter = new MacroDataTableConverter(recorder, table);

		}

		private void InitHotkeys()
		{
			try
			{
				
				stopHotKey = new ManagedWinapi.Hotkey();
				stopHotKey.Ctrl = true;
				stopHotKey.Alt = true;
				stopHotKey.KeyCode = Keys.D7;
				stopHotKey.Enabled = true;
				stopHotKey.HotkeyPressed += new EventHandler(StopHotKeyPressed);
			}
			catch (ManagedWinapi.HotkeyAlreadyInUseException)
			{
			}

		}

		
		private void InitDatagrid()
		{
			macroBindingSource.DataSource = table;

			((DataGridViewComboBoxColumn)dataGridView.Columns["Action"]).Items.Clear();
			foreach (string item in Enum.GetNames(typeof(QAliber.Engine.Win32.MouseEvents)))
			{
				((DataGridViewComboBoxColumn)dataGridView.Columns["Action"]).Items.Add(item);
			}
			((DataGridViewComboBoxColumn)dataGridView.Columns["Key"]).Items.Clear();
			foreach (string item in Enum.GetNames(typeof(System.Windows.Input.Key)))
			{
				((DataGridViewComboBoxColumn)dataGridView.Columns["Key"]).Items.Add(item);
			}


		}

		private void toolStripButtonRecord_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
			notifyIcon.Visible = true;
			notifyIcon.ShowBalloonTip(60000);
			recorder.Record();
			toolStripButtonRecord.Enabled = false;
			toolStripButtonPause.Enabled = true;
			toolStripButtonStop.Enabled = true;
			toolStripButtonPlay.Enabled = false;
		}

		private void toolStripButtonPause_Click(object sender, EventArgs e)
		{
			if (toolStripButtonPause.Checked)
				recorder.Stop();
			else
				recorder.Resume();
		}

		private void toolStripButtonStop_Click(object sender, EventArgs e)
		{
			recorder.Stop();
			WindowState = FormWindowState.Normal;
			Activate();
			converter.ConvertToTable();
			dataGridView.Refresh();
			shouldSave = true;
			toolStripButtonRecord.Enabled = true;
			toolStripButtonPause.Enabled = false;
			toolStripButtonStop.Enabled = false;
			toolStripButtonPlay.Enabled = true;
			notifyIcon.Visible = false;
			notifyIcon.ShowBalloonTip(0);
		}

		private void toolStripButtonPlay_Click(object sender, EventArgs e)
		{
			converter.ConvertToRaw();
			WindowState = FormWindowState.Minimized;
			recorder.Play();
			WindowState = FormWindowState.Normal;
			Activate();
		}

		private void toolStripButtonSave_Click(object sender, EventArgs e)
		{
			if (converter != null)
			{
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					converter.ConvertToRaw();
					//converter.ConvertToTable();
					recorder.Save(saveFileDialog.FileName);
					shouldSave = false;
				}
			}
		}

		private void toolStripButtonLoad_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				recorder.Load(openFileDialog.FileName);
				shouldSave = false;
				converter.ConvertToTable();
				notifyIcon.Visible = false;
				notifyIcon.ShowBalloonTip(0);
				toolStripButtonRecord.Enabled = true;
				toolStripButtonPause.Enabled = false;
				toolStripButtonStop.Enabled = false;
				toolStripButtonPlay.Enabled = true;
			}
		}

		private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			shouldSave = true;
		}

		private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			shouldSave = true;
		}

		private void MacroRecorderForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (shouldSave)
			{
				DialogResult dr = MessageBox.Show("Your recording has not been saved.\nAre you sure you want to quit ?", "Discard Changes ?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (dr == DialogResult.No)
					e.Cancel = true;

			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void StopHotKeyPressed(object sender, EventArgs e)
		{
			if (toolStripButtonStop.Enabled)
				toolStripButtonStop_Click(this, EventArgs.Empty);
		}

		private bool shouldSave = false;
		private MacroRecorder recorder = new MacroRecorder();
		private MacroDataTableConverter converter;
		private MacroRecordingsDataSet.MacroEntriesDataTable table = new MacroRecordingsDataSet.MacroEntriesDataTable();
		ManagedWinapi.Hotkey stopHotKey;
		

		
	}
}
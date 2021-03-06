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
using System.Text;
using QAliber.Recorder;
using QAliber.Engine;
using System.Windows.Forms;
using QAliber.Engine.Controls.Web;

namespace QAliber.VS2005.Plugin.Commands
{
	public class RecordCommand : Command
	{

		public override void Invoke()
		{
			try
			{
				if (PrepareForRecording())
				{
					OnInvoke();
					Statics.Recorder.Record();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error while recording", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private bool PrepareForRecording()
		{
			if (Statics.SpyControl.rootControl is QAliber.Engine.Controls.WPF.WPFRoot)
			{
				MessageBox.Show("WPF recording is not yet implemented", "Record");
				return false;
			}
			RecorderConfig.Default.RootControl = Statics.SpyControl.rootControl;
			Statics.SpyControl.notifyIcon.Visible = true;
			Statics.SpyControl.notifyIcon.ShowBalloonTip(60000);
			
			return true;

		}

		private bool PrepareForWebRecording()
		{
			System.Diagnostics.Process[] ieProcesses = System.Diagnostics.Process.GetProcessesByName("iexplore");
			if (ieProcesses.Length > 0)
			{
				DialogResult result = MessageBox.Show("Internet explorer is open on your machine, and need to be closed.\nDo you want to close it (choosing No will cancel your recording) ?",
					"Close Internet Explorer ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (result == DialogResult.No)
					return false;
				foreach (System.Diagnostics.Process ieProcess in ieProcesses)
				{
					ieProcess.Kill();
					ieProcess.WaitForExit(30000);
				}
			}
			System.Diagnostics.Process testedProcess = System.Diagnostics.Process.Start("iexplore", RecorderConfig.Default.WebStartURL);
			testedProcess.WaitForInputIdle();
			return true;
		}

	   

	}
}

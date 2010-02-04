using System;
using System.Collections.Generic;
using System.Text;
using QAliber.Recorder;
using QAliber.Engine;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
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
			if (spyToolWin.control.rootControl is QAliber.Engine.Controls.WPF.WPFRoot)
			{
				MessageBox.Show("WPF recording is not yet implemented", "Record");
				return false;
			}
			if (Statics.DTE.ActiveDocument == null)
			{
				DialogResult dr = MessageBox.Show("No active document is open in your solution, code generated will be copied to clipboard.\r\nAre you sure ?", "No Active Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dr == DialogResult.No)
					return false;
			}
			//IVsPackage package = spyToolWin.Package as IVsPackage;
			//if (package != null)
			//{
			//	  object obj;
			//	  package.GetAutomationObject("QAliber Test Developer.Recorder", out obj);
			//	  RecorderConfig.Default = obj as RecorderConfig;
			//	  package.GetAutomationObject("QAliber Test Developer.Player", out obj);
			//	  PlayerConfig.Default = obj as PlayerConfig;
			//}
			RecorderConfig.Default.RootControl = spyToolWin.control.rootControl;
			spyToolWin.control.notifyIcon.Visible = true;
			spyToolWin.control.notifyIcon.ShowBalloonTip(60000);
			Statics.DTE.MainWindow.WindowState = vsWindowState.vsWindowStateMinimize;
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

		public SpyToolWindow SpyToolWin
		{
			get { return spyToolWin; }
			set { spyToolWin = value; }
		}

		protected SpyToolWindow spyToolWin;
	}
}

using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using QAliber.Logger;
using System.Text.RegularExpressions;
using System.Diagnostics;



namespace QAliber.Repository.CommonTestCases.FileSystem
{
	/// <summary>
	/// Waits for a process
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Processes")]
	public class WaitProcess : global::QAliber.TestModel.TestCase
	{
		public WaitProcess()
		{
			name = "Wait For Process";
			icon = Properties.Resources.StartProcess;
		}

		public override void Body()
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();
			while (watch.ElapsedMilliseconds < timeout)
			{
				Process[] processes = Process.GetProcessesByName(filename);
				if (processes.Length > 0)
				{
					Log.Default.Info("Process arrived");
					actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
					break;
				}
				System.Threading.Thread.Sleep(50);
			}
			if (actualResult != QAliber.RemotingModel.TestCaseResult.Passed)
			{
				actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				Log.Default.Error("Process did not load after " + timeout);
			}
		}

		private string filename = "";

		/// <summary>
		/// The process to wait for
		/// </summary>
		[Category("Process")]
		[Description("The process to wait for")]
		[DisplayName("1) Process Name")]
		public string Filename
		{
			get { return filename; }
			set { filename = value; }
		}

		private int timeout;

		/// <summary>
		/// The maximum time (in miliseconds) to wait for the process, if timeout will reach the test case will fail
		/// </summary>
		[Category("Process")]
		[DisplayName("2) Timeout")]
		[Description("The maximum time (in miliseconds) to wait for the process to load")]
		public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}
	

		
		public override string Description
		{
			get
			{
				return "Waiting for process '" + filename + "' to load within " + timeout + " miliseconds";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}

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
	/// Launches a process
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Processes")]
	public class StartProcess : global::QAliber.TestModel.TestCase
	{
		public StartProcess()
		{
			name = "Start Process";
			icon = Properties.Resources.StartProcess;
		}

		public override void Body()
		{
			ProcessStartInfo psi = new ProcessStartInfo(filename, argumnets);
			psi.WorkingDirectory = workDir;
			Process p = new Process();
			p.StartInfo = psi;
			p.Start();
			if (waitForExit)
				p.WaitForExit();
			else if (waitForUserInteraction)
				p.WaitForInputIdle();
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string filename = "";

		/// <summary>
		/// The process path to launch
		/// </summary>
		[Category("Process")]
		[Editor(typeof(UITypeEditors.FileBrowseTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Description("The process to launch")]
		[DisplayName("1) Process")]
		public string Filename
		{
			get { return filename; }
			set { filename = value; }
		}

		private string argumnets = "";

		/// <summary>
		/// Arguments to be passed to the process
		/// </summary>
		[Category("Process")]
		[Description("additional arguments to give the process")]
		[DisplayName("2) Arguments")]
		public string Arguments
		{
			get { return argumnets; }
			set { argumnets = value; }
		}
	

		private string workDir = "";

		/// <summary>
		/// The working directory of the process
		/// </summary>
		[Category("Process")]
		[Description("The working directory of the process")]
		[DisplayName("3) Working Directory")]
		public string WorkDir
		{
			get { return workDir; }
			set { workDir = value; }
		}

		private bool waitForExit;

		/// <summary>
		/// Should the test run wait for the process to exit
		/// <remarks>If you want to wait for user interaction then set this property to false</remarks>
		/// </summary>
		[Category("Process")]
		[Description("Should the test scenario wait for the process to quit ?")]
		[DisplayName("4) Wait For Exit ?")]
		public bool WaitForExit
		{
			get { return waitForExit; }
			set { waitForExit = value; }
		}

		private bool waitForUserInteraction;

		/// <summary>
		/// Should the test run wait for the process's UI to be interactive
		/// <remarks>Applicable only for processes that launch a user interface</remarks>
		/// </summary>
		[Category("Process")]
		[Description("Should the test scenario wait for the process to recieve user input ?")]
		[DisplayName("5) Wait For User Interaction ?")]
		public bool WaitForUserInteraction
		{
			get { return waitForUserInteraction; }
			set { waitForUserInteraction = value; }
		}
	
		public override string Description
		{
			get
			{
				return "Launching process '" + filename + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}

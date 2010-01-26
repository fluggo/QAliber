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
	/// Kills a process
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Processes")]
	public class KillProcess : global::QAliber.TestModel.TestCase
	{
		public KillProcess()
		{
			name = "Kill Process";
			icon = Properties.Resources.ProcessRemove;
		}

		public override void Body()
		{
			Process[] processes = Process.GetProcessesByName(filename);
			if (processes.Length > 0)
			{
				if (killAll)
				{
					foreach (Process p in processes)
					{
						p.Kill();
					}
				}
				else
					processes[0].Kill();
				
			}
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		private string filename = "";

		/// <summary>
		/// The name of the process to kill
		/// </summary>
		[Category("Process")]
		[Description("The process to kill")]
		[DisplayName("1) Process Name")]
		public string Filename
		{
			get { return filename; }
			set { filename = value; }
		}

		private bool killAll = true;

		/// <summary>
		/// If multiple processes exist with the same name, kill them all ?
		/// </summary>
		[Category("Process")]
		[DisplayName("2) Kill All Processes With Same Name ?")]
		public bool KillAll
		{
			get { return killAll; }
			set { killAll = value; }
		}
	

		
		public override string Description
		{
			get
			{
				return "Killing process '" + filename + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}

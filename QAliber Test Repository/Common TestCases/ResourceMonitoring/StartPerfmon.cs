using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using QAliber.Logger;
using System.Text.RegularExpressions;
using System.Diagnostics;



namespace QAliber.Repository.CommonTestCases.ResourceMonitoring
{
	/// <summary>
	/// Starts counters logging from windows performace monitor
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Resources Monitoring")]
	public class StartPerfmon : global::QAliber.TestModel.TestCase
	{
		public StartPerfmon()
		{
			name = "Start Performance Monitor";
		}

		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			ProcessStartInfo psi = new ProcessStartInfo("logman",
				string.Format("update {0} -f csv -o \"{1}\\perflog.csv\"", perfname, Log.Default.Path));
			psi.WindowStyle = ProcessWindowStyle.Hidden;
			Process p = Process.Start(psi);
			if (!p.WaitForExit(20000))
				throw new TimeoutException("logman did not exit after 20 seconds");
			if (p.ExitCode != 0)
				throw new InvalidOperationException("logman did not exited successfully");
			psi.Arguments = string.Format("start {0}", perfname);
			p = Process.Start(psi);
			if (!p.WaitForExit(20000))
				throw new TimeoutException("logman did not exit after 20 seconds");
			if (p.ExitCode != 0)
				throw new InvalidOperationException("logman did not exited successfully");
		}

		private string perfname = "";

		/// <summary>
		/// The counter log as appears in windows perfmon application 
		/// <remarks>
		/// To configure a counter log click &lt;a href="http://technet.microsoft.com/en-us/library/cc958263.aspx"&gt;here&lt;/a&gt;.
		/// </remarks>
		/// </summary>
		[Category("Performance Monitoring")]
		[Description("The counter name, as it appears in the perfmon application")]
		[DisplayName("Perfmon Log Counter Name")]
		public string CounterName
		{
			get { return perfname; }
			set { perfname = value; }
		}

		
		public override string Description
		{
			get
			{
				return "Starting performance monitor for '" + perfname + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}

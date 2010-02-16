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

using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using QAliber.Logger;
using System.Text.RegularExpressions;
using System.Diagnostics;



namespace QAliber.Repository.CommonTestCases.ResourceMonitoring
{
	/// <summary>
	/// Stops counters logging from windows performace monitor
	/// </summary>
	[Serializable]
	[global::QAliber.TestModel.Attributes.VisualPath(@"System\Resources Monitoring")]
	public class StopPerfmon : global::QAliber.TestModel.TestCase
	{
		public StopPerfmon()
		{
			name = "Stop Performance Monitor";
		}

		public override void Body()
		{
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
			ProcessStartInfo psi = new ProcessStartInfo("logman",
				string.Format("stop {0}", perfname));
			psi.WindowStyle = ProcessWindowStyle.Hidden;
			Process p = Process.Start(psi);
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
				return "Stopping performance monitor for '" + perfname + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	
	
	}
}

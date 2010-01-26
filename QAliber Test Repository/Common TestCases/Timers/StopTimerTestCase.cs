using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using QAliber.Logger;
using System.Drawing;
using QAliber.TestModel.Attributes;

namespace QAliber.Repository.CommonTestCases.Timers
{

	[Serializable]
	[VisualPath(@"System\Timings")]
	public class StopTimerTestCase : QAliber.TestModel.TestCase
	{
		/// <summary>
		/// Stops a timer
		/// <precondtions>A previous 'Start Timer' with the same key was executed</precondtions>
		/// </summary>
		public StopTimerTestCase()
		{
			name = "Stop Timer";
			icon = Properties.Resources.Timing;
		}

		private string key = "";

		/// <summary>
		/// The same key that was given in the corresponding 'Start Timer'
		/// </summary>
		[Category("Timer")]
		[Description("Enter the same key you entered for 'Start Timer'")]
		public string Key
		{
			get { return key; }
			set { key = value; }
		}


		private double maxTimeAllowed = 0;

		/// <summary>
		/// The maximum time in miliseconds for this test case to pass, 0 for no limit
		/// </summary>
		[Category("Timer")]
		[DisplayName("Maximum Time Allowed")]
		[Description("Enter the maximum timing for this measurement (if the timing will be above it the test case result will be failed)\nEnter 0 for no limit")]
		public double MaxTimeAllowed
		{
			get { return maxTimeAllowed; }
			set { maxTimeAllowed = value; }
		}

		public override void Body()
		{
			double val = TimersCollection.StopTimer(key);
			if (maxTimeAllowed > 0)
			{
				if (val > maxTimeAllowed)
				{
					LogFailedByExpectedResult("Operation exceeded requested time", string.Format("Maximum = {0}, Actual = {1}", maxTimeAllowed, val));
					actualResult = QAliber.RemotingModel.TestCaseResult.Failed;
				}
				else
				{
					LogPassedByExpectedResult(string.Format("Operation ended {0}", val), "");
					actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
				}
			}
		}

		public override string Description
		{
			get
			{
				return "Stopping measuring time for '" + key + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	}
}

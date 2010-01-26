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
	/// <summary>
	/// Starts to measure time
	/// </summary>
	[Serializable]
	[VisualPath(@"System\Timings")]
	public class StartTimerTestCase : QAliber.TestModel.TestCase
	{
		public StartTimerTestCase()
		{
			name = "Start Timer";
			icon = Properties.Resources.Timing;
		}

		private string key = "";

		/// <summary>
		/// A unique key that describe the timing (the same key should be used in 'Stop Timer'
		/// </summary>
		[Category("Timer")]
		[Description("Enter a unique key for the time you want to measure, please use the exact same key in a corresponding 'Stop Timer' test case")]
		public string Key
		{
			get { return key; }
			set { key = value; }
		}

		public override void Body()
		{
			TimersCollection.AddTimer(key);
		}

		public override string Description
		{
			get
			{
				return "Starting measuring time for '" + key + "'";
			}
			set
			{
				base.Description = value;
			}
		}
	
	}
}

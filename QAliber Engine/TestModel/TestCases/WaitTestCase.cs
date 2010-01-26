using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using QAliber.Logger;
using System.Drawing;
using QAliber.TestModel.Attributes;

namespace QAliber.TestModel
{

	/// <summary>
	/// Pause the execution for a specified amount of time
	/// </summary>
	[Serializable]
	[VisualPath(@"System\Timings")]
	public class WaitTestCase : TestCase
	{
		public WaitTestCase()
		{
			name = "Wait";
			icon = Properties.Resources.Clock;
		}

		private int delay = 1000;

		/// <summary>
		/// The length of the pause in miliseconds
		/// </summary>
		[Category(" Wait")]
		[Description("The amount of time (in miliseconds) to suspend the execution")]
		public int Delay
		{
			get { return delay; }
			set { delay = value; }
		}

		public override void Body()
		{
			System.Threading.Thread.Sleep(delay);
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		public override string Description
		{
			get
			{
				return "Waiting For " + delay + " Milliseconds";
			}
			set
			{
				base.Description = value;
			}
		}
	
	}
}

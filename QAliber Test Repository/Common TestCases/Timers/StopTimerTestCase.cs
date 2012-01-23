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
		public StopTimerTestCase() : base( "Stop Timer" )
		{
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
		}
	
	}
}

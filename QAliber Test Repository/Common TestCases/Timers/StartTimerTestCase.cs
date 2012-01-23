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
	/// <summary>
	/// Starts to measure time
	/// </summary>
	[Serializable]
	[VisualPath(@"System\Timings")]
	public class StartTimerTestCase : QAliber.TestModel.TestCase
	{
		public StartTimerTestCase() : base( "Start Timer" )
		{
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
			actualResult = QAliber.RemotingModel.TestCaseResult.Passed;
		}

		public override string Description
		{
			get
			{
				return "Starting measuring time for '" + key + "'";
			}
		}
	
	}
}

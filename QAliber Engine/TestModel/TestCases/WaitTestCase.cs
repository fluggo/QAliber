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

namespace QAliber.TestModel
{

	/// <summary>
	/// Pause the execution for a specified amount of time
	/// </summary>
	[Serializable]
	[VisualPath(@"System\Timings")]
	public class WaitTestCase : TestCase
	{
		public WaitTestCase() : base( "Wait" )
		{
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
		}
	
	}
}

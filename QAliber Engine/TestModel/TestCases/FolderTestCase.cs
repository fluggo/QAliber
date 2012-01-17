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
using QAliber.RemotingModel;

namespace QAliber.TestModel
{
	/// <summary>
	/// A simple folder for more organized and hierarchic test scenario, you can add test cases for this folder
	/// </summary>
	[Serializable]
	[VisualPath(@"Flow Control")]
	public class FolderTestCase : TestCase
	{
		public FolderTestCase()
		{
			name = "Folder";
			expectedResult = TestCaseResult.Passed;
			icon = Properties.Resources.Folder;
			
		}

		protected List<TestCase> children = new List<TestCase>();

		/// <summary>
		/// The children of this test case
		/// </summary>
		[Browsable(false)]
		public List<TestCase> Children
		{
			get { return children; }
			set { children = value; }
		}
		
		public override void Body()
		{
			actualResult = TestCaseResult.Passed;
			bool stopRunning = false;

			foreach (TestCase child in children)
			{
				if( child.MarkedForExecution && (!stopRunning || child.AlwaysRun) )
				{
					child.Run();

					if( stopRunning )
						continue;
					
					if (child.ExpectedResult != TestCaseResult.None && child.ActualResult != child.ExpectedResult)
					{
						actualResult = TestCaseResult.Failed;
						if (child.ExitOnError)
						{
							Log.Default.Error("Stop on error was requested for this step, exiting");
							exitTotally = true;
							break;
						}
						if (child.ExitBranchOnError)
						{
							Log.Default.Info("Exiting from branch on error as requested");
							stopRunning = true;
						}
					}
					if (branchesToBreak > 0)
					{
						branchesToBreak--;
						break;
					}
					if (exitTotally)
						stopRunning = true;
					
				}
				
				
			}
		}
	}
	
}
